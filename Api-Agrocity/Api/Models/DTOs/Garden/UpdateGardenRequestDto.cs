using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTOs.Garden
{
  public class UpdateGardenRequestDto
  {

    [Required(ErrorMessage = "El ID del usuario es obligatorio.")]
    public int? UserId { get; set; }

    [Required(ErrorMessage = "El nombre del jardín es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre del jardín no puede superar los 100 caracteres.")]
    [RegularExpression(@"^[^\d]*$", ErrorMessage = "El nombre no debe contener números.")]
    public string? Name { get; set; }

    [StringLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres.")]
    [RegularExpression(@"^[^\d]*$", ErrorMessage = "La descripción no debe contener números.")]
    public string? Description { get; set; }


    [Required(ErrorMessage = "La fecha de creación es obligatoria.")]
    [DataType(DataType.Date)]
    public DateTime? CreatedAt { get; set; }

      public IFormFile? File { get; set; }
  }
}