using System.ComponentModel.DataAnnotations;

namespace CinemaTicketSystem.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "enter email")]
    [EmailAddress(ErrorMessage = "Wrong Email Format")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "enter password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}