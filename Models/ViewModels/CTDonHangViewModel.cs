using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CuaHangTapHoa.Models.ViewModels
{
    public class CTDonHangViewModel
    {
        public DonHang DonHang { get; set; }
        public List<NhanVien> NhanViens { get; set; }
        public List<SanPham> SanPhams { get; set; }
    }
}
