using System.ComponentModel.DataAnnotations;
using Policies.Core.Enums;

namespace Policies.Core.Dtos.Policy.Request
{
    public class UpdatePolicyRequestDto : IPolicyBaseData
    {
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Amount { get; set; }
        public int ClientId { get; set; }
    }
    
}