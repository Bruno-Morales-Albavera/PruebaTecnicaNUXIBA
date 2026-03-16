using Microsoft.EntityFrameworkCore;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class CsvRepository
    {
        private readonly CcenterRiaContext _context;

        public CsvRepository(CcenterRiaContext context)
        {
            _context = context;
        }

        public async Task<List<LoginCsvRow>> GetCsvDataAsync()
        {
            var users = await _context.CcUsers.ToListAsync();
            var areas = await _context.CcAreas.ToListAsync();
            var logs = await _context.Ccloglogins
                                      .OrderBy(l => l.User_id)
                                      .ThenBy(l => l.Fecha)
                                      .ToListAsync();

            // Calcular total de segundos por usuario
            var secondsPerUser = new Dictionary<int, long>();

            foreach (var userLogs in logs.GroupBy(l => l.User_id))
            {
                long totalSeconds = 0;
                DateTime? loginTime = null;

                foreach (var log in userLogs.OrderBy(l => l.Fecha))
                {
                    if (log.TipoMov == 1)
                    {
                        loginTime = log.Fecha;
                    }
                    else if (log.TipoMov == 0 && loginTime.HasValue)
                    {
                        var diff = (log.Fecha - loginTime.Value).TotalSeconds;
                        if (diff > 0) totalSeconds += (long)diff;
                        loginTime = null;
                    }
                }

                secondsPerUser[userLogs.Key] = totalSeconds;
            }

            // Construir filas del CSV
            var result = new List<LoginCsvRow>();

            foreach (var user in users.Where(u => secondsPerUser.ContainsKey(u.User_id)))
            {
                var area = areas.FirstOrDefault(a => a.IDArea == user.IDArea);
                var secs = secondsPerUser[user.User_id];

                var days = secs / 86400;
                var hours = (secs % 86400) / 3600;
                var minutes = (secs % 3600) / 60;
                var seconds = secs % 60;

                result.Add(new LoginCsvRow
                {
                    Login = user.Login,
                    NombreCompleto = $"{user.Nombres} {user.ApellidoPaterno} {user.ApellidoMaterno}".Trim(),
                    Area = area?.AreaName ?? "Sin área",
                    TotalHorasTrabajadas = $"{days}d {hours}h {minutes}m {seconds}s"
                });
            }

            return result;
        }
    }
}
