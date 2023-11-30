using Microsoft.AspNetCore.Mvc;
using StudentDetails.Model;
using System.Collections.Generic;
namespace StudentDetails.Model
{

    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentRepository studentRepo;

        public StudentsController(StudentRepository studentRepo)
        {
            this.studentRepo = studentRepo;
        }

        // GET: api/students
        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {

            var students = studentRepo.GetStudents();
            return Ok(students);
        }

    

        [HttpPost]
        public ActionResult<Student> PostStudent(Student student)
        {
            int newId = studentRepo.InsertStudent(student);


            return Ok();
        }

   
    }
}