using StudentManagement.Entities;
using StudentManagement.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Repository
{
    public class StudentRepository
    {
        private readonly IGenericRepository<Student> _repository;
        public StudentRepository(IGenericRepository<Student> repository)
        {
            _repository = repository;
        }

        public async Task<List<Student>> Get()
        {
            return await _repository.Get();
        }

        public async Task<Student> Get(int id)
        {
            return await _repository.Get(id);
        }

        public async Task<Student> Add(Student Student)
        {
            return await _repository.Add(Student);
        }

        public async Task<Student> Update(Student Student)
        {
            return await _repository.Update(Student);
        }

        public async Task<Student> Delete(Student Student)
        {
            return await _repository.Delete(Student.Id);
        }
    }
}
