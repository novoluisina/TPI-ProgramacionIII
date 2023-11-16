using Microsoft.EntityFrameworkCore.Diagnostics;
using System.ComponentModel.DataAnnotations;

namespace TPI_ProgramacionIII.Data.Models
{
    public class ClientDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string UserName { get; set; }
        public string UserType { get; set; }
        public string Adress { get; set; }
    }
}
