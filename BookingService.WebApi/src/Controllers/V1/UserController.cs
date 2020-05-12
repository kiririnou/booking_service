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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet(ApiRoutes.User.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetUsersAsync();
            var usersResponse = users.Select(user => new UserResponse
            {
                Id = user.Id,
                Name = user.Name
            }).ToList();
            return Ok(usersResponse);
        }

        [HttpGet(ApiRoutes.User.Get)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(new UserResponse
            {
                Id = user.Id,
                Name = user.Name
            });
        }

        [HttpPost(ApiRoutes.User.Create)]
        public async Task<IActionResult> Post([FromBody] CreateUserRequest request)
        {
            var user = new User { Name = request.Name };

            await _userService.CreateUserAsync(user);

            var url = String.Format(
                "{0}://{1}{2}",
                HttpContext.Request.Scheme,
                HttpContext.Request.Host.ToUriComponent(),
                ApiRoutes.User.Get.Replace("{id}", user.Id.ToString())
            );

            var response = new UserResponse
            {
                Id = user.Id,
                Name = user.Name
            };

            return Created(url, response);
        }

        [HttpPut(ApiRoutes.User.Update)]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateUserRequest request)
        {
            var user = new User
            {
                Id = id,
                Name = request.Name
            };

            if (await _userService.UpdateUserAsync(user))
                return Ok(new UserResponse
                {
                    Id = user.Id,
                    Name = user.Name
                });
            return NotFound();
        }

        [HttpDelete(ApiRoutes.User.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (await _userService.DeleteUserAsync(id))
                return NoContent();
            return NotFound();
        }
    }
}