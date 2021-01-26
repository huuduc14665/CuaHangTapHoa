using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CuaHangTapHoa.Models
{
    public class NhanVien : IdentityUser
    {
        [Display(Name ="Tên nhân viên")]
        public string TenNV { get; set; }
        [Display(Name = "Số điện thoại")]
        public string SoDT { get; set; }
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [NotMapped]
        [Display(Name ="Là quản lí")]
        public bool laQuanLi { get; set; }
    }
}
