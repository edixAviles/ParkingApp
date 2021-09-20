using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrugerApp.Models
{
    public class Customer
    {
        [Key]
        [Display(Name = "Código")]
        [Required(ErrorMessage = "El campo \"Código\" es requerido")]
        public long Id { get; set; }

        [Display(Name = "Identificación")]
        [Required(ErrorMessage = "El campo \"Identificación\" es requerido")]
        public string Dni { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo \"Nombre\" es requerido")]
        public string Name { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "El campo \"Apellido\" es requerido")]
        public string LastName { get; set; }

        [Display(Name = "Ciudad")]
        [Required(ErrorMessage = "El campo \"Ciudad\" es requerido")]
        public string City { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return Name + " " + LastName;
            }
        }
    }
}
