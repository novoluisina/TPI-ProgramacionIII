using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TPI_ProgramacionIII.Data.Entities;
using TPI_ProgramacionIII.Data.Models;
using TPI_ProgramacionIII.Services.Implementations;
using TPI_ProgramacionIII.Services.Interfaces;

namespace TPI_ProgramacionIII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;

        public AdminController(IUserService userService, IAdminService adminService)
        {
            _userService = userService;
            _adminService = adminService;
        }

        [HttpGet]
        public IActionResult GetAdmins()
        {
            var admins = _adminService.GetAdmins();
            
            try
            {
                return Ok(admins);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("{id}")]
        public IActionResult GetAdminById(int id)
        {
            var admin = _adminService.GetAdminById(id);

            if (admin == null)
            {
                return NotFound($"El admmin de ID: {id} no fue encontrado");
            }

            return Ok(admin);
        }

        [HttpPost("CreateAdmin")]
        public IActionResult CreateAdmin([FromBody] AdminDto dto)
        {
            var admin = new Admin()
            {
                Email = dto.Email,
                Name = dto.Name,
                LastName = dto.LastName,
                Password = dto.Password,
                UserName = dto.UserName,
                UserType = "Admin"
            };
            int id = _userService.CreateUser(admin);
            return Ok($"Admin creado exitosamente con id: {id}");
        }
         
}
        //[HttpDelete("DeleteCustomer/{username}")]
        //public IActionResult DeleteCustomer(string username)
        //{
        //    User existingCustomer = _userService.GetUserByUsername(username);
        //    if (existingCustomer == null)
        //    {
        //        return NotFound($"No se encontró un cliente con el nombre de usuario '{username}'.");
        //    }
        //    _userService.DeleteUser(username);
        //    return Ok("Cliente borrado exitosamente");
        //}

        

        //[HttpDelete("DeleteAdmin/{username}")]
        //public IActionResult DeleteAdmin(string username)
        //{
        //    User existingCustomer = _userService.GetUserByUsername(username);
        //    if (existingCustomer == null)
        //    {
        //        return NotFound($"No se encontró un admin con el nombre de usuario '{username}'.");
        //    }
        //    _userService.DeleteUser(username);
        //    return Ok("Admin borrado exitosamente");
        //}

}


