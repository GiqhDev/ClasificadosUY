namespace ClasificadosUY.ViewModels
{
  public class AnuncioVM
  {
    public int IdAnuncio { get; set; }
    
    public string Titulo { get; set; }

    public string Descripcion { get; set; }
 
    public decimal Precio { get; set; }

    public int? IdUsuario { get; set; }    

    public int? IdCategoria { get; set; }    

    public int? IdSubcategoria { get; set; }    

    //public Destacado Destacado { get; set; } 

    public DateTime FechaPublicacion { get; set; }   

  }
}
