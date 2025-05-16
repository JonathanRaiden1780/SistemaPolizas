using System.ComponentModel.DataAnnotations;
using Policies.Core.Enums;

namespace Policies.Core.Dtos.Policy.Request
{

    public class UpdateClientRequestDto : CreateClientRequestDto
    {
        public int ClientId { get; set; }
        public int? Id { get; set; }

    }
}