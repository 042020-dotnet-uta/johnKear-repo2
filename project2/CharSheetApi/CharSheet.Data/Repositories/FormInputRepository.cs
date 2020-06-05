using System.Collections.Generic;
using System.Threading.Tasks;
using CharSheet.Domain;
using CharSheet.Domain.Interfaces;

namespace CharSheet.Data.Repositories
{
    public class FormInputRepository : GenericRepository<FormInput>, IFormInputRepository
    {
        public FormInputRepository(CharSheetContext context)
            : base(context)
        { }

        public async Task<IEnumerable<FormInput>> RemoveRange(IEnumerable<FormInput> formInputs)
        {
            foreach(var formInput in formInputs)
            {
                await this.Remove(formInput);
            }
            return formInputs;
        }
    }
}