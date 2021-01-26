using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CuaHangTapHoa.Models
{
    public class LoaiMatHang
    {
        [Key]
        public int MaLMH { get; set; }
        [Required]
        [DisplayName("Tên")]
        public string TenLMH { get; set; }
        [DisplayName("Mô tả")]
        public string MoTa { get; set; }
        [DisplayName("Trạng thái")]
        public bool TrangThai { get; set; }



    }
}
