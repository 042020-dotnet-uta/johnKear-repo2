using System;
using System.Linq;
using System.Threading.Tasks;
using CharSheet.Domain;
using CharSheet.Domain.Interfaces;

namespace CharSheet.Data.Repositories
{
    public class FormTemplateRepository : GenericRepository<FormTemplate>, IFormTemplateRepository
    {
        public FormTemplateRepository(CharSheetContext context)
            : base(context)
        { }

        public async override Task<FormTemplate> Find(object id)
        {
            return (await Get(FormTemplate => FormTemplate.FormTemplateId == (Guid) id, null, "FormPosition,FormLabels")).FirstOrDefault();
        }
    }
}