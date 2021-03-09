using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.EF
{
    [Table("User")]
    public class User
    {
        public int Id { get; set; }

        [Display(Name = "Tên Đăng Nhập")]
        [Required(ErrorMessage = ("Hãy nhập tên đăng nhập"))]
        public string UserName { get; set; }

        [Display(Name = "Mật Khẩu")]
        [Required(ErrorMessage = ("Hãy nhập mật khẩu"))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = ("Bạn điền sai mật khẩu"))]
        public string ConfirmPassword { get; set; }
       
        public string FullName { get; set; }
       
        public string Address { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Hãy nhập email")]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = "Hãy nhập email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool Status { get; set; }
    }
}
