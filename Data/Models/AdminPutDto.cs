using System.ComponentModel.DataAnnotations;

namespace TPI_ProgramacionIII.Data.Models
{
    public class AdminPutDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
