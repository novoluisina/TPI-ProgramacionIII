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

        [HttpGet("GetAllAdmins")]
        public IActionResult GetAdmins()
        {
            var admins = _adminService.GetAdmins();

            try
            {
                return Ok(admins.Where(x=>x.State==true)); //solo los activos
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("GetAdminById{id}")]
        public IActionResult GetAdminById(int id)
        {
            var admin = _adminService.GetAdminById(id);

            if (admin == null)
            {
                return NotFound($"El admmin de ID: {id} no fue encontrado");
            }

            return Ok(admin);
        }

        [HttpPost("CreateNewAdmin")]
        public IActionResult CreateAdmin([FromBody] AdminPostDto dto)
        {
            if (dto.Name == "string" || dto.LastName == "string" || dto.Email== "string" || dto.UserName=="string" || dto.Password=="string")
            {
                return BadRequest("Admin no creado, por favor completar los campos");
            }
            try
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpDelete("DeleteAdmin/{id}")]
        public IActionResult DeleteAdmin(int id)
        {
            try
            {
                var existingAdmin = _adminService.GetAdminById(id);
                if (existingAdmin == null)
                {
                    return NotFound($"No se encontró ningún Admin con el ID: {id}");
                }
                _userService.DeleteUser(id);
                return Ok($"Admin con ID: {id} eliminado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("UpdateAdmin{id}")]
        public IActionResult UpdateAdmin([FromRoute] int id, [FromBody] AdminPutDto admin)
        {
            if (admin.Email == "string" || admin.UserName == "string" || admin.Password == "string")
            {
                return BadRequest("Admin no actualizado, por favor completar los campos");
            }
            var adminToUpdate=_adminService.GetAdminById(id);
            if (adminToUpdate == null)
            {
                return NotFound($"Admin con ID {id} no encontrado");
            }
            try
            {
                adminToUpdate.Email = admin.Email;
                adminToUpdate.Password = admin.Password;
                adminToUpdate.UserName = admin.UserName;

                adminToUpdate = _adminService.UpdateAdmin(adminToUpdate);
                return Ok($"Admin actualizado exitosamente");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el producto: {ex.Message}");
            }
        }

    }

}








