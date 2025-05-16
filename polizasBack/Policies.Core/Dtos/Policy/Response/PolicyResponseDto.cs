using Policies.Core.Enums;

namespace Policies.Core.Dtos.Policy.Response
{
    /// <summary>
    /// DTO para la respuesta detallada de una póliza
    /// </summary>
    public class PolicyResponseDto
    {
        /// <summary>
        /// Identificador único de la póliza
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Número de póliza generado automáticamente
        /// </summary>
        public string PolicyNumber { get; set; }

        /// <summary>
        /// Tipo de póliza (Vida, Auto, Hogar, Salud)
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Fecha de inicio de la póliza
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Fecha de fin de la póliza (calculada automáticamente)
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Monto de la prima
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Estado actual de la póliza
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Información del cliente asociado
        /// </summary>
        public ClientResponseDto Client { get; set; }
    }
}