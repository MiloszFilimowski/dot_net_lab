using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Entities;

namespace CarDealer.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CarAPIController : ControllerBase
    {
        private readonly CarDealerContext _context;

        public CarAPIController(CarDealerContext context)
        {
            _context = context;
        }

        // GET: api/CarAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarEntity>>> GetCars()
        {
            return await _context.Cars.ToListAsync();
        }

        // GET: api/CarAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarEntity>> GetCarEntity(int id)
        {
            var carEntity = await _context.Cars.FindAsync(id);

            if (carEntity == null)
            {
                return NotFound();
            }

            return carEntity;
        }

        // PUT: api/CarAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarEntity(int id, CarEntity carEntity)
        {
            if (id != carEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(carEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CarAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarEntity>> PostCarEntity(CarEntity carEntity)
        {
            _context.Cars.Add(carEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarEntity", new { id = carEntity.Id }, carEntity);
        }

        // DELETE: api/CarAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarEntity(int id)
        {
            var carEntity = await _context.Cars.FindAsync(id);
            if (carEntity == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(carEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarEntityExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
