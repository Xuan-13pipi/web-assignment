using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace web_assignment;

public class Helper
{
    private readonly IWebHostEnvironment en;
    private readonly IHttpContextAccessor ct;// access to the SignInAsync() and SignOutAsync() methods 

    public Helper(IWebHostEnvironment en, IHttpContextAccessor ct)
    {
        this.en = en;
        this.ct = ct;
    }

    // ------------------------------------------------------------------------
    // Photo Upload
    // ------------------------------------------------------------------------

    public string ValidatePhoto(IFormFile f)
    {
        var reType = new Regex(@"^image\/(jpeg|png)$", RegexOptions.IgnoreCase);
        var reName = new Regex(@"^.+\.(jpeg|jpg|png)$", RegexOptions.IgnoreCase);

        if (!reType.IsMatch(f.ContentType) || !reName.IsMatch(f.FileName))
        {
            return "Only JPG and PNG photo is allowed.";
        }
        else if (f.Length > 1 * 1024 * 1024)
        {
            return "Photo size cannot more than 1MB.";
        }

        return "";
    }

    public string SavePhoto(IFormFile f, string folder)
    {
        var file = Guid.NewGuid().ToString("n") + ".jpg";
        var path = Path.Combine(en.WebRootPath, folder, file);

        var options = new ResizeOptions
        {
            Size = new(200, 200),
            Mode = ResizeMode.Crop,
        };

        using var stream = f.OpenReadStream();
        using var img = Image.Load(stream);
        img.Mutate(x => x.Resize(options));
        img.Save(path);

        return file;
    }
   
    public void DeletePhoto(string file, string folder)
    {
        file = Path.GetFileName(file);
        var path = Path.Combine(en.WebRootPath, folder, file);
        File.Delete(path);
    }



    // ------------------------------------------------------------------------
    // Security Helper Functions
    // ------------------------------------------------------------------------

    
    private readonly PasswordHasher<object> ph = new();

    public string HashPassword(string password)
    {
        
        return ph.HashPassword(0,password);
    }

    public bool VerifyPassword(string hash, string password)
    {
        
        return ph.VerifyHashedPassword(0,hash,password)
                == PasswordVerificationResult.Success; //Verify if hash matches to the given password

    }

    public void SignIn(string email, string role, bool rememberMe)
    {
        // (1) Claim（Represents the user's identity characteristics (key-value pairs)）, identity and principal

        List<Claim> claims = 
            [
            new(ClaimTypes.Name, email),//Stores the user's unique ID (Email)
            new(ClaimTypes.Role, role),//Stores user roles (such as "Admin")
            ];

        
        ClaimsIdentity identity = new(claims,"Cookies");//the authentication scheme must be "Cookies"



        ClaimsPrincipal principal = new(identity);

        // (2) Remember me (authentication properties)
        
        AuthenticationProperties properties = new()
        {
            IsPersistent = rememberMe,//Create a persistent login session（14 day） if user ticks Remember Me (true)
        };

        // (3) Sign in
        ct.HttpContext!.SignInAsync(principal, properties);//signin the user
    }

    public void SignOut()
    {
        // Sign out
        ct.HttpContext!.SignOutAsync();
    }

    public string RandomPassword()
    {
        string s = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string password = "";

        Random r = new();

        for (int i = 1; i <= 10; i++)//Repeat 10 times

        {
            password += s[r.Next(s.Length)];//r.Next(36) = Generate a random integer between 0 (inclusive) and 36 (exclusive).
        }

        return password;
    }
}
