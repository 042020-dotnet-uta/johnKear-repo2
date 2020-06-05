using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using CharSheet.Data;
using CharSheet.Domain;
using CharSheet.Api.Services;
using CharSheet.Api.Models;

namespace CharSheet.Test
{
    public partial class APITests : DbContextTests
    {
        public Guid InsertUser(DbContextOptions<CharSheetContext> options)
        {
            using (var context = GetContext(options))
            {
                var user = context.Users.Add(new User()).Entity;
                context.SaveChanges();
                return user.UserId;
            }
        }

        public IBusinessService GetBusinessService(CharSheetContext context)
        {
            var logger = new Mock<ILogger<BusinessService>>();
            return new BusinessService(logger.Object, context);
        }

        public async Task<TemplateModel> InsertTemplate(IBusinessService service, Guid id)
        {
            var templateModel = new TemplateModel
            {
                FormTemplates = new List<FormTemplateModel>
                    {
                        new FormTemplateModel
                        {
                            Labels = new List<string>()
                        }
                    }
            };
            return await service.CreateTemplate(templateModel, id);
        }

        public async Task<SheetModel> InsertSheet(IBusinessService service, Guid id)
        {
            var templateModel = await InsertTemplate(service, id);
            var sheetModel = new SheetModel
            {
                FormGroups = new List<FormInputGroupModel>
                {
                    new FormInputGroupModel
                    {
                        FormTemplateId = templateModel.FormTemplates.First().FormTemplateId,
                        FormInputs = new List<string>()
                    }
                }
            };
            return await service.CreateSheet(sheetModel, id);
        }
    }
}