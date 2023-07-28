using System.ComponentModel.DataAnnotations;

namespace DataAccess.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Username or Email")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Username or email can not be empty!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password can not be empty")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public LoginVM(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public LoginVM()
        {
        }
    }
}
