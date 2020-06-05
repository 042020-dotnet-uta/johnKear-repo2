using System.Collections.Generic;
using System.Threading.Tasks;

namespace CharSheet.Domain.Interfaces
{
    public interface IFormInputRepository: IRepository<FormInput>
    {
        Task<IEnumerable<FormInput>> RemoveRange(IEnumerable<FormInput> formInputs);
    }
}