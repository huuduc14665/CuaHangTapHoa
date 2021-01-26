using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CuaHangTapHoa.Models
{
    public class SanPham
    {
        [Key]
        public int MaSP { get; set; }
        [Display(Name ="Tên")]
        public string TenSP { get; set; }
        [Display(Name ="Giá")]
        public double Gia { get; set; }
        public int MaMH { get; set; }
        
        public int MaTag { get; set; }
        public int MaNCC { get; set; }
        [NotMapped]
        [Display(Name ="Số lượng")]
        public int SoLuong { get; set; }
        public string Hinh { get; set; }
        [Display(Name ="Mô tả sản phẩm")]
        public string MoTa { get; set; }
        [Display(Name ="Trạng thái")]
        public bool TrangThai { get; set; }

        [ForeignKey("MaMH")]
        [Display(Name = "Mặt hàng")]
        public virtual MatHang MatHang { get; set; }
        [ForeignKey("MaTag")]
        public virtual Tag Tag { get; set; }
        [ForeignKey("MaNCC")]
        [Display(Name = "Nhà cung cấp")]
        public virtual NhaCungCap NhaCungCap { get; set; }

    }
}
