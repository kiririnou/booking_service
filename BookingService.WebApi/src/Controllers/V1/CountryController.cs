using System;
using System.Linq;
using System.Threading.Tasks;
using BookingService.WebApi.Contracts.V1;
using BookingService.WebApi.Contracts.V1.Requests;
using BookingService.WebApi.Contracts.V1.Responses;
using BookingService.WebApi.Models;
using BookingService.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.WebApi.Controllers
{
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet(ApiRoutes.Country.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var countries = await _countryService.GetCountriesAsync();
            var countriesResponses = countries.Select(country => new CountryResponse
            {
                Id = country.Id,
                Name = country.Name
            }).ToList();
            return Ok(countriesResponses);
        }

        [HttpGet(ApiRoutes.Country.Get)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var country = await _countryService.GetCountryByIdAsync(id);
            if (country == null)
                return NotFound();
            return Ok(new CountryResponse
            {
                Id = country.Id,
                Name = country.Name
            });
        }

        [HttpPost(ApiRoutes.Country.Create)]
        public async Task<IActionResult> Post([FromBody] CreateCountryRequest request)
        {
            var country = new Country { Name = request.Name };

            await _countryService.CreateCountryAsync(country);

            var url = string.Format(
                "{0}://{1}{2}",
                HttpContext.Request.Scheme,
                HttpContext.Request.Host.ToUriComponent(),
                ApiRoutes.Country.Get.Replace("{id}", country.Id.ToString())
            );

            var response = new CountryResponse
            {
                Id = country.Id,
                Name =  country.Name
            };

            return Created(url, response);
        }

        [HttpPut(ApiRoutes.Country.Update)]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateCountryRequest request)
        {
            var country = new Country
            {
                Id = id,
                Name = request.Name
            };

            if (await _countryService.UpdateCountryAsync(country))
                return Ok(new CountryResponse
                {
                    Id = country.Id,
                    Name = country.Name
                });
            return NotFound();
        }

        [HttpDelete(ApiRoutes.Country.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (await _countryService.DeleteCountryAsync(id))
                return NoContent();
            return NotFound();
        }
    }
}