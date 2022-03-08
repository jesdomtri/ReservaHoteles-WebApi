using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using System.Threading.Tasks;
using Application;
using Serilog;
using System;

namespace Presentation.Controllers
{
    [Route("api/reservas")]
    [ApiController]
    public class ReservaController : Controller
    {
        private readonly InterfaceReservaManager _reservaManager;

        public ReservaController(InterfaceReservaManager reservaManager)
        {
            _reservaManager = reservaManager;
        }

        /// <summary>
        /// Muestra todas las reservas
        /// </summary>
        /// <returns>Devuelve todas las reservas</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllReservas()
        {
            Log.Information("Se han mostrado todas las reservas");
            return Ok(await _reservaManager.GetAllReservas());
        }

        /// <summary>
        /// Muestra una reserva mediante su ID
        /// </summary>
        /// <param name="id">Identificador de la reserva</param>
        /// <returns>Devuelve una reserva</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReserva(int id)
        {
            var reserva = await _reservaManager.GetReserva(id);
            if (reserva != null)
            {
                Log.Information($"Se ha mostrado la reserva con ID igual a {id}");
                return Ok(reserva);
            }
            else
            {
                Log.Information($"No se ha encontrado la reserva con ID igual a {id}");
                return NotFound();
            }
        }

        /// <summary>
        /// Anyade una reserva leyendo todos los campos del Body
        /// </summary>
        /// <param name="reserva">Body de la reserva</param>
        /// <returns>Crea una reserva</returns>
        [HttpPost]
        public async Task<IActionResult> PostReserva([FromBody] Reserva reserva)
        {
            if (reserva == null) { return BadRequest(); }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            try
            {
                var created = await _reservaManager.InsertReserva(reserva);
                Log.Information($"Se ha creado la reserva con ID igual a {reserva.ReservaID}");
                return Created("created", created);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "ERROR");
                return Created(ex.Message, false);
            }
        }

        /// <summary>
        /// Edita una reserva leyendo todos los campos del Body
        /// </summary>
        /// <param name="reserva">Body de la reserva</param>
        /// <returns>Edita una reserva</returns>
        [HttpPut]
        public async Task<IActionResult> PutReserva([FromBody] Reserva reserva)
        {
            if (reserva == null) { return BadRequest(); }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            try
            {
                await _reservaManager.UpdateReserva(reserva);
                Log.Information($"Se ha actualizado la reserva con ID igual a {reserva.ReservaID}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "ERROR");
            }
            return NoContent();
        }

        /// <summary>
        /// Elimina una reserva mediante su ID
        /// </summary>
        /// <param name="id">Identificador de la reserva</param>
        /// <returns>Elimina una reserva</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReserva(int id)
        {
            try
            {
                await _reservaManager.DeleteReserva(new Reserva { ReservaID = id });
                Log.Information($"Se ha eliminado la reserva con ID igual a {id}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "ERROR");
            }
            return NoContent();
        }

        /// <summary>
        /// Cancela una reserva mediante su ID
        /// </summary>
        /// <param name="id">Identificador de la reserva</param>
        /// <returns>Cancela una reserva</returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> CancelReserva(int id)
        {
            try
            {
                var cancel = await _reservaManager.CancelarReserva(id);
                Log.Information($"Se ha cancelado la reserva con ID igual a {id}");
                return Ok(cancel);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "ERROR");
                return NotFound();
            }
        }

        /// <summary>
        /// Muestra las reservas activas mediante el ID de un usuario
        /// </summary>
        /// <param name="id_usuario">Identificador del usuario</param>
        /// <returns>Reservas activas de un usuario</returns>
        [HttpGet("usuario/{id_usuario}")]
        public async Task<IActionResult> GetAllReservasActivasByUsuario(int id_usuario)
        {
            Log.Information($"Se han mostrado todas las reservas activas para el " +
                $"usuario con ID igual a {id_usuario}");
            return Ok(await _reservaManager.GetAllReservasActivasByUsuario(id_usuario));
        }

        /// <summary>
        /// Muestra las reservas de un hotel mediante su ID y el email de un usuario
        /// </summary>
        /// <param name="id_hotel">Identificador del hotel</param>
        /// <param name="mail">Email del usuario</param>
        /// <returns>Reservas de un hotel por email de usuario</returns>
        [HttpGet("hotelAndEmail")]
        public async Task<IActionResult> GetAllReservasByHotelAndEmail(int id_hotel, string mail)
        {
            Log.Information($"Se han mostrado todas las reservas que tengan " +
                $"el ID del hotel igual a {id_hotel} y el email del usuario " +
                $"sea igual a {mail}");
            return Ok(await _reservaManager.GetAllReservasByHotelAndEmail(id_hotel, mail));
        }
    }
}
