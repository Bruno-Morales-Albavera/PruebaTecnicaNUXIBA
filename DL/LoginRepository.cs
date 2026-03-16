using Microsoft.EntityFrameworkCore;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class LoginRepository
    {
        private readonly CcenterRiaContext _context;

        public LoginRepository(CcenterRiaContext context)
        {
            _context = context;
        }

        public async Task<List<CcLogLogin>> GetAllAsync()
            => await _context.Ccloglogins.ToListAsync();

        public async Task<CcLogLogin?> GetByIdAsync(int id)
            => await _context.Ccloglogins.FindAsync(id);

        public async Task<CcLogLogin> CreateAsync(CcLogLogin login)
        {
            _context.Ccloglogins.Add(login);
            await _context.SaveChangesAsync();
            return login;
        }

        public async Task<bool> UpdateAsync(CcLogLogin login)
        {
            var existing = await _context.Ccloglogins.FindAsync(login.Id);
            if (existing == null) return false;

            // Actualizar los campos manualmente
            existing.User_id = login.User_id;
            existing.Extension = login.Extension;
            existing.TipoMov = login.TipoMov;
            existing.Fecha = login.Fecha;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var login = await _context.Ccloglogins.FindAsync(id);
            if (login == null) return false;

            _context.Ccloglogins.Remove(login);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
