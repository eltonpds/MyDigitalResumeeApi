using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyDigitalResumeeApi.Entidade;
using System.Data.SqlClient;

namespace MyDigitalResumeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private SqlConnection _sqlConnection;

        public UsuarioController(IConfiguration configuration)
        {
            _sqlConnection = new SqlConnection(configuration.GetConnectionString("MyDigitalResumeeDb")); ;
        }

        /// <summary>
        /// Retorna todos os usuários cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> GetAllUsers()
        {
            IEnumerable<Usuario> usuarios = _sqlConnection.Query<Usuario>("SELECT Nome, Email, Cpf, DataNascimento, Sexo, Celular, Cep, Endereco, Bairro, Cidade, Estado, Pais");

            return Ok(usuarios);
        }

        /// <summary>
        /// Retornar um usuário de acordo com o Id informado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Usuario> GetUsuarioPorId(int id)
        {
            Usuario usuario = _sqlConnection.QueryFirst<Usuario>("SELECT Nome, Email, Cpf, DataNascimento, Sexo, Celular, Cep, Endereco, Bairro, Cidade, Estado, Pais FROM Usuario WHERE Id = @Id", new
            {
                @Id = id
            });
            return Ok(usuario);
        }

        /// <summary>
        /// Inserir um novo usuário
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///     POST /Usuario
        ///     {
        ///        "nome": "Elton Santos",
        ///        "email": "teste@teste.com",
        ///        "senha": "123456",
        ///        "cpf": "12345678901",
        ///        "dataNascimento": "1994-03-18",
        ///        "sexo": 0,
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
            _sqlConnection.Execute("INSERT INTO Usuario VALUES(@Nome, @Email, @Senha, @Cpf, @DataNascimento, @Sexo, @Celular, @Cep, @Endereco, @Bairro, @Cidade, @Estado, @Pais)", usuario);
            return Ok();
        }

        [HttpPut]
        public ActionResult AtualizarUsuario(Usuario usuario)
        {
            _sqlConnection.Execute("UPDATE Usuario SET Nome = @Nome, Email = @Email, Senha = @Senha, Cpf = @Cpf, DataNascimento = @DataNascimento, Sexo = @Sexo, Celular = @Celular, Cep = @Cep, Endereco = @Endereco, Bairro = @Bairro, Cidade = @Cidade, Estado = @Estado, Pais = @Pais WHERE Id = @Id", usuario);
            return Ok();
        }

        [HttpDelete]
        public ActionResult DeletarUsuario(int id)
        {
            _sqlConnection.Execute("DELETE FROM Usuario WHERE Id = @Id", new { @Id = id });
            return Ok();
        }
    }
}
