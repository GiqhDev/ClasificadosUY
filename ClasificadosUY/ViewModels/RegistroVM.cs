using ClasificadosUY.Models;

namespace ClasificadosUY.ViewModels
{
  public class RegistroVM
  {
    public string NombreCompleto { get; set; }

    public string Telefono { get; set; }

    public string Direccion { get; set; }

    public string Departamento { get; set; }

    public string Correo { get; set; }

    public string Clave { get; set; }

    public string ConfirmarClave { get; set; }

    public string Privacidad { get; set; }

    public ICollection<Anuncio> Anuncios { get; set; }
  }
}
