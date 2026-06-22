namespace ClasificadosUY.ViewModels
{
  public class AnuncioVM
  {    
    public string Titulo { get; set; }

    public string? Descripcion { get; set; }
 
    public decimal Precio { get; set; }

    public int? IdUsuario { get; set; }  
    
    public string NombreUsuario { get; set; } = string.Empty;

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public string? Direccion { get; set; }

    public string? Departamento { get; set; }

    public int? IdAnuncio { get; set; }

    public int? IdCategoria { get; set; } 
    
    public string NombreCategoria { get; set; } = string.Empty;

    public int? IdSubcategoria { get; set; }  
    
    public string NombreSubcategoria { get; set; } = string.Empty;

    //public Destacado Destacado { get; set; } 

    public DateTime FechaPublicacion { get; set; } = DateTime.UtcNow;  
    
    public string? Imagen1 { get; set; }

    public string? Imagen2 { get; set; }
    public string? Imagen3 { get; set; }
    public string? Imagen4 { get; set; }
    public string? Imagen5 { get; set; }

    public string? Estado { get; set; }

  }
}
