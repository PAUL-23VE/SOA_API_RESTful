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

    [HttpPost]
    public IActionResult Post([FromBody] Usuario nuevoUsuario)
    {
        if (nuevoUsuario == null || string.IsNullOrWhiteSpace(nuevoUsuario.Username) || string.IsNullOrWhiteSpace(nuevoUsuario.Password))
        {
            return BadRequest("Datos de usuario inválidos.");
        }

        if (usuarios.Any(u => u.Username == nuevoUsuario.Username))
        {
            return Conflict("El nombre de usuario ya existe.");
        }

        usuarios.Add(nuevoUsuario);
        return CreatedAtAction(nameof(Get), new { username = nuevoUsuario.Username }, nuevoUsuario);
    }
    [HttpPut("{username}")]
    public IActionResult Put(string username, [FromBody] Usuario usuarioActualizado)
    {
        if (usuarioActualizado == null)
        {
            return BadRequest("Datos de usuario inválidos.");
        }

        var usuario = usuarios.FirstOrDefault(u => u.Username == username);
        if (usuario == null)
        {
            return NotFound("Usuario no encontrado.");
        }

        if (!string.IsNullOrWhiteSpace(usuarioActualizado.Username) && usuarioActualizado.Username != usuario.Username)
        {
            if (usuarios.Any(u => u.Username == usuarioActualizado.Username))
            {
                return Conflict("El nuevo nombre de usuario ya existe.");
            }
            usuario.Username = usuarioActualizado.Username;
        }

        if (!string.IsNullOrWhiteSpace(usuarioActualizado.Password))
        {
            usuario.Password = usuarioActualizado.Password;
        }

        return Ok(usuario);
    }

    [HttpDelete("{username}")]
    public IActionResult Delete(string username)
    {
        var usuario = usuarios.FirstOrDefault(u => u.Username == username);
        if (usuario == null)
        {
            return NotFound("Usuario no encontrado.");
        }

        usuarios.Remove(usuario);
        return Ok("Usuario eliminado exitosamente.");
    }
}
