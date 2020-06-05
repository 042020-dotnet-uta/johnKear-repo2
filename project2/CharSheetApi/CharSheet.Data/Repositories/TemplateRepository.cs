using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CharSheet.Domain;
using CharSheet.Domain.Interfaces;

namespace CharSheet.Data.Repositories
{
    public class TemplateRepository : GenericRepository<Template>, ITemplateRepository
    {
        public TemplateRepository(CharSheetContext context)
            : base(context)
        { }

        public async override Task<IEnumerable<Template>> All()
        {
            return (await this.Get(null, null, "FormTemplates,FormTemplates.FormPosition,FormTemplates.FormLabels")).AsEnumerable();
        }

        public async override Task<Template> Find(object id)
        {
            return (await this.Get(template => template.TemplateId == (Guid) id, null, "FormTemplates,FormTemplates.FormPosition,FormTemplates.FormLabels")).FirstOrDefault();
        }
    }
}