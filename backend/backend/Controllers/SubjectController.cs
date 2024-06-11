using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        DatabaseContext dbContext;

        public SubjectController(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Subject> GetSubjects()
        {
            return dbContext.Subjects;
        }

        [HttpGet("{id}")]
        public Subject? GetSubject(string id)
        {
            return dbContext.Subjects.FirstOrDefault(x => x.Id == id);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddSubject([FromBody] Subject s)
        {
            if (string.IsNullOrEmpty(s.Id))
            {
                s.Id = Guid.NewGuid().ToString();
            }
            else
            {
                var existingSubject = dbContext.Subjects.FirstOrDefault(x => x.Id == s.Id);
                if (existingSubject != null)
                {
                    return Conflict("Subject with the same Id already exists");
                }
            }

            dbContext.Add(s);
            dbContext.SaveChanges();
            return Ok(s);
        }

        [Authorize]
        [HttpPut]
        public void EditSubject([FromBody] Subject s)
        {
            var old = dbContext.Subjects.FirstOrDefault(x => x.Id == s.Id);
            old.Name = s.Name;
            old.Neptun = s.Neptun;
            old.Image = s.Image;
            old.Exam = s.Exam;
            old.Credit = s.Credit;
            dbContext.SaveChanges();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public void DeteleSubject(string id)
        {
            var subjectToDelete = dbContext.Subjects.FirstOrDefault(x => x.Id == id);
            dbContext.Subjects.Remove(subjectToDelete);
            dbContext.SaveChanges();
        }
    }
}
