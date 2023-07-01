using Dapper;
using Microsoft.AspNetCore.Mvc;
using MyDigitalResumeeApi.Entidade;
using System.Data.SqlClient;

namespace MyDigitalResumeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;

        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("MyDigitalResumeeDb"));
        }

        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> GetAllUsers()
        {
            IEnumerable<Usuario> usuarios = _connection.Query<Usuario>("SELECT Nome, Email, Cpf, DataNascimento, Celular, Cep, Endereco, Bairro, Cidade, Estado, Pais");

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public ActionResult<Usuario> GetUsuarioPorId(int id)
        {
            Usuario usuario = _connection.QueryFirst<Usuario>("SELECT Nome, Email, Cpf, DataNascimento, Celular, Cep, Endereco, Bairro, Cidade, Estado, Pais FROM Usuario WHERE Id = @Id", new
            {
                @Id = id
            });
            return Ok(usuario);
        }

        /// <summary>
        /// Inserir um novo usuário
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /Usuario
        ///     {
        ///        "nome": "Elton Santos",
        ///        "email": "teste@teste.com",
        ///        "senha": "123456",
        ///        "cpf": "12345678901",
        ///        "dataNascimento": "18/03/1994",
        ///        "celular": "81981336252",
        ///        "cep": "",
        ///        "endereco": "",
        ///        "bairro": "",
        ///        "cidade": "Jaboatão dos Guararapes",
        ///        "estado": "Pernambuco",
        ///        "pais": "Brasil"
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InserirUsuario(Usuario usuario)
        {
            _connection.Execute("INSERT INTO Usuario VALUES(@Nome, @Email, @Senha, @Cpf, @DataNascimento, @Celular, @Cep, @Endereco, @Bairro, @Cidade, @Estado, @Pais)", usuario);
            return Ok();
        }

        [HttpPut]
        public ActionResult AtualizarUsuario(Usuario usuario)
        {
            _connection.Execute("UPDATE Usuario SET Nome = @Nome, Email = @Email, Senha = @Senha, Cpf = @Cpf, DataNascimento = @DataNascimento, Celular = @Celular, Cep = @Cep, Endereco = @Endereco, Bairro = @Bairro, Cidade = @Cidade, Estado = @Estado, Pais = @Pais WHERE Id = @Id", usuario);
            return Ok();
        }

        [HttpDelete]
        public ActionResult DeletarUsuario(int id)
        {
            _connection.Execute("DELETE FROM Usuario WHERE Id = @Id", new { @Id = id });
            return Ok();
        }
    }
}
