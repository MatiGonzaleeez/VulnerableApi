using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

[ApiController]
[Route("[controller]")]
public class ClientsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetClient()
    {
        var id = HttpContext.Request.Query["id"];
        var sql = $"SELECT * FROM Clients WHERE Id = {id}";
        using var connection = new SqlConnection("Server=mydb;Database=test;User Id=sa;Password=MyS3cret!");
        connection.Open();
        using var command = new SqlCommand(sql, connection);
        var reader = command.ExecuteReader();
        return Ok("Query executed (vulnerable to SQL Injection)");
    }

    [HttpPost]
    public IActionResult HashPassword([FromBody] string password)
    {
        var md5 = MD5.Create();
        var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Ok("Password hashed with MD5 (insecure)");
    }
}
