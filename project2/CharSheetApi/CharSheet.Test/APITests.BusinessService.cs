using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;
using CharSheet.Api;
using CharSheet.Api.Models;
using CharSheet.Api.Services;
using CharSheet.Domain;
using CharSheet.Data;

namespace CharSheet.Test
{
    public partial class APITests : DbContextTests
    {
        [Fact]
        public async void CreateTemplate()
        {
            // Arrrange
            var options = GetOptions("CreateTemplate");
            Guid id;
            using (var context = GetContext(options))
            {
                id = InsertUser(options);
            }

            // Act
            using (var context = GetContext(options))
            {
                var service = GetBusinessService(context);
                await InsertTemplate(service, id);
            }

            // Assert
            using (var context = GetContext(options))
            {
                Assert.Single(context.Templates.ToList());
            }
        }

        [Fact]
        public async void GetTemplates()
        {
            // Arrange
            var options = GetOptions("GetTemplates");
            Guid id;
            TemplateModel templateModel;
            IEnumerable<TemplateModel> templateModels;
            using (var context = GetContext(options))
            {
                var service = GetBusinessService(context);
                id = InsertUser(options);
                templateModel = await InsertTemplate(service, id);
            }

            // Act
            using (var context = GetContext(options))
            {
                var service = GetBusinessService(context);
                templateModel = await service.GetTemplate(templateModel.TemplateId);
                templateModels = await service.GetTemplates(id);
            }

            // Assert
            Assert.NotNull(templateModel);
            Assert.Single(templateModels);
        }

        [Fact]
        public async void CreateSheet()
        {
            // Arrange
            var options = GetOptions("CreateSheet");
            var id = InsertUser(options);

            // Act
            using (var context = GetContext(options))
            {
                var service = GetBusinessService(context);
                await InsertSheet(service, id);
            }

            // Assert
            using (var context = GetContext(options))
            {
                Assert.Single(context.Sheets.ToList());
            }
        }

        [Fact]
        public async void GetSheets()
        {
            // Arrange
            var options = GetOptions("GetSheets");
            var id = InsertUser(options);
            Guid sheetId;
            SheetModel sheetModel;
            IEnumerable<SheetModel> sheetModels;
            using (var context = GetContext(options))
            {
                var service = GetBusinessService(context);
                sheetId = (await InsertSheet(service, id)).SheetId;
            }

            // Act
            using (var context = GetContext(options))
            {
                var service = GetBusinessService(context);
                sheetModel = await service.GetSheet(sheetId);
                sheetModels = await service.GetSheets(id);
            }

            // Assert
            Assert.NotNull(sheetModel);
            Assert.Single(sheetModels);
        }

        [Fact]
        public async void UpdateSheet()
        {
            // Arrange
            var options = GetOptions("UpdateSheet");
            var id = InsertUser(options);
            SheetModel sheetModel;
            using (var context = GetContext(options))
            {
                var service = GetBusinessService(context);
                sheetModel = await InsertSheet(service, id);
            }
            sheetModel.FormGroups.First().FormInputs = new List<string>
            {
                "something",
                "hi"
            }.AsEnumerable();

            // Act
            using (var context = GetContext(options))
            {
                var service = GetBusinessService(context);
                await service.UpdateSheet(sheetModel, id);
            }

            // Assert
            using (var context = GetContext(options))
            {
                var inputs = context.Sheets
                    .Include(s => s.FormInputGroups)
                    .ThenInclude(fig => fig.FormInputs)
                    .ToList()
                    .First()
                    .FormInputGroups
                    .First()
                    .FormInputs;
                Assert.Equal(2, inputs.Count());
            }
        }

        [Fact]
        public async void DeleteSheet()
        {
            // Arrange
            var options = GetOptions("DeleteSheet");
            var id = InsertUser(options);
            SheetModel sheetModel;
            using (var context = GetContext(options))
            {
                var service = GetBusinessService(context);
                sheetModel = await InsertSheet(service, id);
            }

            // Act
            using (var context = GetContext(options))
            {
                var service = GetBusinessService(context);
                await service.DeleteSheet(sheetModel.SheetId, id);
            }

            // Assert
            using (var context = GetContext(options))
            {
                Assert.Empty(context.Sheets.ToList());
            }
        }
    }
}