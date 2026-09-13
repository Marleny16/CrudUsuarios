using CrudUsuarios.Data;
using CrudUsuarios.Models;
using CrudUsuarios.Validaciones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudUsuarios.Controllers
{
    /// <summary>
    /// Controlador (C de MVC): orquesta el CRUD de Usuarios.
    /// Antes de Insertar/Actualizar, llama a la regla de negocio aislada
    /// ValidadorUsuario.ValidarCorreo(), que es la que se prueba en el video.
    /// </summary>
    public class UsuariosController : Controller
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();
            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Correo")] Usuario usuario)
        {
            // 1) Regla de negocio AISLADA: se valida ANTES de tocar la base de datos.
            try
            {
                ValidadorUsuario.ValidarCorreo(usuario.Correo);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(nameof(usuario.Correo), ex.Message);
                return View(usuario);
            }

            if (!ModelState.IsValid) return View(usuario);

            // 2) Solo si la validación pasó, se toca la base de datos.
            _context.Add(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Correo")] Usuario usuario)
        {
            if (id != usuario.Id) return NotFound();

            try
            {
                ValidadorUsuario.ValidarCorreo(usuario.Correo);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(nameof(usuario.Correo), ex.Message);
                return View(usuario);
            }

            if (!ModelState.IsValid) return View(usuario);

            try
            {
                _context.Update(usuario);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Usuarios.Any(u => u.Id == usuario.Id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
