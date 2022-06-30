using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TeacherAndClassRoomWebApi.ApiModel;
using TeachersAndClassroomDll;
using TeachersAndClassroomDll.Entities;

namespace TeacherAndClassRoomWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly TeacherAndClassroomDbContext _context;
        private readonly IMapper mapper;

        public TeachersController(TeacherAndClassroomDbContext context , IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Teachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
        {
          if (_context.Teachers == null)
          {
              return NotFound();
          }
            //  var teacher = _context.Teachers.ToList();
            return await _context.Teachers.ToListAsync();
        }

        // GET: api/Teachers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(int id)
        {
          if (_context.Teachers == null)
          {
              return NotFound();
          }
            var teacher = await _context.Teachers.Where(t => t.Id == id).Include(e => e.Classroom).FirstOrDefaultAsync();
               

            var obj = JsonConvert.SerializeObject(teacher);
            

            if (obj == null)
            {
                return NotFound();
            }

            return Ok(obj);
        }

        // PUT: api/Teachers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(int id, TeacherAndClassroomApiModel teacherAndClassroomApiModel)
        {
            if (id != teacherAndClassroomApiModel.Teacher.Id)
            {
                return BadRequest();
            }
            var teacher = mapper.Map<Teacher>(teacherAndClassroomApiModel.Teacher);
            var classrooms = mapper.Map<List<Classroom>>(teacherAndClassroomApiModel.Classrooms);

            teacher.Classroom = classrooms;

            _context.Update(teacher);
           // _context.Entry(teacherAndClassroomApiModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
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

        // POST: api/Teachers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(TeacherAndClassroomApiModel teacherAndClassroomApiModel)
        {
          if (_context.Teachers == null)
          {
              return Problem("Entity set 'TeacherAndClassroomDbContext.Teachers'  is null.");
          }
            var teacher = mapper.Map<Teacher>(teacherAndClassroomApiModel.Teacher);
            var classrooms = mapper.Map<List<Classroom>>(teacherAndClassroomApiModel.Classrooms);

            teacher.Classroom = classrooms;

            _context.Teachers.Add(teacher);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeacher", new { id = teacher.Id }, teacher);
        }

        // DELETE: api/Teachers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            if (_context.Teachers == null)
            {
                return NotFound();
            }
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeacherExists(int id)
        {
            return (_context.Teachers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
