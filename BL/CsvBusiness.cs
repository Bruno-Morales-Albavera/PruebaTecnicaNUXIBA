using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class CsvBusiness
    {
        private readonly CsvRepository _repository;

        public CsvBusiness(CsvRepository repository)
        {
            _repository = repository;
        }

        public async Task<byte[]> GenerateCsvAsync()
        {
            var data = await _repository.GetCsvDataAsync();

            var lines = new List<string>
            {
                "Login,Nombre Completo,Area,Total Horas Trabajadas"
            };

            foreach (var row in data)
            {
                lines.Add($"\"{row.Login}\"," +
                          $"\"{row.NombreCompleto}\"," +
                          $"\"{row.Area}\"," +
                          $"\"{row.TotalHorasTrabajadas}\"");
            }

            return System.Text.Encoding.UTF8.GetBytes(string.Join("\n", lines));
        }
    }
}
