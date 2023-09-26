using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Entities;
using StudentManagement.Repository;
using System.Diagnostics.Metrics;

namespace StudentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly CountryRepository _countryRepository;
        public CountryController(CountryRepository countryRepository)
        {
            this._countryRepository = countryRepository;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            var _countries = await _countryRepository.Get();
            return Ok(_countries);
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var _country = await _countryRepository.Get(id);
            return Ok(_country);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(Country country)
        {
            var _country = await _countryRepository.Add(country);
            if (_country is null)
                return BadRequest(_country);
            return Ok(_country);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(Country country)
        {
            var _country = await _countryRepository.Update(country);
            if (_country is null)
                return BadRequest(_country);
            return Ok(_country);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Country country)
        {
            var _country = await _countryRepository.Delete(country);
            if (_country is null)
                return BadRequest(_country);
            return Ok(_country);
        }
    }
}
