using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CuaHangTapHoa.Models
{
    public class MatHang
    {
        [Key]
        public int MaMH { get; set; }
        [Required]
        [Display(Name ="Tên mặt hàng")]
        public string TenMH { get; set; }
        public int MaLMH { get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }
        [Display(Name = "Trạng thái")]
        public bool TrangThai { get; set; }
        [ForeignKey("MaLMH")]
        [Display(Name = "Loại mặt hàng")]
        public virtual LoaiMatHang LoaiMatHang { get; set; }
    }
}
