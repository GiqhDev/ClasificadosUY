using ClasificadosUY.Models;

namespace ClasificadosUY.ViewModels
{
  public class CuentaVM
  {
    public Usuario Usuario { get; set; }
    public List<Anuncio> Anuncios { get; set; } = new();
  }
}
