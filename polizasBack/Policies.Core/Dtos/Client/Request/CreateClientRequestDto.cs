using System.ComponentModel.DataAnnotations;

namespace Policies.Core.Dtos.Policy.Request
{
    public class CreateClientRequestDto
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El apellido paterno es requerido")]
        [StringLength(100)]
        public string FirstLastName { get; set; }

        [Required(ErrorMessage = "El apellido materno es requerido")]
        [StringLength(100)]
        public string SecondLastName { get; set; }

        [Required(ErrorMessage = "La edad es requerida")]
        [Range(18, 99, ErrorMessage = "La edad debe ser mayor a 18 años")]
        public int Age { get; set; }

        [Required(ErrorMessage = "El país de nacimiento es requerido")]
        public string BirthCountry { get; set; }

        [Required(ErrorMessage = "El género es requerido")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "El correo electrónico es requerido")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El teléfono es requerido")]
        [Phone(ErrorMessage = "El formato del teléfono no es válido")]
        public string Phone { get; set; }
    }
}