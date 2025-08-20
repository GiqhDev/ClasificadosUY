using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClasificadosUY.Models
{
  public class Usuario
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdUsuario { get; set; }

    [Required, StringLength(50)]
    public string NombreCompleto { get; set; }

    [Required, StringLength(50)]
    public string Correo { get; set; }

    [Required, StringLength(150)]
    public string Clave { get; set; }

    [Required, StringLength(15)]
    public string Telefono { get; set; }

    [Required, StringLength(250)]
    public string Direccion { get; set; }
    [Required]
    public string Departamento { get; set; }

    public DateTime FechaRegistro { get; set; }

    public string Privacidad { get; set; }
    public ICollection<Anuncio> Anuncios { get; set; }
  }
}
