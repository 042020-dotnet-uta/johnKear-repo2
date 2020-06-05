using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using CharSheet.Domain;
using CharSheet.Data;

namespace CharSheet.Api.Services
{
    public partial interface IBusinessService
    {
        
    }

    public partial class BusinessService : IBusinessService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ILogger<BusinessService> _logger;

        public BusinessService(ILogger<BusinessService> logger, CharSheetContext context)
        {
            this._logger = logger;
            this._unitOfWork = new UnitOfWork(context);
        }

        #region Helpers
        public async Task<User> AuthenticateUser(object id)
        {
            var user = await _unitOfWork.UserRepository.Find(id);
            if (user == null)
                throw new InvalidOperationException("User not found.");
            else
                return user;
        }
        #endregion
    }
}