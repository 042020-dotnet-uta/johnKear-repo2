using CharSheet.Data;
using CharSheet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using Xunit;
using System.Collections.Generic;
using CharSheet.Data.Repositories;

namespace CharSheet.Test
{
    public class RepositoryTestsDerived : RepositoryTests
    {
        [Fact]
        public async void FormInptGroupFind()
        {
            // Arrange
            var options = GetOptions("FormInputGroupFind");
            Guid id;
            FormInputGroup formInputGroup = null;
            using (var unitOfWork = GetUnitOfWork(options))
            {
                var newFormInputGroup = await unitOfWork.FormInputGroupRepository.Insert(new FormInputGroup
                {
                    FormTemplate = new FormTemplate
                    {
                        FormPosition = new FormPosition(),
                        FormLabels = new List<FormLabel>()
                    },
                    FormInputs = new List<FormInput>()
                });
                id = newFormInputGroup.FormInputGroupId;
                await unitOfWork.Save();
            }

            // Act
            using (var unitOfWork = GetUnitOfWork(options))
            {
                formInputGroup = await unitOfWork.FormInputGroupRepository.Find(id);
            }

            // Assert
            Assert.NotNull(formInputGroup);
        }

        [Fact]
        public async void FormInputRemoveRange()
        {
            // Arrange
            var options = GetOptions("FormInputRemoveRange");
            using (var unitOfWork = GetUnitOfWork(options))
            {
                await unitOfWork.FormInputRepository.Insert(new FormInput());
                await unitOfWork.FormInputRepository.Insert(new FormInput());
                await unitOfWork.Save();
            }

            // Act
            using (var unitOfWork = GetUnitOfWork(options))
            {
                var formInputs = await unitOfWork.FormInputRepository.All();
                await unitOfWork.FormInputRepository.RemoveRange(formInputs);
                await unitOfWork.Save();
            }

            // Assert
            using (var unitOfWork = GetUnitOfWork(options))
            {
                var formInputs = await unitOfWork.FormInputRepository.All();
                Assert.Empty(formInputs);
            }
        }

        [Fact]
        public async void FormTemplateFind()
        {
            // Arrange
            var options = GetOptions("FormTemplateFind");
            FormTemplate formTemplate = null;
            using (var unitOfWork = GetUnitOfWork(options))
            {
                await unitOfWork.FormTemplateRepository.Insert(new FormTemplate
                {
                    FormLabels = new List<FormLabel>(),
                    FormPosition = new FormPosition()
                });
                await unitOfWork.Save();
            }

            // Act
            using (var unitOfWork = GetUnitOfWork(options))
            {
                var formTemplates = await unitOfWork.FormTemplateRepository.All();
                formTemplate = await unitOfWork.FormTemplateRepository.Find(formTemplates.First().FormTemplateId);
            }

            // Assert
            Assert.NotNull(formTemplate);
        }

        [Fact]
        public async void SheetFind()
        {
            // Arrange
            var options = GetOptions("SheetFind");
            Sheet sheet = null;
            using (var unitOfWork = GetUnitOfWork(options))
            {
                await unitOfWork.SheetRepository.Insert(new Sheet
                {
                    FormInputGroups = new List<FormInputGroup>
                    {
                        new FormInputGroup
                        {
                            FormInputs = new List<FormInput>(),
                            FormTemplate = new FormTemplate
                            {
                                FormPosition = new FormPosition(),
                                FormLabels = new List<FormLabel>()
                            }
                        }
                    }
                });

                await unitOfWork.Save();
            }

            // Act
            using (var unitOfWork = GetUnitOfWork(options))
            {
                var sheets = await unitOfWork.SheetRepository.All();
                sheet = await unitOfWork.SheetRepository.Find(sheets.First().SheetId);
            }

            // Assert
            Assert.NotNull(sheet);
        }

        [Fact]
        public async void SheetGetFormInputGroups()
        {
            // Arrange
            var options = GetOptions("SheetGetFormInputGroups");
            IEnumerable<FormInputGroup> formInputGroups;
            using (var unitOfWork = GetUnitOfWork(options))
            {
                await unitOfWork.SheetRepository.Insert(new Sheet
                {
                    FormInputGroups = new List<FormInputGroup>
                    {
                        new FormInputGroup(),
                        new FormInputGroup()
                    }
                });

                await unitOfWork.Save();
            }

            // Act
            using (var unitOfWork = GetUnitOfWork(options))
            {
                var sheets = await unitOfWork.SheetRepository.All();
                var sheet = sheets.First();
                formInputGroups = await unitOfWork.SheetRepository.GetFormInputGroups(sheet.SheetId);
            }

            // Assert
            Assert.NotEmpty(formInputGroups);
        }

        [Fact]
        public async void TemplateFind()
        {
            // Arrange
            var options = GetOptions("TemplateFind");
            Template template = null;
            using (var unitOfWork = GetUnitOfWork(options))
            {
                await unitOfWork.TemplateRepository.Insert(new Template
                {
                    FormTemplates = new List<FormTemplate>
                    {
                        new FormTemplate
                        {
                            FormPosition = new FormPosition(),
                            FormLabels = new List<FormLabel>()
                        }
                    }
                });
                await unitOfWork.Save();
            }

            // Act
            using (var unitOfWork = GetUnitOfWork(options))
            {
                var templates = await unitOfWork.TemplateRepository.All();
                template = await unitOfWork.TemplateRepository.Find(templates.First().TemplateId);
            }

            // Assert
            Assert.NotNull(template);
        }

        [Fact]
        public async void GetAllLogins()
        {
            // Arrange
            var options = GetOptions("GetAllLogins");
            Login login = null;
            using (var unitOfWork = GetUnitOfWork(options))
            {
                await unitOfWork.LoginRepository.Insert(new Login
                {
                    UserId = new Guid(),
                    LoginId = new Guid(),
                    Hashed = "hashed",
                    IterationCount = 1,
                    Salt = new byte[1],
                }) ;
                await unitOfWork.Save();
            }

            // Act
            using (var unitOfWork = GetUnitOfWork(options))
            {
                var logins = await unitOfWork.LoginRepository.All();
                login = await unitOfWork.LoginRepository.Find(logins.First().LoginId);
            }

            // Assert
            Assert.NotNull(login);
        }
    }
}
