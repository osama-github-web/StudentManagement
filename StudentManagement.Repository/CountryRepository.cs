using StudentManagement.Entities;
using StudentManagement.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Repository
{
    public class CountryRepository
    {
        private readonly IGenericRepository<Country> _repository;
        public CountryRepository(IGenericRepository<Country> repository)
        {
            _repository = repository;
        }

        public async Task<List<Country>> Get()
        {
            return await _repository.Get();
        }

        public async Task<Country> Get(int id)
        {
            return await _repository.Get(id);
        }

        public async Task<Country> Add(Country Country)
        {
            return await _repository.Add(Country);
        }

        public async Task<Country> Update(Country Country)
        {
            return await _repository.Update(Country);
        }

        public async Task<Country> Delete(Country Country)
        {
            return await _repository.Update(Country);
        }
    }
}
