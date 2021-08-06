using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodOrderApplicationAPI.DatabaseContext;
using FoodOrderApplicationAPI.Models;

namespace FoodOrderApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestMenuModelController : ControllerBase
    {
        private readonly MenuDbContext _context;

        public TestMenuModelController(MenuDbContext context)
        {
            _context = context;
        }

        // GET: api/TestMenuModel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuModel>>> GetMenu()
        {
            return await _context.Menu.ToListAsync();
        }

        // GET: api/TestMenuModel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuModel>> GetMenuModel(int id)
        {
            var menuModel = await _context.Menu.FindAsync(id);

            if (menuModel == null)
            {
                return NotFound();
            }

            return menuModel;
        }

        // PUT: api/TestMenuModel/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenuModel(int id, MenuModel menuModel)
        {
            if (id != menuModel.MenuModelID)
            {
                return BadRequest();
            }

            _context.Entry(menuModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuModelExists(id))
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

        // POST: api/TestMenuModel
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MenuModel>> PostMenuModel(MenuModel menuModel)
        {
            _context.Menu.Add(menuModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMenuModel", new { id = menuModel.MenuModelID }, menuModel);
        }

        // DELETE: api/TestMenuModel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuModel(int id)
        {
            var menuModel = await _context.Menu.FindAsync(id);
            if (menuModel == null)
            {
                return NotFound();
            }

            _context.Menu.Remove(menuModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenuModelExists(int id)
        {
            return _context.Menu.Any(e => e.MenuModelID == id);
        }
    }
}
