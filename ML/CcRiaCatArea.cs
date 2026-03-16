using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    [Table("ccRIACat_Areas")]
    public class CcRiaCatArea
    {
        [Key]
        public int IDArea { get; set; }

        [Required]
        public string AreaName { get; set; } = string.Empty;

        public int StatusArea { get; set; }

        public DateTime? CreateDate { get; set; }
    }
}
