using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NonCrudController : ControllerBase
    {
        DatabaseContext dbContext;

        public NonCrudController(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("neptuncode/{neptun}")]
        public IEnumerable<Student> GetStudentsWithNeptunCode(string neptun)
        {
            return dbContext.Students.Where(x => x.Neptun.StartsWith(neptun));
        }

        [HttpGet("studentmoresubjects")]
        public IEnumerable<Student> GetStudentsWithMoreThanOneSubject()
        {
            return dbContext.Students.Where(x => x.Subjects.Count > 1);
        }

        [HttpGet("examsubjects/{credit}")]
        public IEnumerable<Subject> GetExamSubjectsWithCredit(int credit)
        {
            return dbContext.Subjects.Where(x => x.Credit > credit && x.Exam == true);
        }
    }
}
