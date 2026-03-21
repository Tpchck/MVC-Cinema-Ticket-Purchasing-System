using System.ComponentModel.DataAnnotations;

namespace CinemaTicketSystem.Models;

public class RegisterViewModel
{
    [Required(ErrorMessage = "enter first name")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "enter last name")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "enter phone number")]
    public  string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "enter email")]
    [EmailAddress(ErrorMessage = "wrong email format")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "enter password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "confirm password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "passwords dont match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}