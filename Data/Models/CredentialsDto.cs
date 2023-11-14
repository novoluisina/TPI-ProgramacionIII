using System.Globalization;

namespace TPI_ProgramacionIII.Data.Models
{
    public interface CredentialsDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
