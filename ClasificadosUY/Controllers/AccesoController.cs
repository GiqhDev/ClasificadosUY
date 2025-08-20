using ClasificadosUY.Data;
using ClasificadosUY.Models;
using ClasificadosUY.Tools;
using ClasificadosUY.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace ClasificadosUY.Controllers
{
  public class AccesoController : Controller
  {
    private readonly ClasificadosDbContext _dbContext;

    public AccesoController(ClasificadosDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Registrarse()
    {     
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Registrarse(RegistroVM model)
    {
      if (model.Clave != model.ConfirmarClave)
      {
        ViewData["Mensaje"] = "Las claves no coinciden.";
        return View(model);
      }

      Usuario nuevoUsuario = new()
      {
        NombreCompleto = model.NombreCompleto,
        Telefono = model.Telefono,
        Direccion = model.Direccion,
        Departamento = model.Departamento,
        Correo = model.Correo,
        Clave = HashPass.HashPassword( model.Clave),
        Privacidad = model.Privacidad,
        FechaRegistro = DateTime.UtcNow,
        Anuncios = new List<Anuncio>(), 
      };

      await _dbContext.Usuarios.AddAsync(nuevoUsuario);
      await _dbContext.SaveChangesAsync();

      if (nuevoUsuario.IdUsuario != 0)
        return RedirectToAction("Login", "Acceso");

      ViewData["Mensaje"] = "No se pudo registrar el usuario";
      return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
      if (User.Identity!.IsAuthenticated)
      {
        return RedirectToAction("Index", "Home");
      }
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM model)
    {
      Usuario? usuarioEncontrado = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Correo == model.Correo);
      if (usuarioEncontrado != null && HashPass.VerifyPassword(model.Clave, usuarioEncontrado.Clave))
      {
        List<Claim> claims =
        [
          new Claim(ClaimTypes.NameIdentifier, usuarioEncontrado.IdUsuario.ToString()),
          new Claim(ClaimTypes.Name, usuarioEncontrado.NombreCompleto),
          new Claim(ClaimTypes.Email, usuarioEncontrado.Correo)
        ];

        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties
        {
          //IsPersistent = model.Recordar,
          ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
          AllowRefresh = true
        });


        return RedirectToAction("Index", "Home");
      }
      ViewData["Mensaje"] = "No se encontraron coincidendias";
      return View();
    }


    [HttpPost]
    public IActionResult Logout()
    {
      HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
      return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult AccesoDenegado()
    {
      return View();
    }
  }
}
