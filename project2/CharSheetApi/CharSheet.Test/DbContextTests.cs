using CharSheet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using Xunit;

namespace CharSheet.Test
{
	public class DbContextTests
	{
		public DbContextOptions<CharSheetContext> GetOptions(string connectionString)
        {
            return new DbContextOptionsBuilder<CharSheetContext>()
                .UseInMemoryDatabase(connectionString)
                .Options;
        }

        public CharSheetContext GetContext(DbContextOptions<CharSheetContext> options)
        {
            return new CharSheetContext(options);
        }
	}
}
