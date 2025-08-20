using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClasificadosUY.Models
{
  public class Categoria
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? IdCategoria { get; set; }

    [Required, StringLength(50)]
    public string? Nombre { get; set; }

    public ICollection<Subcategoria> Subcategorias { get; set; }

    public ICollection<Anuncio> Anuncios { get; set; }
  }
}
