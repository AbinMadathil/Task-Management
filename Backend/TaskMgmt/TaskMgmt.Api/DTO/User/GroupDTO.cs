using System.ComponentModel.DataAnnotations;

namespace TaskMgmt.Api.DTO.User
{
    public class GroupDTO
    {
        [Required(ErrorMessage = "GroupName is required.")]
        [StringLength(20, ErrorMessage = "GroupName must between 3-20 characters", MinimumLength = 3)]
        public string GroupName { get; set; }
    }
}
