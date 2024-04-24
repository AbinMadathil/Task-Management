using System.ComponentModel.DataAnnotations;

namespace TaskMgmt.Api.DTO
{
    public class EmailDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
    }
}
