using BL;
using ML;

namespace SL_API
{
    public class LoginService
    {
        private readonly LoginBusiness _business;

        public LoginService(LoginBusiness business)
        {
            _business = business;
        }

        public Task<List<CcLogLogin>> GetAllAsync()
            => _business.GetAllAsync();

        public Task<CcLogLogin?> GetByIdAsync(int id)
            => _business.GetByIdAsync(id);

        public Task<(bool success, string message, CcLogLogin? data)>
            CreateAsync(CcLogLogin login)
            => _business.CreateAsync(login);

        public Task<(bool success, string message)>
            UpdateAsync(int id, CcLogLogin login)
            => _business.UpdateAsync(id, login);

        public Task<(bool success, string message)>
            DeleteAsync(int id)
            => _business.DeleteAsync(id);
    }

    public class CsvService
    {
        private readonly CsvBusiness _business;

        public CsvService(CsvBusiness business)
        {
            _business = business;
        }

        public Task<byte[]> GenerateCsvAsync()
            => _business.GenerateCsvAsync();
    }
}
