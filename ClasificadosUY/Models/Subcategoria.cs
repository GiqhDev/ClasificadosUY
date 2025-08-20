using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClasificadosUY.Models
{
  public class Subcategoria
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? IdSubcategoria { get; set; }

    [Required, StringLength(50)]
    public string? Nombre { get; set; }
   
    public Categoria Categoria { get; set; } = new Categoria();

    public ICollection<Anuncio> Anuncios { get; set; } = new List<Anuncio>();

  }
}
