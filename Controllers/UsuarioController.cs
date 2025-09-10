using Microsoft.AspNetCore.Mvc;
using SOA_API_RESTful.Models;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private static List<Usuario> usuarios = new List<Usuario>
        {
            new Usuario { Username = "Juan", Password = "1234" },
            new Usuario { Username = "Ana", Password = "abcd" },
            new Usuario { Username = "Pedro", Password = "pass123" }
        };

    [HttpGet]
    public IActionResult Get() => Ok(usuarios);

}
