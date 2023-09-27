﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Notes.API.Data;
using Notes.API.Models.Entities;

namespace Notes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly TableDbContext TableDbContext;

        public NotesController(TableDbContext TableDbContext)
        {
            this.TableDbContext = TableDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotes()
        {
            return Ok(await TableDbContext.Notes.ToListAsync());
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetNoteById")]
        public async Task<IActionResult> GetNoteById([FromRoute] Guid id)
        {
            //await TableDbContext.Notes.FirstOrDefaultAsync(x =>x.ID == id);
            // or

            var note = await TableDbContext.Notes.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        [HttpPost]
        public async Task<IActionResult> AddNote(Note note)
        {
            note.Id = Guid.NewGuid();
            _ = await TableDbContext.Notes.AddAsync(note);
            _ = await TableDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNoteById), new { id = note.Id }, note);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateNote([FromRoute] Guid id, [FromBody] Note updatedNote)
        {
            var existingNote = await TableDbContext.Notes.FindAsync(id);

            if (existingNote == null)
            {
                return NotFound();
            }

            existingNote.Title = updatedNote.Title;
            existingNote.Description = updatedNote.Description;
            existingNote.IsVisable = updatedNote.IsVisable;

            _ = await TableDbContext.SaveChangesAsync();

            return Ok(existingNote);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteNote([FromRoute] Guid id)
        {
            var existingNote = await TableDbContext.Notes.FindAsync(id);

            if (existingNote == null)
            {
                return NotFound();
            }

            _ = TableDbContext.Notes.Remove(existingNote);

            _ = await TableDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}