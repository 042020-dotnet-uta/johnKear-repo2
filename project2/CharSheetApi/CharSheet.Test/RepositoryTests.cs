using CharSheet.Data;
using CharSheet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using Xunit;

namespace CharSheet.Test
{
	public class RepositoryTests : DbContextTests
	{
		public UnitOfWork GetUnitOfWork(DbContextOptions<CharSheetContext> options)
        {
            return new UnitOfWork(new CharSheetContext(options));
        }

        public async void InsertUser(DbContextOptions<CharSheetContext> options)
        {
            using (var unitOfWork = GetUnitOfWork(options))
            {
                await unitOfWork.UserRepository.Insert(new User
                {
                    Login = new Login()
                });
                await unitOfWork.Save();
            }
        }
	}
}
