using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrugerApp.Models
{
    public class VehicleType
    {
        [Key]
        [Display(Name = "Código")]
        [Required(ErrorMessage = "El campo \"Código\" es requerido")]
        public long Id { get; set; }

        [Display(Name = "Tipo (Camión, motocicleta)")]
        [Required(ErrorMessage = "El campo \"Tipo\" es requerido")]
        public string Name { get; set; }

        [Display(Name = "Valor por hora")]
        [Column(TypeName = "decimal(18,2)")]
        [Required(ErrorMessage = "El campo \"Valor por hora\" es requerido")]
        public decimal ValuePerHour { get; set; }
    }
}
