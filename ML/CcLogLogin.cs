using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ML
{
    [Table("ccloglogin")]
    public class CcLogLogin
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int User_id { get; set; }

        [Required]
        public int Extension { get; set; }

        [Required]
        public int TipoMov { get; set; }   // 1 = login, 0 = logout

        [Required]
        public DateTime Fecha { get; set; }
    }
}
