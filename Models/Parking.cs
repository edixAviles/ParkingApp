using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrugerApp.Models
{
    public class Parking
    {
        [Key]
        [Display(Name = "Código")]
        [Required(ErrorMessage = "El campo \"Código\" es requerido")]
        public long Id { get; set; }

        [Display(Name = "Fecha/Hora de Entrada")]
        [Required(ErrorMessage = "El campo \"Fecha/Hora de Entrada\" es requerido")]
        public DateTime EntryDate { get; set; }

        [Display(Name = "Fecha/Hora de Salida")]
        public DateTime? ExitDate { get; set; }

        [Display(Name = "Valor")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? ParkingValue { get; set; }

        [Display(Name = "Observación")]
        [Required(ErrorMessage = "El campo \"Observación\" es requerido")]
        public string Observation { get; set; }

        [Display(Name = "Vehiculo")]
        [Required(ErrorMessage = "El campo \"Vehiculo\" es requerido")]
        public long VehicleId { get; set; }

        [Display(Name = "Vehiculo")]
        [ForeignKey("VehicleId")]
        public Vehicle VehicleParked { get; set; }

        [NotMapped]
        public string FullNameOwnerVehicle
        {
            get
            {
                return VehicleParked.Owner.FullName + " - " + VehicleParked.Mark;
            }
        }
    }
}
