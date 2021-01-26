using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CuaHangTapHoa.Models
{
    public class CT_DonHang
    {
        [Key]
        public int Ma { get; set; }

        public int MaDH { get; set; }
        
        public int MaSP { get; set; }

        public int SoLuong { get; set; }

        [ForeignKey("MaDH")]
        public virtual DonHang DonHang { get; set; }
        [ForeignKey("MaSP")]
        public virtual SanPham SanPham { get; set; }

    }
}
