using System.ComponentModel.DataAnnotations;

namespace BaseCleanArchitecture.API.ApiModels
{
    public class UserDTO
    {
        [Required(ErrorMessage = "The {0} is required!")]
        [StringLength(80, ErrorMessage = "The {0} mus have {2} and {1} characters!", MinimumLength = 6)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The {0} is required!")]
        [EmailAddress(ErrorMessage = "The {0} is invalid!")]
        public string Email { get; set; }

        public UserDTO()
        {
        }
    }
}