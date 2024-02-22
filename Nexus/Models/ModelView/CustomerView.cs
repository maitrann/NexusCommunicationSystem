using System.ComponentModel.DataAnnotations;

namespace Nexus.Models.ModelView
{
    public class CustomerView
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Input This Field")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please Input This Field")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Please Input This Field")]
        [RegularExpression("[a-z0-9]+@gmail.com", ErrorMessage = "Please Input Correct Email,Example: examplemail@gmail.com")]
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public bool? Status { get; set; }

        //Other
        [Compare("Password", ErrorMessage = "Confirm Password doesn't match,please input again")]
        public string? confirmPass { get; set; }
    }
}
