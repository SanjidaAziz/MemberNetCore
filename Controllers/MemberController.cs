using CrudnetCoreMember.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudnetCoreMember.Controllers
{
	
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly MemberContext _dbContext;

        public MemberController(MemberContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMember()
        {
            if (_dbContext.Members == null)
            {
                return NotFound();

            }
            return await _dbContext.Members.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Member>>> GetMember(int id)
        {
            if (_dbContext.Members == null)
            {
                return NotFound();
            }
            var member = await _dbContext.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return Ok(member);
        }

        [HttpPost]
        public async Task<ActionResult<Member>> PostMember(Member member)
        {
            _dbContext.Members.Add(member);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Member), new { id = member.Id }, member);
        }

        [HttpPut]
        public async Task<IActionResult> PutBrand(int id, Member member)
        {
            if (id != member.Id)
            {
                return BadRequest();
            }
            _dbContext.Entry(member).State = EntityState.Modified;
            try

            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        private bool MemberAvailable(int id)
        {
            return (_dbContext.Members?.Any(x => x.Id == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            if (_dbContext.Members == null)
            {
                return NotFound();
            }
            var member = await _dbContext.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();

            }
            _dbContext.Members.Remove(member);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
