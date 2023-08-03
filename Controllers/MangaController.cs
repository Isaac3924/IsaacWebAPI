using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IsaacWebAPI.Models;

namespace IsaacWebAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MangaController : ControllerBase
  {
    private readonly MangaContext _context;
    public MangaController(MangaContext context)
    {
      _context = context;
    }

    // GET: api/Manga
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MangaDTO>>> GetManga()
    {
    if (_context.Manga == null)
    {
      return NotFound();
    }
    return await _context.Manga
      .Select(x => MangaDTO(x))
      .ToListAsync();
    }

    // GET: api/Manga/5
    [HttpGet("{id}")]
    public async Task<ActionResult<MangaDTO>> GetManga(long id)
    {
      if (_context.Manga == null)
      {
        return NotFound();
      }
      var manga = await _context.Manga.FindAsync(id);

      if (manga == null)
      {
        return NotFound();
      }

      return MangaDTO(manga);
    }

    // PUT: api/Manga/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutManga(long id, MangaDTO mangaDTO)
    {
      if (id != mangaDTO.Id)
      {
        return BadRequest();
      }

      var manga = await _context.Manga.FindAsync(id);
      if (manga == null)
      {
        return NotFound();
      }

      manga.Name = mangaDTO.Name;
      manga.IsComplete = mangaDTO.IsComplete;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException) when (!MangaExists(id))
      {
        return NotFound();
      }

      return NoContent();
    }

    // POST: api/Manga
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<MangaDTO>> PostManga(MangaDTO mangaDTO)
    {
      var manga = new Manga
      {
        IsComplete = mangaDTO.IsComplete,
        Name = mangaDTO.Name
      };

      _context.Manga.Add(manga);
      await _context.SaveChangesAsync();

      // return CreatedAtAction("GetManga", new { id = manga.Id }, manga);
      return CreatedAtAction(
        nameof(GetManga), 
        new { id = manga.Id}, 
        MangaDTO(manga));
    }

    // DELETE: api/Manga/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteManga(long id)
    {
      var manga = await _context.Manga.FindAsync(id);
      if (manga == null)
      {
        return NotFound();
      }

      _context.Manga.Remove(manga);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool MangaExists(long id)
    {
      return _context.Manga.Any(e => e.Id == id);
    }

    private static MangaDTO MangaDTO(Manga manga) =>
      new MangaDTO
      {
        Id = manga.Id,
        Name = manga.Name,
        IsComplete = manga.IsComplete
      };
  }
}
