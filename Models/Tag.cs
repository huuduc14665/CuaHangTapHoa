using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CuaHangTapHoa.Models
{
    public class Tag
    {
        [Key]
        public int MaTag { get; set; }
        [Required]
        [Display(Name ="Tên")]
        public string TenTag { get; set; }
    }
}
