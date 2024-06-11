using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        DatabaseContext dbContext;

        public StudentController(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Student> GetStudents()
        {
            return dbContext.Students;
        }

        [HttpGet("{id}")]
        public Student? GetStudent(string id)
        {
            return dbContext.Students.FirstOrDefault(x => x.Id == id);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddStudent([FromBody] Student s)
        {
            if (string.IsNullOrEmpty(s.Id))
            {
                s.Id = Guid.NewGuid().ToString();
            }
            else
            {
                var existingStudent = dbContext.Students.FirstOrDefault(x => x.Id == s.Id);
                if (existingStudent != null)
                {
                    return Conflict("Student with the same Id already exists");
                }
            }

            dbContext.Add(s);
            dbContext.SaveChanges();
            return Ok(s);
        }


        [Authorize]
        [HttpPut]
        public void EditStudent([FromBody] Student s)
        {
            var old = dbContext.Students.FirstOrDefault(x => x.Id == s.Id);
            old.Name = s.Name;
            old.Neptun = s.Neptun;
            old.Image = s.Image;
            dbContext.SaveChanges();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public void DeteleStudent(string id)
        {
            var studentToDelete = dbContext.Students.FirstOrDefault(x => x.Id == id);
            dbContext.Students.Remove(studentToDelete);
            dbContext.SaveChanges();
        }
    }
}
