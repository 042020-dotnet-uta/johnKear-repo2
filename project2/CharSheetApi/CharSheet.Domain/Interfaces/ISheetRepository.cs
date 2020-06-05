using System.Collections.Generic;
using System.Threading.Tasks;

namespace CharSheet.Domain.Interfaces
{
    public interface ISheetRepository: IRepository<Sheet>
    {
        Task<IEnumerable<FormInputGroup>> GetFormInputGroups(object id);
    }
}