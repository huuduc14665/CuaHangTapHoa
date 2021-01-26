using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CuaHangTapHoa.Models.ViewModels
{
    public class DSSanPhamViewModel
    {
        public IEnumerable<SanPham> SanPhams { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
