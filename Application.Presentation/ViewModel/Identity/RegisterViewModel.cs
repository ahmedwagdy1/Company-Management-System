using System.ComponentModel.DataAnnotations;

namespace Application.Presentation.ViewModel.Identity
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="UserName Can Not Be Null!")]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "FirstName Can Not Be Null!")]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName Can Not Be Null!")]
        [MaxLength(50)]
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        public bool IsAgreed { get; set; }
    }
}
