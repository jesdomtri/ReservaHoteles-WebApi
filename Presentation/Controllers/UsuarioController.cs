using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using System.Threading.Tasks;
using Application;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;

namespace Presentation.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly InterfaceUsuarioManager _usuarioManager;

        public UsuarioController(InterfaceUsuarioManager usuarioManager)
        {
            _usuarioManager = usuarioManager;
        }

        /// <summary>
        /// Muestra todos los usuarios
        /// </summary>
        /// <returns>Devuelve todos los usuarios</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllUsuarios()
        {
            Log.Information("Se han mostrado todos los usuarios");
            return Ok(await _usuarioManager.GetAllUsuarios());
        }

        /// <summary>
        /// Muestra un usuario mediante su ID
        /// </summary>
        /// <param name="id">Identificador del usuario</param>
        /// <returns>Devuelve un usuario</returns>
        /// <response code="404">No se ha encontrado el usuario</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsuario(int id)
        {
            var usuario = await _usuarioManager.GetUsuario(id);
            if (usuario != null)
            {
                Log.Information($"Se ha mostrado el usuario con ID igual a {id}");
                return Ok(usuario);
            }
            else
            {
                Log.Information($"No se ha encontrado el usuario con ID igual a {id}");
                return NotFound();
            }
        }

        /// <summary>
        /// Anyade un usuario leyendo todos los campos del Body
        /// </summary>
        /// <param name="usuario">Body del usuario</param>
        /// <returns>Devuelve un usuario</returns>
        [HttpPost]
        public async Task<IActionResult> PostUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null) { return BadRequest(); }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var created = false;
            try
            {
                created = await _usuarioManager.InsertUsuario(usuario);
                Log.Information($"Se ha creado el usuario {usuario.Nombre} {usuario.Apellidos} con ID igual a {usuario.UsuarioID}");
                return Created("Creado nuevo usuario", created);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "ERROR");
                return Created(ex.Message, created);
            }
        }

        /// <summary>
        /// Actualiza un usuario leyendo todos los campos del Body
        /// </summary>
        /// <param name="usuario">Body del usuario</param>
        /// <returns>Actualiza un usuario</returns>
        [HttpPut]
        public async Task<IActionResult> PutUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null) { return BadRequest(); }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            try
            {
                await _usuarioManager.UpdateUsuario(usuario);
                Log.Information($"Se ha actualizado el usuario con ID igual a {usuario.UsuarioID}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "ERROR");
            }
            return NoContent();
        }

        /// <summary>
        /// Elimina un usuario mediante su ID
        /// </summary>
        /// <param name="id">Identificador del usuario</param>
        /// <returns>Elimina un usuario</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                await _usuarioManager.DeleteUsuario(new Usuario { UsuarioID = id });
                Log.Information($"Se ha eliminado el usuario con ID igual a {id}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "ERROR");
            }
            return NoContent();
        }
    }
}
