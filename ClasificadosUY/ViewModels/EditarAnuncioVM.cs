namespace ClasificadosUY.ViewModels
{
  public class EditarAnuncioVM
  {
    public int? IdAnuncio { get; set; }  
    public string? Titulo { get; set; }
    public string? Descripcion { get; set; }
    public decimal Precio { get; set; }

    List<IFormFile>? Imagenes { get; set; } = new List<IFormFile>();

    public IFormFile? Imagen1 { get; set; }
    public IFormFile? Imagen2 { get; set; }
    public IFormFile? Imagen3 { get; set; }
    public IFormFile? Imagen4 { get; set; }
    public IFormFile? Imagen5 { get; set; }

    public string? Estado { get; set; }
  }
}
