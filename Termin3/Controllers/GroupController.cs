using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Termin3.DataAccess;
using Termin3.DataAccess.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Termin3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly Termin3Context _context;
        public GroupController() 
        {
            _context = new Termin3Context();
        }
        // GET: api/<GroupController>
        [HttpGet]
        public IActionResult Get()
        {
            var groups = _context.Groups.ToList();
            
            return Ok(groups);
        }

        // GET api/<GroupController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var group = _context.Groups.Find(id);
            return Ok(group);
        }

        // POST api/<GroupController>
        [HttpPost]
        public void Post([FromBody] GroupDto dto)
        {
            var group = new Group
            {
                Name = dto.Name
            };

            _context.Groups.Add(group);

            _context.SaveChanges();
        }

        // PUT api/<GroupController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] GroupDto dto)
        {
            var group = _context.Groups.Find(id);
            if(group == null)
            {
                return NotFound();
            }

            group.Name = dto.Name;
            try
            {
                _context.SaveChanges();

                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
           
        }

        // DELETE api/<GroupController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var groupToDelete = _context.Groups.Find(id);
            
            if (groupToDelete == null)
            {
                return NotFound();
            }

            try
            {
                groupToDelete.IsDeleted = true;
                groupToDelete.DeletedAt = DateTime.Now;
                _context.SaveChanges();
                
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }

    public class GroupDto
    {
        public string Name { get; set; }
    }
}
