using System.ComponentModel.DataAnnotations;

namespace TaskMgmt.Api.DTO
{
    public class SignUpDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, ErrorMessage = "Password must be between 6-20 characters ", MinimumLength = 6)]

        public string Password { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(20, ErrorMessage = "Name must between 3-20 characters", MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "GroupName is required.")]
        [StringLength(20, ErrorMessage = "GroupName must between 3-20 characters", MinimumLength = 3)]
        public string GroupName { get; set; }

        public string? ReferralCode { get; set; }
    }
}