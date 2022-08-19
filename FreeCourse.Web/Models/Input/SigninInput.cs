using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models.Input
{
    public class SigninInput
    {
        [Display(Name = "Email adresiniz")]
        public string   Email { get; set; }

        [Display(Name = "Şifreniz")]
        public string Password { get; set; }

        [Display(Name = "Beni hatırla")]
        public bool IsRemember { get; set; }
    }
}
