using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CuaHangTapHoa.Models
{
    public class NhaCungCap
    {
        [Key]
        public int MaNCC { get; set; }
        [Required]
        [Display(Name ="Tên nhà cung cấp")]
        public string TenNCC { get; set; }
        [Required]
        [Display(Name = "Số điện thoại")]

        public string SoDienThoai { get; set; }
        [Required]
        [Display(Name = "Địa chỉ")]

        public string DiaChi { get; set; }
        [Display(Name = "Trạng thái")]

        public bool TrangThai { get; set; }


    }
}
