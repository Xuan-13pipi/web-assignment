using System.ComponentModel.DataAnnotations;

namespace web_assignment.Models;

#nullable disable warnings


public class LoginVM
{
    [Required]
    [EmailAddress]
    [StringLength(100, ErrorMessage = "Email address cannot be longer than 100 characters.")]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
    public bool RememberMe { get; set; }

}

public class RegisterVM
{
    [Required]
    [EmailAddress]
    [StringLength(100, ErrorMessage = "Email address cannot be longer than 100 characters.")]
    public string Email { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "Password must be at least 6 characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    public string Phone { get; set; }

}

