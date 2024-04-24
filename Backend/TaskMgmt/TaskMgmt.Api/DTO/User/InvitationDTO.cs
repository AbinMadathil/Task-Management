using System.ComponentModel.DataAnnotations;

namespace TaskMgmt.Api.DTO
{
    public class InvitationDTO
    {
        public string? GroupName { get; set; }
        public string? ReferralCode { get; set; }
    }
}