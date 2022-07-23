using System.ComponentModel.DataAnnotations;

namespace Project_WebApi.DTO
{
    public class LoginDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }


    }
}
