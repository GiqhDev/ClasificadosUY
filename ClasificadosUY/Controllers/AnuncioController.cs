using ClasificadosUY.Data;
using ClasificadosUY.Models;
using ClasificadosUY.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ClasificadosUY.Controllers
{

  public class AnuncioController : Controller
  {
    private readonly ClasificadosDbContext _context;

    public AnuncioController(ClasificadosDbContext context)
    {
      _context = context;
    }

    [Authorize]
    [HttpGet]
    public IActionResult Publicar()
    {
      ViewBag.Categorias = _context.Categorias.ToList();
      ViewBag.Subcategorias = _context.Subcategorias.ToList();
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Publicar(AnuncioVM anuncioVM, List<IFormFile> Imagenes)
    {
      var anuncio = new Anuncio();

      if (ModelState.IsValid)
      {
        anuncio.Titulo = anuncioVM.Titulo;
        anuncio.Descripcion = anuncioVM.Descripcion;
        anuncio.Precio = anuncioVM.Precio;
        anuncio.IdUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!); // Obtener el Id del usuario autenticado
        anuncio.IdCategoria = anuncioVM.IdCategoria;
        //anuncio.Categoria.Nombre= _context.Categorias
        //    .Where(c => c.IdCategoria == anuncioVM.IdCategoria)
        //    .Select(c => c.Nombre)
        //    .FirstOrDefault() ?? string.Empty; // Obtener el nombre de la categoría
        anuncio.IdSubcategoria = anuncioVM.IdSubcategoria;
        //anuncio.Subcategoria.Nombre = _context.Subcategorias
        //    .Where(s => s.IdSubcategoria == anuncioVM.IdSubcategoria)
        //    .Select(s => s.Nombre)
        //    .FirstOrDefault() ?? string.Empty; // Obtener el nombre de la subcategoría
        anuncio.FechaPublicacion = anuncioVM.FechaPublicacion;

        // Procesar imágenes
        for (int i = 0; i < Imagenes.Count && i < 5; i++)
        {
          using var ms = new MemoryStream();
          await Imagenes[i].CopyToAsync(ms);
          var base64 = Convert.ToBase64String(ms.ToArray());

          switch (i)
          {
            case 0: anuncio.Imagen1 = base64; break;
            case 1: anuncio.Imagen2 = base64; break;
            case 2: anuncio.Imagen3 = base64; break;
            case 3: anuncio.Imagen4 = base64; break;
            case 4: anuncio.Imagen5 = base64; break;
          }
        }

        await _context.AddAsync(anuncio);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index", "Home");
      }

      ViewBag.Categorias = _context.Categorias.ToList();
      ViewBag.Subcategorias = _context.Subcategorias.ToList();
      return View();
    }

    [HttpGet]
    public async Task<IActionResult> Detalle(int id)
    {
      var anuncio = await _context.Anuncios
            .Include(a => a.Categoria)
            .Include(a => a.Subcategoria)
            .Include(a => a.Usuario)
            .FirstOrDefaultAsync(m => m.IdAnuncio == id);

      if (anuncio == null)
      {
        return NotFound();
      }

      return View(anuncio);
    }
    [Authorize]
    public async Task<IActionResult> Editar(int id)
    {
      var anuncio = await _context.Anuncios.FindAsync(id);
      if (anuncio == null) return NotFound();

      return View(anuncio);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Editar(EditarAnuncioVM anuncio, List<IFormFile> imagenes)
    {
      if (ModelState.IsValid)
      {
        var anuncioAEditar = await _context.Anuncios.FindAsync(anuncio.IdAnuncio);

        anuncioAEditar.Titulo = anuncio.Titulo;
        anuncioAEditar.Descripcion = anuncio.Descripcion;
        anuncioAEditar.Precio = anuncio.Precio;
        anuncioAEditar.Estado = anuncio.Estado;

        foreach (var item in imagenes)
        {
          using var ms = new MemoryStream();
          await item.CopyToAsync(ms);
          var base64 = Convert.ToBase64String(ms.ToArray());

          for (int i = 0; i < 5; i++)
          {     
            bool salir = false;
            switch (i)
            {
              case 0:
                if (string.IsNullOrEmpty(anuncioAEditar.Imagen1))
                {
                  anuncioAEditar.Imagen1 = base64;
                  salir = true;
                }
                else
                  continue;

                break;
              case 1:
                if (string.IsNullOrEmpty(anuncioAEditar.Imagen2))
                {
                  anuncioAEditar.Imagen2 = base64;
                  salir = true;
                }
                else
                  continue;
                break;
              case 2:
                if (string.IsNullOrEmpty(anuncioAEditar.Imagen3))
                {
                  anuncioAEditar.Imagen3 = base64;
                  salir = true;
                }
                else
                  continue;

                break;
              case 3:
                if (string.IsNullOrEmpty(anuncioAEditar.Imagen4))
                {
                  anuncioAEditar.Imagen4 = base64;
                  salir = true;
                }
                else
                  continue;

                break;
              case 4:
                if (string.IsNullOrEmpty(anuncioAEditar.Imagen5))
                {
                  anuncioAEditar.Imagen5 = base64;
                  salir = true;
                }
                else
                  continue;
                break;
            }

            if (salir) break; // Salir del bucle si ya se ha asignado una imagen
          }
        }
        _context.Anuncios.Update(anuncioAEditar);
        await _context.SaveChangesAsync();
        return RedirectToAction("Cuenta", "Account");
      }
      return View(anuncio);
    }

    [HttpPost, ActionName("Eliminar")]
    [Authorize]
    public async Task<IActionResult> EliminarConfirmado(int id)
    {
      var anuncio = await _context.Anuncios.FindAsync(id);
      if (anuncio == null) return NotFound();

      _context.Anuncios.Remove(anuncio);
      await _context.SaveChangesAsync();
      return RedirectToAction("Cuenta", "Account");
    }

    [HttpGet]
    [Authorize]
    public IActionResult Eliminar(int id)
    {
      var anuncio = _context.Anuncios.Find(id);
      if (anuncio == null) return NotFound();

      return View(anuncio);
    }
  }
}
