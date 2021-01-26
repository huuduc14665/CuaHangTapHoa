using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CuaHangTapHoa.Models.ViewModels
{
    public class MatHangViewModel
    {
        public MatHang MatHang { get; set; }
        [Display(Name = "Loại mặt hàng")]
        public IEnumerable<LoaiMatHang> LoaiMatHang { get; set; }
    }
}
