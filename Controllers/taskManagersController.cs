using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todolistapi.Models;

namespace Todolistapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class taskManagersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public taskManagersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/taskManagers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<taskManager>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        // GET: api/taskManagers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<taskManager>> GettaskManager(Guid id)
        {
            var taskManager = await _context.Tasks.FindAsync(id);

            if (taskManager == null)
            {
                return NotFound();
            }

            return taskManager;
        }

        // PUT: api/taskManagers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PuttaskManager(Guid id, taskManager taskManager)
        {
            if (id != taskManager.id)
            {
                return BadRequest();
            }

            _context.Entry(taskManager).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!taskManagerExists(id))
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

        // POST: api/taskManagers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<taskManager>> PosttaskManager(taskManager taskManager)
        {
            _context.Tasks.Add(taskManager);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GettaskManager", new { id = taskManager.id }, taskManager);
        }

        // DELETE: api/taskManagers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<taskManager>> DeletetaskManager(Guid id)
        {
            var taskManager = await _context.Tasks.FindAsync(id);
            if (taskManager == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(taskManager);
            await _context.SaveChangesAsync();

            return taskManager;
        }

        private bool taskManagerExists(Guid id)
        {
            return _context.Tasks.Any(e => e.id == id);
        }
    }
}
