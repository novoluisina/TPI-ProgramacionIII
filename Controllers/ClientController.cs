using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TPI_ProgramacionIII.Data.Entities;
using TPI_ProgramacionIII.Data.Models;
using TPI_ProgramacionIII.Services.Interfaces;

namespace TPI_ProgramacionIII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IUserService _userService;

        public ClientController(IUserService userService, IClientService clientService)
        {
            _userService = userService;
            _clientService = clientService;
        }

        [HttpGet("GetAllClients")]
        public IActionResult GetClients()
        {
            var clients = _clientService.GetClients();

            try
            {
                return Ok(clients.Where(x => x.State == true)); //solo los activos
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("GetClientById{id}")]
        public IActionResult GetClientById(int id)
        {
            var client = _clientService.GetClientById(id);

            if (client == null)
            {
                return NotFound($"El cliente de ID: {id} no fue encontrado");
            }

            return Ok(client);
        }

        [HttpPost("CreateClient")]
        public IActionResult CreateAdmin([FromBody] AdminPostDto dto)
        {
            if (dto.Name == "string" || dto.LastName == "string" || dto.Email == "string" || dto.UserName == "string" || dto.Password == "string")
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

        [HttpPut("{id}")]
        public IActionResult UpdateAdmin([FromRoute] int id, [FromBody] AdminPutDto admin)
        {
            if (admin.Name == "string" || admin.LastName == "string" || admin.Email == "string" || admin.UserName == "string" || admin.Password == "string")
            {
                return BadRequest("Admin no actualizado, por favor completar los campos");
            }
            var adminToUpdate = _adminService.GetAdminById(id);
            if (adminToUpdate == null)
            {
                return NotFound($"Admin con ID {id} no encontrado");
            }
            try
            {
                adminToUpdate.Name = admin.Name;
                adminToUpdate.LastName = admin.LastName;
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
}
