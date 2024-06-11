using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        DatabaseContext dbContext;

        public TeacherController(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Teacher> GetTeachers()
        {
            return dbContext.Teachers;
        }

        [HttpGet("{id}")]
        public Teacher? GetTeacher(string id)
        {
            return dbContext.Teachers.FirstOrDefault(x => x.Id == id);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddTeacher([FromBody] Teacher s)
        {
            if (string.IsNullOrEmpty(s.Id))
            {
                s.Id = Guid.NewGuid().ToString();
            }
            else
            {
                var existingTeacher = dbContext.Teachers.FirstOrDefault(x => x.Id == s.Id);
                if (existingTeacher != null)
                {
                    return Conflict("Teacher with the same Id already exists");
                }
            }

            dbContext.Add(s);
            dbContext.SaveChanges();
            return Ok(s);
        }

        [Authorize]
        [HttpPut]
        public void EditTeacher([FromBody] Teacher s)
        {
            var old = dbContext.Teachers.FirstOrDefault(x => x.Id == s.Id);
            old.Name = s.Name;
            old.Neptun = s.Neptun;
            old.Image = s.Image;
            dbContext.SaveChanges();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public void DeteleTeacher(string id)
        {
            var teacherToDelete = dbContext.Teachers.FirstOrDefault(x => x.Id == id);
            dbContext.Teachers.Remove(teacherToDelete);
            dbContext.SaveChanges();
        }
    }
}
