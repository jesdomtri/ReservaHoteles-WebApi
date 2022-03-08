using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using System.Threading.Tasks;
using Application;
using Serilog;
using System;

namespace Presentation.Controllers
{
    [Route("api/hoteles")]
    [ApiController]
    public class HotelController : Controller
    {
        private readonly InterfaceHotelManager _hotelManager;

        public HotelController(InterfaceHotelManager hotelManager)
        {
            _hotelManager = hotelManager;
        }

        /// <summary>
        /// Muestra todos los hoteles
        /// </summary>
        /// <returns>Devuelve todos los hoteles</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllHoteles()
        {
            Log.Information("Se han mostrado todos los hoteles");
            return Ok(await _hotelManager.GetAllHoteles());
        }

        /// <summary>
        /// Muestra un hotel mediante su ID
        /// </summary>
        /// <param name="id">Identificador del hotel</param>
        /// <returns>Devuelve un hotel mediante su ID</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotel(int id)
        {
            var hotel = await _hotelManager.GetHotel(id);
            if (hotel != null)
            {
                Log.Information($"Se ha mostrado el hotel con ID igual a {id}");
                return Ok(hotel);
            }
            else
            {
                Log.Information($"No se ha encontrado el hotel con ID igual a {id}");
                return NotFound();
            }
        }

        /// <summary>
        /// Anyade un hotel leyendo todos los campos del Body
        /// </summary>
        /// <param name="hotel">Body del hotel</param>
        /// <returns>Crea un hotel</returns>
        [HttpPost]
        public async Task<IActionResult> PostHotel([FromBody] Hotel hotel)
        {
            if (hotel == null) { return BadRequest(); }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            try
            {
                var created = await _hotelManager.InsertHotel(hotel);
                Log.Information($"Se ha creado el hotel {hotel.Nombre}");
                return Created("created", created);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "ERROR");
                return Created(ex.Message, false);
            }
        }

        /// <summary>
        /// Actualiza un hotel leyendo todos los campos del Body
        /// </summary>
        /// <param name="hotel">Body del hotel</param>
        /// <returns>Actualiza un hotel</returns>
        [HttpPut]
        public async Task<IActionResult> PutHotel([FromBody] Hotel hotel)
        {
            if (hotel == null) { return BadRequest(); }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            try
            {
                await _hotelManager.UpdateHotel(hotel);
                Log.Information($"Se ha actualizado el hotel con ID igual a {hotel.HotelID}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "ERROR");
            }
            return NoContent();
        }

        /// <summary>
        /// Elimina un hotel mediante su ID
        /// </summary>
        /// <param name="id">Identificador del hotel</param>
        /// <returns>Elimina un hotel</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            try
            {
                await _hotelManager.DeleteHotel(new Hotel { HotelID = id });
                Log.Information($"Se ha eliminado el hotel con ID igual a {id}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "ERROR");
            }
            return NoContent();
        }
    }
}
