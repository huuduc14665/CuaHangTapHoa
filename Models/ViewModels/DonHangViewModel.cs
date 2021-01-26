using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CuaHangTapHoa.Models.ViewModels
{
    public class DonHangViewModel
    {
        public List<DonHang> DonHangs { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
