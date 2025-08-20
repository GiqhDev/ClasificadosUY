using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClasificadosUY.Models
{
  public class Anuncio
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdAnuncio { get; set; }

    [Required]
    public string? Titulo { get; set; }

    public string? Descripcion { get; set; }

    [DataType(DataType.Currency)]
    public decimal Precio { get; set; }

    public DateTime FechaPublicacion { get; set; }

    // Hasta 5 imágenes en Base64
    public string? Imagen1 { get; set; }
    public string? Imagen2 { get; set; }
    public string? Imagen3 { get; set; }
    public string? Imagen4 { get; set; }
    public string? Imagen5 { get; set; }

    public string Estado { get; set; } = "Activo";


    // Relaciones   
    public int? IdUsuario { get; set; }
    public Usuario Usuario { get; set; }

    public int? IdCategoria { get; set; }

    public Categoria Categoria { get; set; }

    public int? IdSubcategoria { get; set; }

    public Subcategoria Subcategoria { get; set; }
    public Destacado Destacado { get; set; }
  }
}
