using DL;
using ML;

namespace BL
{
    public class LoginBusiness
    {
        private readonly LoginRepository _repository;

        public LoginBusiness(LoginRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CcLogLogin>> GetAllAsync()
            => await _repository.GetAllAsync();

        public async Task<CcLogLogin?> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task<(bool success, string message, CcLogLogin? data)>
            CreateAsync(CcLogLogin login)
        {
            if (login.TipoMov != 0 && login.TipoMov != 1)
                return (false, "TipoMov debe ser 0 (logout) o 1 (login).", null);

            if (login.Fecha == default)
                return (false, "La fecha es requerida.", null);

            var result = await _repository.CreateAsync(login);
            return (true, "Registro creado correctamente.", result);
        }

        public async Task<(bool success, string message)>
            UpdateAsync(int id, CcLogLogin login)
        {
            if (id != login.Id)
                return (false, "El ID no coincide con el registro.");

            if (login.TipoMov != 0 && login.TipoMov != 1)
                return (false, "TipoMov debe ser 0 (logout) o 1 (login).");

            var exists = await _repository.GetByIdAsync(id);
            if (exists == null)
                return (false, $"No existe registro con Id {id}.");

            var updated = await _repository.UpdateAsync(login);
            return updated
                ? (true, "Registro actualizado correctamente.")
                : (false, "No se pudo actualizar el registro.");
        }

        public async Task<(bool success, string message)> DeleteAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            return deleted
                ? (true, "Registro eliminado correctamente.")
                : (false, $"No existe registro con Id {id}.");
        }
    }
}
