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
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los registros: {ex.Message}");
            }
        }

        public async Task<CcLogLogin?> GetByIdAsync(int id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el registro: {ex.Message}");
            }
        }

        public async Task<(bool success, string message, CcLogLogin? data)>
            CreateAsync(CcLogLogin login)
        {
            try
            {
                if (login.TipoMov != 0 && login.TipoMov != 1)
                    return (false, "TipoMov debe ser 0 (logout) o 1 (login).", null);

                if (login.Fecha == default)
                    return (false, "La fecha es requerida.", null);

                var result = await _repository.CreateAsync(login);
                return (true, "Registro creado correctamente.", result);
            }
            catch (Exception ex)
            {
                return (false, $"Error al crear el registro: {ex.Message}", null);
            }
        }

        public async Task<(bool success, string message)>
            UpdateAsync(int id, CcLogLogin login)
        {
            try
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
            catch (Exception ex)
            {
                return (false, $"Error al actualizar el registro: {ex.Message}");
            }
        }

        public async Task<(bool success, string message)> DeleteAsync(int id)
        {
            try
            {
                var deleted = await _repository.DeleteAsync(id);
                return deleted
                    ? (true, "Registro eliminado correctamente.")
                    : (false, $"No existe registro con Id {id}.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al eliminar el registro: {ex.Message}");
            }
        }
    }
}
