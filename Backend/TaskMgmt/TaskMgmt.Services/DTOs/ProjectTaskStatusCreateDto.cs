using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMgmt.Services.DTOs
{
    public class ProjectTaskStatusCreateDto
    {
        [MaxLength(20, ErrorMessage = "Status text must be less than 20")]
        public string StatusText { get; set; } = null!;
        [RegularExpression("^#([0-9A-F]{6})$", ErrorMessage = "Invalid hexadecimal color format")]
        public string StatusColor { get; set; } = null!;
    }
}
