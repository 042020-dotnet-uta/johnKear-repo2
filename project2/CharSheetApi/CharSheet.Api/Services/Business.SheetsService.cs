using System;
using System.Security;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using CharSheet.Domain;
using CharSheet.Api.Models;

namespace CharSheet.Api.Services
{
    public partial interface IBusinessService
    {
        #region GET
        Task<IEnumerable<SheetModel>> GetSheets(object id);
        Task<SheetModel> GetSheet(object id);
        #endregion

        #region POST
        Task<SheetModel> CreateSheet(SheetModel sheetModel, Guid userId);
        #endregion

        #region PUT
        Task<SheetModel> UpdateSheet(SheetModel sheetModel, Guid userId);
        #endregion

        #region DELETE
        Task DeleteSheet(object id, Guid userId);
        #endregion
    }

    public partial class BusinessService : IBusinessService
    {
        #region GET
        /// <summary>
        /// Get all sheets by a user.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>All sheets by a user.</returns>
        public async Task<IEnumerable<SheetModel>> GetSheets(object id)
        {
            // Load sheets from database, filter by user id.
            var sheets = (await _unitOfWork.SheetRepository.Get(sheet => sheet.UserId == (Guid)id)).Select(sheet => sheet.SheetId);

            // Sheets as sheet models.
            var sheetModels = new List<SheetModel>();
            foreach (var sheetId in sheets)
            {
                // Sheet as sheet model.
                var sheetModel = await GetSheet(sheetId);
                sheetModels.Add(sheetModel);
            }

            return sheetModels.ToList();
        }

        /// <summary>
        /// Get a sheet by id.
        /// </summary>
        /// <param name="id">Sheet id.</param>
        /// <returns>A sheet.</returns>
        public async Task<SheetModel> GetSheet(object id)
        {
            // Load sheet from database.
            var sheet = await _unitOfWork.SheetRepository.Find(id);
            if (sheet == null)
                throw new InvalidOperationException("Sheet not found");

            // Returns sheet as sheet model.
            return await ToModel(sheet);
        }
        #endregion

        #region POST
        /// <summary>
        /// Create a new sheet.
        /// </summary>
        /// <param name="sheetModel">Sheet model containing the new sheet's properties.</param>
        /// <param name="userId">Id of the user creating the sheet.</param>
        /// <returns>Sheet model of the new sheet.</returns>
        public async Task<SheetModel> CreateSheet(SheetModel sheetModel, Guid userId)
        {
            await AuthenticateUser(userId);

            var sheet = await ToObject(sheetModel);
            sheet.UserId = userId;

            await _unitOfWork.SheetRepository.Insert(sheet);
            await _unitOfWork.Save();

            _logger.LogInformation($"New Sheet: {sheet.SheetId} by {sheet.SheetId}");
            return await ToModel(sheet);
        }
        #endregion

        #region PUT
        /// <summary>
        /// Update a sheet.
        /// </summary>
        /// <param name="sheetModel">Sheet model containing updated sheet properties.</param>
        /// <param name="userId">Id of the user updating the sheet.</param>
        /// <returns>Sheet model containing updated properties.</returns>
        public async Task<SheetModel> UpdateSheet(SheetModel sheetModel, Guid userId)
        {
            await AuthenticateUser(userId);

            // Load existing sheet from database.
            var sheet = await _unitOfWork.SheetRepository.Find(sheetModel.SheetId);
            if (sheet == null)
                throw new InvalidOperationException("Sheet not found.");
            if (sheet.UserId != userId)
                throw new SecurityException("User mismatch.");

            // Validate sheet model structure.
            if (sheetModel.FormGroups == null)
                throw new InvalidOperationException("Missing form groups.");

            sheet.Name = sheetModel.Name;
            sheetModel.FormGroups = sheetModel.FormGroups.OrderBy(fg => fg.FormTemplateId);
            sheet.FormInputGroups = sheet.FormInputGroups.OrderBy(fig => fig.FormTemplate.FormTemplateId).ToList();

            var deletedInputs = new List<FormInput>();

            for (int i = 0; i < sheetModel.FormGroups.Count(); i++)
            {
                var formGroup = sheetModel.FormGroups.ElementAt(i);
                var formInputGroup = sheet.FormInputGroups.ElementAt(i);

                // Verify form templates.
                if (formGroup.FormTemplate?.FormTemplateId != formInputGroup.FormTemplate.FormTemplateId && formGroup.FormTemplateId != formInputGroup.FormTemplate.FormTemplateId)
                    throw new InvalidOperationException("Form template mismatch.");

                int j;
                var formInputsCount = formInputGroup.FormInputs.Count;
                for (j = 0; j < formGroup.FormInputs.Count(); j++)
                {
                    var input = formGroup.FormInputs.ElementAt(j);
                    if (formInputsCount > j)
                    {
                        var formInput = formInputGroup.FormInputs.ElementAt(j);
                        formInput.Value = input;
                    }
                    else
                    {
                        formInputGroup.FormInputs.Add(new FormInput { Index = j, Value = input });
                    }
                }

                // Delete excess form inputs.
                if (j < formInputsCount)
                {
                    deletedInputs.AddRange(formInputGroup.FormInputs.ToList().GetRange(j, formInputsCount - j));
                }
            }

            await _unitOfWork.FormInputRepository.RemoveRange(deletedInputs);
            await _unitOfWork.SheetRepository.Update(sheet);
            await _unitOfWork.Save();

            return await ToModel(sheet);
        }
        #endregion

        #region DELETE
        /// <summary>
        /// Delete a sheet.
        /// </summary>
        /// <param name="id">Id of sheet being deleted.</param>
        /// <param name="userId">Id of user deleting sheet.</param>
        /// <returns></returns>
        public async Task DeleteSheet(object id, Guid userId)
        {
            var sheet = await _unitOfWork.SheetRepository.Find(id);
            if (sheet == null)
                throw new InvalidOperationException("Sheet not found.");
            if (sheet.UserId != userId)
                throw new SecurityException("User mismatch.");

            await _unitOfWork.SheetRepository.Remove(sheet);
            await _unitOfWork.Save();
            _logger.LogInformation($"Deleted Sheet: {sheet.SheetId}");
            // return; //not needed, function type is void.
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Convert form input group object into model.
        /// </summary>
        /// <param name="formInputGroup">Form input group object.</param>
        /// <returns>Form input group model.</returns>
        private async Task<FormInputGroupModel> ToModel(FormInputGroup formInputGroup)
        {
            var formInputGroupModel = new FormInputGroupModel
            {
                // Get form template as model.
                FormTemplate = await ToModel(formInputGroup.FormTemplate),
                FormInputs = formInputGroup.FormInputs.Select(fi => fi.Value)
            };
            return formInputGroupModel;
        }

        /// <summary>
        /// Convert sheet object into model.
        /// </summary>
        /// <param name="sheet">Sheet object.</param>
        /// <returns>Sheet model.</returns>
        private async Task<SheetModel> ToModel(Sheet sheet)
        {
            // Instantiate sheet model.
            var sheetModel = new SheetModel
            {
                SheetId = sheet.SheetId,
                Name = sheet.Name
            };

            // Instantiate a form input group model for each form input group.
            var formInputGroupModels = new List<FormInputGroupModel>();
            foreach (var formInputGroup in sheet.FormInputGroups)
            {
                // Convert form input group object to model.
                var formInputGroupModel = await ToModel(formInputGroup);
                formInputGroupModels.Add(formInputGroupModel);
            }
            sheetModel.FormGroups = formInputGroupModels.ToList();

            return sheetModel;
        }

        /// <summary>
        /// Convert sheet model into object.
        /// </summary>
        /// <param name="sheetModel">Sheet model.</param>
        /// <returns>Sheet object.</returns>
        private async Task<Sheet> ToObject(SheetModel sheetModel)
        {
            var sheet = new Sheet
            {
                FormInputGroups = new List<FormInputGroup>(),
                Name = sheetModel.Name
            };

            // Create form input groups.
            foreach (var formInputGroupModel in sheetModel.FormGroups)
            {
                var formTemplate = await _unitOfWork.FormTemplateRepository.Find(formInputGroupModel.FormTemplateId);
                if (formTemplate == null)
                    throw new InvalidOperationException("Form template not found.");

                var formInputGroup = new FormInputGroup
                {
                    FormTemplate = formTemplate,
                    FormInputs = new List<FormInput>()
                };

                for (int i = 0; i < formInputGroupModel.FormInputs.Count(); i++)
                {
                    var formInput = new FormInput
                    {
                        Index = i,
                        Value = formInputGroupModel.FormInputs.ElementAt(i)
                    };
                    formInputGroup.FormInputs.Add(formInput);
                }

                sheet.FormInputGroups.Add(formInputGroup);
            }

            return await Task.FromResult(sheet);
        }
        #endregion
    }
}