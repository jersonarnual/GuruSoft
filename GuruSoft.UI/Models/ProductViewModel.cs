using GuruSoft.Infraestructure.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuruSoft.UI.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Codigo del producto")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string ProductCode { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string ProductName { get; set; }
        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Description { get; set; }
        [Display(Name = "precio")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal Price { get; set; }
        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Quantity { get; set; }
        [Display(Name = "Total")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal Total { get; set; }
        public List<ProductDTO> ListProduct { get; set; }
    }
}

