using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMgmt.Console.Dtos.Group
{
    public class GroupRequestDTO
    {
        [Required(ErrorMessage = "GroupName is required.")]
        [StringLength(20, ErrorMessage = "GroupName must between 3-20 characters", MinimumLength = 3)]
        public string GroupName { get; set; }
    }
}
