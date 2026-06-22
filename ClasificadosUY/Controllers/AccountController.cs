using ClasificadosUY.Data;
using ClasificadosUY.Models;
using ClasificadosUY.Tools;
using ClasificadosUY.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ClasificadosUY.Controllers
{
  [Authorize]
  public class AccountController : Controller
  {
    private readonly ClasificadosDbContext _context;

    public AccountController(ClasificadosDbContext context)
    {
      _context = context;
    }


    public IActionResult Cuenta()
    {
      // Recuperar id del usuario desde cookie
      var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

      if (string.IsNullOrEmpty(usuarioId))
      {
        return RedirectToAction("Login", "Acceso"); // Si no está logueado
      }

      int id = int.Parse(usuarioId);
      var usuario = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == id);
      var anuncios = _context.Anuncios
                             .Where(a => a.IdUsuario == id)
                             .ToList();

      var vm = new CuentaVM
      {
        Usuario = usuario,
        Anuncios = anuncios
      };

      return View(vm);
    }




    [HttpGet]
    public IActionResult Index()
    {
      // Recuperar id del usuario desde cookie
      var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

      if (string.IsNullOrEmpty(usuarioId))
      {
        return RedirectToAction("Login", "Acceso"); // Si no está logueado
      }

      int id = int.Parse(usuarioId);

      // Obtener los anuncios del usuario
      var anuncios = _context.Anuncios
          .Where(a => a.IdUsuario == id)
          .ToList();

      return View(anuncios);
    }

    // GET: Cuenta/Editar
    public async Task<IActionResult> Editar()
    {
      // recupera usuario de la sesión
      var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
      if (usuarioId == null) return RedirectToAction("Login", "Acceso");

      var usuario = await _context.Usuarios.FindAsync(int.Parse(usuarioId));
      if (usuario == null) return NotFound();

      return View(usuario);
    }

    // POST: Cuenta/Editar
    [HttpPost]
    public async Task<IActionResult> Editar(Usuario model)
    {
      //if (!ModelState.IsValid) return View(model);

      var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
      if (usuarioId == null) return RedirectToAction("Login", "Acceso");

      var usuario = await _context.Usuarios.FindAsync(int.Parse(usuarioId));
      if (usuario == null)
      {
        ViewData["Mensaje"] = "No se encontraron coincidendias";
        return View();
      }

      usuario.NombreCompleto = model.NombreCompleto;
      usuario.Telefono = model.Telefono;
      usuario.Direccion = model.Direccion;
      usuario.Departamento = model.Departamento;
      usuario.Correo = model.Correo;

      // si el usuario escribió una nueva clave la actualizamos
      if (!string.IsNullOrEmpty(model.Clave))
      {
        usuario.Clave = HashPass.HashPassword(model.Clave); // ⚠️ idealmente aquí deberías hashearla
      }

      _context.Update(usuario);
      await _context.SaveChangesAsync();

      return RedirectToAction("Index", "Account");
    }
  }
}
