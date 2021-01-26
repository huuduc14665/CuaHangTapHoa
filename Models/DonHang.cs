using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CuaHangTapHoa.Models
{
    public class DonHang
    {
        [Key]
        public int MaDH { get; set; }
        [Display(Name ="Nhân viên phụ trách")]
        public string MaNV { get; set; }
        [Display(Name ="Tên khách hàng")]
        public string TenKH { get; set; }
        [Display(Name = "Địa chỉ")]
        public string DiaChiKH { get; set; }
        [Display(Name = "Số điện thoại")]
        public string SoDienThoai { get; set; }
        [Display(Name = "Ngày lập đơn")]
        public DateTime NgayLapDH { get; set; }
        [Display(Name = "Tổng tiền")]
        public double TongTien { get; set; }
        [Display(Name = "Ngày nhận hàng")]
        public DateTime NgayNhanHang { get; set; }
        [NotMapped]
        [Display(Name = "Giờ nhận hàng")]
        public DateTime GioNhanHang { get; set; }
        [Display(Name = "Trạng thái")]
        public bool TrangThai { get; set; }

        [ForeignKey("MaNV")]
        public virtual NhanVien NhanVien { get; set; }

    }
}
