using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    [Table("ccUsers")]
    public class CcUser
    {
        [Key]
        public int User_id { get; set; }

        [Required]
        public string Login { get; set; } = string.Empty;

        public string? Nombres { get; set; }

        public string? ApellidoPaterno { get; set; }

        public string? ApellidoMaterno { get; set; }

        public string? Password { get; set; }

        public int TipoUser_id { get; set; }

        public int Status { get; set; }

        public DateTime? fCreate { get; set; }

        public int IDArea { get; set; }

        public DateTime? LastLoginAttempt { get; set; }
    }
}
