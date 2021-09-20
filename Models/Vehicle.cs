using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrugerApp.Models
{
    public class Vehicle
    {
        [Key]
        [Display(Name = "Código")]
        [Required(ErrorMessage = "El campo \"Código\" es requerido")]
        public long Id { get; set; }

        [Display(Name = "Marca")]
        [Required(ErrorMessage = "El campo \"Marca\" es requerido")]
        public string Mark { get; set; }

        [Display(Name = "Placa")]
        [Required(ErrorMessage = "El campo \"Placa\" es requerido")]
        public string Plate { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "El campo \"Tipo\" es requerido")]
        public long VehicleTypeId { get; set; }

        [Display(Name = "Propietario")]
        [Required(ErrorMessage = "El campo \"Propietario\" es requerido")]
        public long CustomerId { get; set; }

        [Display(Name = "Tipo")]
        [ForeignKey("VehicleTypeId")]
        public VehicleType Type { get; set; }
        [Display(Name = "Propietario")]
        [ForeignKey("CustomerId")]
        public Customer Owner { get; set; }
    }
}
