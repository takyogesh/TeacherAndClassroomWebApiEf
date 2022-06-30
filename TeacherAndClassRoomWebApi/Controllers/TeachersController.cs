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
        private readonly TeacherAndClassroomDbContext context;
        private readonly IMapper mapper;

        public TeachersController(TeacherAndClassroomDbContext context , IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/Teachers
        [HttpGet]
        public ActionResult GetTeachers()
        {
            if (context.Teachers == null)
                return NotFound();
            else
            {
                var teacherObj= context.Teachers.Include(e => e.Classrooms).ToList();
                return Ok(mapper.Map<List<Teacher>>(teacherObj)) ;
              //return Ok(context.Teachers.Include(e => e.Classrooms).ToList());
            }
        }

        // GET: api/Teachers/5
        [HttpGet("{id}")]
        public ActionResult GetTeacher(int id)
        {
          if (context.Teachers == null)
          {
              return NotFound();
          }
           var teacher = context.Teachers.Where(t => t.Id == id).Include(e => e.Classrooms).FirstOrDefault();
            if (teacher == null)
                return NotFound();
            else
                //return Ok(mapper.Map<TeacherApiModel>(teacher));
                return Ok(mapper.Map<Teacher>(teacher));
        }

        // PUT: api/Teachers/5
        [HttpPut("{id}")]
        public ActionResult PutTeacher(int id, TeacherAndClassroomApiModel teacherAndClassroomApiModel)
        {
            if (id != teacherAndClassroomApiModel.Teacher.Id)
            {
                return BadRequest();
            }
            var teacher = mapper.Map<Teacher>(teacherAndClassroomApiModel.Teacher);
            var classrooms = mapper.Map<List<Classroom>>(teacherAndClassroomApiModel.Classrooms);

            teacher.Classrooms = classrooms;
            context.Update(teacher);

            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }

        // POST: api/Teachers
        [HttpPost]
        public ActionResult PostTeacher(TeacherAndClassroomApiModel teacherAndClassroomApiModel)
        {
          if (context.Teachers == null)
          {
              return Problem("Entity set 'TeacherAndClassroomDbContext.Teachers'  is null.");
          }
            var teacher = mapper.Map<Teacher>(teacherAndClassroomApiModel.Teacher);
            var classrooms = mapper.Map<List<Classroom>>(teacherAndClassroomApiModel.Classrooms);

            teacher.Classrooms = classrooms;

            context.Teachers.Add(teacher);

            context.SaveChanges();
            return Ok(teacherAndClassroomApiModel.Teacher);
          //  return CreatedAtAction("GetTeacher", new { id = teacher.Id }, teacher);
        }

        // PATCH: api/Teachers
        [HttpPatch("{id}")]
        public ActionResult PatchTeachers( int id, string address)
        {
            if(context.Teachers==null)
            {
                return BadRequest();
            }
            var updateTeacher = context.Teachers.Where(t => t.Id == id).Include(c=>c.Classrooms).FirstOrDefault();
            if (updateTeacher == null)
            {
                return BadRequest();
            }
            updateTeacher.Address = address;
            context.Update(updateTeacher);
            context.SaveChanges();
            return Ok();
        }


        [HttpDelete("{id}")]
        public ActionResult DeleteTeacher(int id)
        {
            if (context.Teachers == null)
            {
                return NotFound();
            }
            var teacher = context.Teachers.Include(e => e.Classrooms).Where(t => t.Id == id).FirstOrDefault();
            if (teacher == null)
            {
                return NotFound();
            }
            context.Teachers.Remove(teacher);
            context.SaveChanges();
            return NoContent();
        }

        private bool TeacherExists(int id)
        {
            return (context.Teachers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
