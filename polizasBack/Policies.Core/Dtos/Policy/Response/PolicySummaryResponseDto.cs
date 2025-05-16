using Policies.Core.Enums;

namespace Policies.Core.Dtos.Policy.Response
{
    public class PolicySummaryResponseDto
    {
        public int Id { get; set; }
        public string PolicyNumber { get; set; }
        public string ClientName { get; set; }
        public decimal Premium { get; set; }
        public Policiestatus Status { get; set; }
    }
}