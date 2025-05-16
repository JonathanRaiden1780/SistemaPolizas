using Policies.Core.Enums;

namespace Policies.Core.Dtos.Policy.Response
{

    public class ClientResponseDto
    {
        /// <summary>
        /// Identificador único del cliente
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Identificador único del cliente
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identificador externo del cliente
        /// </summary>
        public string ExId { get; set; }
        
        /// <summary>
        /// Nombre del cliente
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Apellido paterno del cliente
        /// </summary>
        public string FirstLastName { get; set; }

        /// <summary>
        /// Apellido materno del cliente
        /// </summary>
        public string SecondLastName { get; set; }

        /// <summary>
        /// Edad del cliente
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// País de nacimiento del cliente
        /// </summary>
        public string BirthCountry { get; set; }

        /// <summary>
        /// Género del cliente
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Correo electrónico del cliente
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Teléfono del cliente
        /// </summary>
        public string Phone { get; set; }
    }
}