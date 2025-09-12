using Microsoft.AspNetCore.Mvc;
using SOA_API_RESTful.Models;
using SOA_API_RESTful.Data;
using MySql.Data.MySqlClient;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var usuarios = new List<Usuario>();
        using (var conn = DB.GetConnection())
        {
            var cmd = new MySqlCommand("SELECT Username, Password FROM usuarios", conn);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    usuarios.Add(new Usuario
                    {
                        Username = reader.GetString("Username"),
                        Password = reader.GetString("Password")
                    });
                }
            }
        }
        return Ok(usuarios);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Usuario nuevoUsuario)
    {
        if (nuevoUsuario == null || string.IsNullOrWhiteSpace(nuevoUsuario.Username) || string.IsNullOrWhiteSpace(nuevoUsuario.Password))
        {
            return BadRequest("Datos de usuario inválidos.");
        }

        using (var conn = DB.GetConnection())
        {
            var checkCmd = new MySqlCommand("SELECT COUNT(*) FROM usuarios WHERE Username=@Username", conn);
            checkCmd.Parameters.AddWithValue("@Username", nuevoUsuario.Username);
            var exists = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0;

            if (exists)
                return Conflict("El nombre de usuario ya existe.");

            var cmd = new MySqlCommand("INSERT INTO usuarios (Username, Password) VALUES (@Username, @Password)", conn);
            cmd.Parameters.AddWithValue("@Username", nuevoUsuario.Username);
            cmd.Parameters.AddWithValue("@Password", nuevoUsuario.Password);
            cmd.ExecuteNonQuery();
        }

        return CreatedAtAction(nameof(Get), new { username = nuevoUsuario.Username }, nuevoUsuario);
    }

    [HttpPut("{username}")]
    public IActionResult Put(string username, [FromBody] Usuario usuarioActualizado)
    {
        if (usuarioActualizado == null)
            return BadRequest("Datos de usuario inválidos.");

        using (var conn = DB.GetConnection())
        {
            // Verificar que exista el usuario
            var checkCmd = new MySqlCommand("SELECT COUNT(*) FROM usuarios WHERE Username=@Username", conn);
            checkCmd.Parameters.AddWithValue("@Username", username);
            var exists = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0;

            if (!exists)
                return NotFound("Usuario no encontrado.");

            // Si quiere cambiar el username
            if (!string.IsNullOrWhiteSpace(usuarioActualizado.Username) && usuarioActualizado.Username != username)
            {
                var checkNew = new MySqlCommand("SELECT COUNT(*) FROM usuarios WHERE Username=@NewUsername", conn);
                checkNew.Parameters.AddWithValue("@NewUsername", usuarioActualizado.Username);
                var newExists = Convert.ToInt32(checkNew.ExecuteScalar()) > 0;

                if (newExists)
                    return Conflict("El nuevo nombre de usuario ya existe.");

                var updateCmd = new MySqlCommand("UPDATE usuarios SET Username=@NewUsername, Password=@Password WHERE Username=@OldUsername", conn);
                updateCmd.Parameters.AddWithValue("@NewUsername", usuarioActualizado.Username);
                updateCmd.Parameters.AddWithValue("@Password", usuarioActualizado.Password ?? "");
                updateCmd.Parameters.AddWithValue("@OldUsername", username);
                updateCmd.ExecuteNonQuery();

                return Ok(usuarioActualizado);
            }
            else
            {
                var updateCmd = new MySqlCommand("UPDATE usuarios SET Password=@Password WHERE Username=@OldUsername", conn);
                updateCmd.Parameters.AddWithValue("@Password", usuarioActualizado.Password ?? "");
                updateCmd.Parameters.AddWithValue("@OldUsername", username);
                updateCmd.ExecuteNonQuery();

                usuarioActualizado.Username = username;
                return Ok(usuarioActualizado);
            }
        }
    }

    [HttpDelete("{username}")]
    public IActionResult Delete(string username)
    {
        using (var conn = DB.GetConnection())
        {
            var checkCmd = new MySqlCommand("SELECT COUNT(*) FROM usuarios WHERE Username=@Username", conn);
            checkCmd.Parameters.AddWithValue("@Username", username);
            var exists = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0;

            if (!exists)
                return NotFound("Usuario no encontrado.");

            var cmd = new MySqlCommand("DELETE FROM usuarios WHERE Username=@Username", conn);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.ExecuteNonQuery();
        }

        return Ok("Usuario eliminado exitosamente.");
    }
}
