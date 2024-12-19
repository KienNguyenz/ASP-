namespace W2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.IO;
    using System.Web.UI.WebControls;
    using System.Xml.Linq;

    [Table("User")]
    public partial class User
    {
        public int id { get; set; }

        [Required(ErrorMessage ="Tên tài khoản không được để trống")]
        [StringLength(30)]
        [DisplayName("Tài khoản")]
        public string TaiKhoan { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(20)]
        [DisplayName("Mật khẩu")]
        //[Display( = "*")]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(50)]
        [DisplayName("Họ và tên")]
        public string HoTen { get; set; }

        [StringLength(100)]
        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }
        [DisplayName("Giới tính")]
        public int? GioiTinh { get; set; }
    }
}
