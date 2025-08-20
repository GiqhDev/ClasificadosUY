using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClasificadosUY.Models
{
  public class Destacado
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? IdDestacado { get; set; }
    
    public int? IdAnuncio { get; set; }
    public Anuncio Anuncio { get; set; } = new Anuncio();

    [Required]
    public string Tipo { get; set; } // Repost | Destacado | Premium

    [DataType(DataType.DateTime)]
    public DateTime FechaInicio { get; set; } = DateTime.Now;

    [DataType(DataType.DateTime)]
    public DateTime FechaFin { get; set; }
  }
}
