using System.ComponentModel.DataAnnotations;
using Policies.Core.Enums;

namespace Policies.Core.Dtos.Policy.Request
{
    public class CreatePolicyRequestDto: IPolicyBaseData
    {
        public string PolicyNumber { get; set; }

        [Required(ErrorMessage = "El tipo de póliza es requerido")]
        public string Type { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es requerida")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "El id del cliente es requerida")]
        public int ClientId { get; set; }

        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "El monto debe ser mayor a 0")]
        public decimal Amount { get; set; }

    }

    public class NewPolicyRequestDto : CreateClientRequestDto
    {
        public string PolicyNumber { get; set; }

        [Required(ErrorMessage = "El tipo de póliza es requerido")]
        public string Type { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es requerida")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "El id del cliente es requerida")]
        public int ClientId { get; set; }

        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "El monto debe ser mayor a 0")]
        public decimal Amount { get; set; }
    }

    public interface IPolicyBaseData
{
    DateTime StartDate { get; set; }
    DateTime EndDate { get; set; }
    decimal Amount { get; set; }
}
}