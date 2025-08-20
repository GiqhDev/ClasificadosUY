using ClasificadosUY.Data;
using ClasificadosUY.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ClasificadosUY.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly ClasificadosDbContext _context;

    public HomeController(ILogger<HomeController> logger, ClasificadosDbContext context)
    {
      _logger = logger;
      _context = context;
    }

    [HttpGet]
    public IActionResult Index(string search, int? IdCategoria, int? IdSubcategoria)
    {
      var anuncios = _context.Anuncios
          .Include(a => a.Categoria)
          .Include(a => a.Subcategoria)
          .Include(a => a.Destacado)
          .AsQueryable();

      // Filtrar por palabra clave
      if (!string.IsNullOrEmpty(search))
        anuncios = anuncios.Where(a => a.Titulo.Contains(search) || a.Descripcion.Contains(search));

      // Filtrar por categoría
      if (IdCategoria.HasValue)
        anuncios = anuncios.Where(a => a.IdCategoria == IdCategoria.Value);

      // Filtrar por subcategoría
      if (IdSubcategoria.HasValue)
        anuncios = anuncios.Where(a => a.IdSubcategoria == IdSubcategoria.Value);

      // Ordenar destacados primero
      anuncios = anuncios.OrderByDescending(a => a.Destacado != null && a.Destacado.FechaFin > DateTime.UtcNow)
                         .ThenByDescending(a => a.FechaPublicacion);

      // Pasar categorías y subcategorías al viewbag para filtros
      ViewBag.Categorias = _context.Categorias.Include(c => c.Subcategorias).ToList();
      ViewBag.Subcategorias = IdCategoria.HasValue 
          ? _context.Subcategorias.Where(s => s.Categoria.IdCategoria == IdCategoria.Value).ToList() 
          : _context.Subcategorias.ToList();

      return View(anuncios.ToList());
    }
  

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
