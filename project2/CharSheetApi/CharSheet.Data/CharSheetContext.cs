using Microsoft.EntityFrameworkCore;
using CharSheet.Domain;

namespace CharSheet.Data
{
    public class CharSheetContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<Sheet> Sheets { get; set; }
        public DbSet<FormInputGroup> FormInputGroups { get; set; }
        public DbSet<FormTemplate> FormTemplates { get; set; }
        public DbSet<FormInput> FormInputs { get; set; }
        public DbSet<FormPosition> FormPositions { get; set; }
        public DbSet<FormLabel> FormLabels { get; set; }

        public CharSheetContext(DbContextOptions<CharSheetContext> options)
            : base(options)
        { }
    }
}
