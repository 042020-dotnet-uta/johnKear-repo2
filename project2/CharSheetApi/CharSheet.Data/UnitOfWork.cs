using System;
using System.Threading.Tasks;
using CharSheet.Data.Repositories;
using CharSheet.Domain.Interfaces;

namespace CharSheet.Data
{
    public class UnitOfWork : IDisposable
    {
        #region Repository Properties
        private readonly CharSheetContext _context;

        private IFormInputGroupRepository _formInputGroupRepository;
        public IFormInputGroupRepository FormInputGroupRepository
        {
            get
            {
                if (this._formInputGroupRepository == null)
                {
                    this._formInputGroupRepository = new FormInputGroupRepository(this._context);
                }
                return _formInputGroupRepository;
            }
        }

        private IFormInputRepository _formInputRepository;
        public IFormInputRepository FormInputRepository
        {
            get
            {
                if (this._formInputRepository == null)
                {
                    this._formInputRepository = new FormInputRepository(this._context);
                }
                return _formInputRepository;
            }
        }

        private IFormTemplateRepository _formTemplateRepository;
        public IFormTemplateRepository FormTemplateRepository
        {
            get
            {
                if (this._formTemplateRepository == null)
                {
                    this._formTemplateRepository = new FormTemplateRepository(this._context);
                }
                return this._formTemplateRepository;
            }
        }

        private ILoginRepository _loginRepository;
        public ILoginRepository LoginRepository
        {
            get
            {
                if (this._loginRepository == null)
                {
                    this._loginRepository = new LoginRepository(this._context);
                }
                return this._loginRepository;
            }
        }

        private ISheetRepository _sheetRepository;
        public ISheetRepository SheetRepository
        {
            get
            {
                if (this._sheetRepository == null)
                {
                    this._sheetRepository = new SheetRepository(this._context);
                }
                return this._sheetRepository;
            }
        }

        private ITemplateRepository _templateRepository;
        public ITemplateRepository TemplateRepository
        {
            get
            {
                if (this._templateRepository == null)
                {
                    this._templateRepository = new TemplateRepository(this._context);
                }
                return this._templateRepository;
            }
        }

        private IUserRepository _userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                if (this._userRepository == null)
                {
                    this._userRepository = new UserRepository(this._context);
                }
                return _userRepository;
            }
        }
        #endregion

        #region Constructor
        public UnitOfWork(CharSheetContext context)
        {
            this._context = context;
        }
        #endregion

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        #region IDisposable
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                    _context.Dispose();            
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}