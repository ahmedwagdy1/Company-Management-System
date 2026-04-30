using System.ComponentModel.DataAnnotations;

namespace Application.Presentation.ViewModel.Identity
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email can not be empty!!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
