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
            try
            {
                IEnumerable<Usuario> usuarios = _sqlConnection.Query<Usuario>("SELECT Nome, Email, Cpf, DataNascimento, Sexo, Celular, Cep, Endereco, Bairro, Cidade, Estado, Pais FROM Usuario");

                return Ok(usuarios);
            }
            catch (SqlException)
            {
                return BadRequest("Erro ao tentar conectar com o banco de dados!");
            }
            catch (Exception)
            {
                return BadRequest("Sistema indisponível no momento");
            }
        }

        /// <summary>
        /// Retornar um usuário de acordo com o Id informado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Usuario> GetUsuarioPorId(int id)
        {
            try
            {
                Usuario usuario = _sqlConnection.QueryFirst<Usuario>("SELECT Nome, Email, Cpf, DataNascimento, Sexo, Celular, Cep, Endereco, Bairro, Cidade, Estado, Pais FROM Usuario WHERE Id = @Id", new
                {
                    @Id = id
                });
                return Ok(usuario);
            }
            catch (SqlException)
            {
                return BadRequest("Erro ao tentar conectar com o banco de dados!");
            }
            catch (Exception)
            {
                return BadRequest("Sistema indisponível no momento");
            }
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
            try
            {
                _sqlConnection.Execute("INSERT INTO Usuario VALUES(@Nome, @Email, @Senha, @Cpf, @DataNascimento, @Sexo, @Celular, @Cep, @Endereco, @Bairro, @Cidade, @Estado, @Pais)", usuario);
                return Ok();
            }
            catch (SqlException)
            {
                return BadRequest("Erro ao tentar conectar com o banco de dados!");
            }
            catch (Exception)
            {
                return BadRequest("Sistema indisponível no momento");
            }
        }

        [HttpPut]
        public ActionResult AtualizarUsuario(Usuario usuario)
        {
            try
            {
                _sqlConnection.Execute("UPDATE Usuario SET Nome = @Nome, Email = @Email, Senha = @Senha, Cpf = @Cpf, DataNascimento = @DataNascimento, Sexo = @Sexo, Celular = @Celular, Cep = @Cep, Endereco = @Endereco, Bairro = @Bairro, Cidade = @Cidade, Estado = @Estado, Pais = @Pais WHERE Id = @Id", usuario);
                return Ok();
            }
            catch (SqlException)
            {
                return BadRequest("Erro ao tentar conectar com o banco de dados!");
            }
            catch (Exception)
            {
                return BadRequest("Sistema indisponível no momento");
            }
        }

        [HttpDelete]
        public ActionResult DeletarUsuario(int id)
        {
            try
            {
                _sqlConnection.Execute("DELETE FROM Usuario WHERE Id = @Id", new { @Id = id });
                return Ok();
            }
            catch (SqlException)
            {
                return BadRequest("Erro ao tentar conectar com o banco de dados!");
            }
            catch (Exception)
            {
                return BadRequest("Sistema indisponível no momento");
            }
        }
    }
}
