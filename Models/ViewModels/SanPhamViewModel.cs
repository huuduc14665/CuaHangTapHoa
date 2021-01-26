using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CuaHangTapHoa.Models.ViewModels
{
    public class SanPhamViewModel
    {
        public SanPham SanPham { get; set; }
        [Display(Name ="Mặt hàng")]
        public IEnumerable<MatHang> MatHangs { get; set; }
        [Display(Name = "Tag")]
        public IEnumerable<Tag> Tags { get; set; }
        [Display(Name = "Nhà cung cấp")]
        public IEnumerable<NhaCungCap> NhaCungCaps { get; set; }
    }
}
