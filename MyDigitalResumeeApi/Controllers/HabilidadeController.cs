using Dapper;
using Microsoft.AspNetCore.Mvc;
using MyDigitalResumeeApi.Entidade;
using System.Data.SqlClient;

namespace MyDigitalResumeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabilidadeController : ControllerBase
    {
        private readonly SqlConnection _connection;

        public HabilidadeController(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("MyDigitalResumeeDb"));
        }
        
        [HttpGet]
        public ActionResult ObterTodasHabilidadesPorUsuario(int usuarioId)
        {
            try
            {
                IEnumerable<Habilidade> habilidades = _connection.Query<Habilidade>("SELECT Titulo, Nivel, UsuarioId FROM Habilidade WHERE UsuarioId = @UsuarioId", new { @UsuarioId = usuarioId });

                if (habilidades is null)
                    return Ok(Enumerable.Empty<Habilidade>());

                return Ok(habilidades);
            }
            catch(SqlException sqlException)
            {
                return BadRequest(sqlException.Message);
            }
            catch (Exception)
            {
                return BadRequest("Sistema indisponível no momento");
            }
        }

        /// <summary>
        /// Inserir uma nova habilidade
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// {
        ///     "titulo":"C#",
        ///     "nivel": 1,
        ///     "usuarioId": 0
        /// }
        /// </remarks>
        /// <param name="habilidade"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InserirNovaHabilidade(Habilidade habilidade)
        {
            try
            {
                _connection.Execute("INSERT INTO Habilidade VALUES (@Titulo, @Nivel,@UsuarioId)", habilidade);
                return Ok();
            }
            catch (SqlException sqlException)
            {
                return BadRequest(sqlException.Message);
            }
            catch (Exception)
            {
                return BadRequest("Sistema indisponível no momento");
            }
        }

        [HttpPut]
        public ActionResult AtualizarHabilidade(Habilidade habilidade)
        {
            try
            {
                _connection.Execute("UPDATE Habilidade SET Titulo = @Titulo, Nivel = @Nivel WHERE Id = @Id", habilidade);
                return Ok();
            }
            catch (SqlException sqlException)
            {
                return BadRequest(sqlException.Message);
            }
            catch (Exception)
            {
                return BadRequest("Sistema indisponível no momento");
            }
        }

        [HttpDelete]
        public ActionResult DeletarHabilidade(int id)
        {
            try
            {
                _connection.Execute("DELETE FROM Habilidade WHERE Id = @Id", new { @Id = id });
                return Ok(); 
            }
            catch (SqlException sqlException)
            {
                return BadRequest(sqlException.Message);
            }
            catch (Exception)
            {
                return BadRequest("Sistema indisponível no momento");
            }
        }
    }
}
