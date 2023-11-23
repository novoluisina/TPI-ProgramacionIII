using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TPI_ProgramacionIII.Data.Entities;
using TPI_ProgramacionIII.Data.Models;
using TPI_ProgramacionIII.Services.Implementations;
using TPI_ProgramacionIII.Services.Interfaces;

namespace TPI_ProgramacionIII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SaleOrderLineController : ControllerBase
    {
        private readonly ISaleOrderLineService _solService;

        public SaleOrderLineController(ISaleOrderLineService solService)
        {
            _solService = solService;
        }

        [HttpGet("GetAllBySaleOrder/{saleOrderId}")]
        public IActionResult GetAllBySaleOrder([FromRoute] int saleOrderId)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value.ToString();
            if (role == "Admin")
            {
                try
                {
                    var sol = _solService.GetAllBySaleOrder(saleOrderId);
                    if (sol.Count == 0)
                    {
                        return NotFound("Líneas de venta no encontradas");
                    }
                    return Ok(sol);

                }
                catch (Exception ex)
                {
                    return BadRequest($"Error: {ex.Message}");
                }
            }
            return Forbid();
        }

        [HttpGet("GetAllByProduct/{productId}")]
        public IActionResult GetAllByProduct([FromRoute] int productId)
        {
             string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value.ToString();
             if (role == "Admin")
             {
                try
                {
                    var sol = _solService.GetAllByProduct(productId);
                    if (sol.Count == 0)
                    {
                        return NotFound("Líneas de venta no encontradas");
                    }
                    return Ok(sol);

                }
                catch (Exception ex)
                {
                    return BadRequest($"Error: {ex.Message}");
                }
             }
             return Forbid();
        }


        [HttpGet("GetSOLById/{solId}")]
        public IActionResult GetOne([FromRoute] int solId)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value.ToString();
            if (role == "Admin")
            {
                try
                {
                    var sol = _solService.GetOne(solId);

                    if (sol == null)
                    {
                        return NotFound($"Línea de venta con id {solId} no encontrada");
                    }

                    return Ok(sol);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error: {ex.Message}");
                }
            }
            return Forbid();
        }

        [HttpPost("CreateSaleOrderLine")]
        public IActionResult CreateSaleOrderLine([FromBody] SaleOrderLineDto dto)
        {
             string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value.ToString();
             if (role == "Admin")
             {
                if (dto.ProductId==0 || dto.SaleOrderId==0 || dto.Amount==0 || dto.UnitPrice==0)
                {
                    return BadRequest("Por favor complete los campos");
                }

                try
                {
                    var newSaleOrderLine = new SaleOrderLine()
                    {
                        ProductId = dto.ProductId,
                        SaleOrderId = dto.SaleOrderId,
                        Amount = dto.Amount,
                        UnitPrice = dto.UnitPrice
                    };

                    newSaleOrderLine = _solService.CreateSaleOrderLine(newSaleOrderLine);
                    return Ok($"Línea de orden de venta creada exitosamente con id {newSaleOrderLine.Id}");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
             }
             return Forbid();
        }

        [HttpDelete("DeleteSOL{id}")]
        public IActionResult DeleteSaleOrderLine([FromRoute] int id)
        {
             string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value.ToString();
             if (role == "Admin")
             {
                try
                {
                    var existingSOL = _solService.GetOne(id);

                    if (existingSOL == null)
                    {
                        return NotFound($"No se encontró línea de venta con el ID: {id}");
                    }

                    _solService.DeleteSaleOrderLine(id);
                    return Ok($"Línea de venta con ID: {id} eliminada");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
             }
             return Forbid();
        }


        [HttpPut("UpdateSaleOrderLine{id}")]
        public IActionResult UpdateSaleOrderLine([FromRoute] int id, [FromBody] SaleOrderLineDto dto)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value.ToString();
            if (role == "Admin")
            {
                var solToUpdate = _solService.GetOne(id);
                if (solToUpdate == null)
                {
                    return NotFound($"Líne de venta con ID {id} no encontrada");
                }
                if (dto.ProductId == 0 || dto.SaleOrderId == 0 || dto.Amount == 0 || dto.UnitPrice == 0)
                {
                    return BadRequest("Línea de venta no actualizado, por favor completar los campos");
                }

                try
                {
                    solToUpdate.ProductId = dto.ProductId;
                    solToUpdate.SaleOrderId = dto.SaleOrderId;
                    solToUpdate.Amount = dto.Amount;
                    solToUpdate.UnitPrice = dto.UnitPrice;

                    solToUpdate = _solService.UpdateSaleOrderLine(solToUpdate);
                    return Ok($"Línea de venta actualizada exitosamente");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error al actualizar línea de venta: {ex.Message}");
                }
            }
            return Forbid();
        }
    }
}

