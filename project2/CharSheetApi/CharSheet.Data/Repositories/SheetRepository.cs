using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CharSheet.Domain;
using CharSheet.Domain.Interfaces;

namespace CharSheet.Data.Repositories
{
    public class SheetRepository : GenericRepository<Sheet>, ISheetRepository
    {
        public SheetRepository(CharSheetContext context)
            : base(context)
        { }

        public async override Task<Sheet> Find(object id)
        {
            var sheet = (await this.Get(s => s.SheetId == (Guid) id, null, "FormInputGroups,FormInputGroups.FormInputs,FormInputGroups.FormTemplate,FormInputGroups.FormTemplate.FormPosition,FormInputGroups.FormTemplate.FormLabels")).FirstOrDefault();
            if (sheet != null && sheet.FormInputGroups != null)
                sheet.FormInputGroups = sheet.FormInputGroups.OrderBy(fig => fig.FormTemplateId).ToList();
            return sheet;
        }

        public async Task<IEnumerable<FormInputGroup>> GetFormInputGroups(object id)
        {
            return await base._context.FormInputGroups.Where(formInputGroup => formInputGroup.SheetId == (Guid) id).ToListAsync();
        }
    }
}