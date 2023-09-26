using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Entities;
using StudentManagement.Repository;

namespace StudentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentRepository _studentRepository;
        public StudentController(StudentRepository studentRepository)
        {
            this._studentRepository = studentRepository;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            var _students = await _studentRepository.Get();
            return Ok(_students);
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var _student = await _studentRepository.Get(id);
            return Ok(_student);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(Student student)
        {
            var _student = await _studentRepository.Add(student);
            if (_student is null)
                return BadRequest(_student);
            return Ok(_student);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(Student student)
        {
            var _student = await _studentRepository.Update(student);
            if (_student is null)
                return BadRequest(_student);
            return Ok(_student);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Student student)
        {
            var _student = await _studentRepository.Delete(student);
            if (_student is null)
                return BadRequest(_student);
            return Ok(_student);
        }
    }
}
