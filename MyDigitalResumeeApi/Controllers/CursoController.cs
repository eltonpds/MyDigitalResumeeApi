using Dapper;
using Microsoft.AspNetCore.Mvc;
using MyDigitalResumeeApi.Entidade;
using System.Data.SqlClient;

namespace MyDigitalResumeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly SqlConnection _connection;

        public CursoController(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("MyDigitalResumeeDb"));
        }

        [HttpGet]
        public IActionResult ObterTodosCursosPorUsuarioId(int usuarioId)
        {
            try
            {
                IEnumerable<Curso> cursos = _connection.Query<Curso>("SELECT Id, Titulo, Instituicao, CargaHoraria, Link, UsuarioId FROM Curso WHERE UsuarioId = @UsuarioId", new
                {
                    @UsuarioId = usuarioId
                });
                return Ok(cursos);
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
        /// Inserir um novo curso para o usuário
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///     {
        ///         "titulo": "Asp Net Core e Angular Para Iniciantes com Loja Virtual",
        ///         "instituicao": "Udemy",
        ///         "cargaHoraria": "28.8",
        ///         "link": "",
        ///         "usuarioId": 0
        ///     }
        /// </remarks>
        /// <param name="curso"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InserirNovoCurso(Curso curso)
        {
            try
            {
                _connection.Execute("INSERT INTO Curso VALUES (@Titulo, @Instituicao, @CargaHoraria, @Link, @UsuarioId)", curso);
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

        /// <summary>
        /// Atualizar o curso
        /// </summary>        
        /// <remarks>
        /// Exemplo de requisição:
        ///     {
        ///         "id": 1,
        ///         "titulo": "Asp Net Core e Angular Para Iniciantes com Loja Virtual",
        ///         "instituicao": "Udemy",
        ///         "cargaHoraria": "28.8",
        ///         "link": "https://www.udemy.com/certificate/UC-3957ab44-b1de-45f8-b994-5b4e4699154c/"
        ///     }   
        /// </remarks>
        /// <param name="curso"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult AtualizarCurso(Curso curso)
        {
            try
            {
                _connection.Execute("UPDATE Curso SET Titulo = @Titulo, Instituicao = @Instituicao, CargaHoraria = @CargaHoraria, Link = @Link WHERE Id = @Id", curso);
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
        public ActionResult DeletarCurso(int id)
        {
            try
            {
                _connection.Execute("DELETE FROM Curso WHERE Id = @Id", new
                {
                    @Id = id
                });
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
