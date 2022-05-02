using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proline.CentralEngine.DBApi.Contexts;
using Proline.Component.UserManagment.Web.Service.Database.Models;

namespace Proline.Component.UserManagment.Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaveFilesController : ControllerBase
    {
        private readonly ProlineCentralContext _context;

        public SaveFilesController(ProlineCentralContext context)
        {
            _context = context;
        }

        // GET: api/SaveFiles
        [HttpGet]
        [Route("GetSaveFiles")]
        public async Task<ActionResult<IEnumerable<SaveFile>>> GetSaveFile()
        {
            return await _context.SaveFile.ToListAsync();
        }

        // GET: api/SaveFiles/5
        [HttpGet]
        [Route("GetSaveFile")]
        public async Task<ActionResult<SaveFile>> GetSaveFile(long id)
        {
            var saveFile = await _context.SaveFile.FindAsync(id);

            if (saveFile == null)
            {
                return NotFound();
            }

            return saveFile;
        }

        // POST: api/SaveFiles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Route("PostSaveFile")]
        public async Task<ActionResult<SaveFile>> PostSaveFile(SaveFile saveFile)
        {
            _context.SaveFile.Add(saveFile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSaveFile", new { id = saveFile.FileId }, saveFile);
        }

        // PUT: api/SaveFiles/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        [Route("PutSaveFile")]
        public async Task<IActionResult> PutSaveFile(long id, SaveFile saveFile)
        {
            if (id != saveFile.FileId)
            {
                return BadRequest();
            }

            _context.Entry(saveFile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaveFileExists(id))
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


        // DELETE: api/SaveFiles/5
        [HttpDelete]
        [Route("DeleteSaveFile")]
        public async Task<ActionResult<SaveFile>> DeleteSaveFile(long id)
        {
            var saveFile = await _context.SaveFile.FindAsync(id);
            if (saveFile == null)
            {
                return NotFound();
            }

            _context.SaveFile.Remove(saveFile);
            await _context.SaveChangesAsync();

            return saveFile;
        }

        private bool SaveFileExists(long id)
        {
            return _context.SaveFile.Any(e => e.FileId == id);
        }
    }
}
