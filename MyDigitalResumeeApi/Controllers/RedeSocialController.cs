using Dapper;
using Microsoft.AspNetCore.Mvc;
using MyDigitalResumeeApi.Entidade;
using System.Data.SqlClient;

namespace MyDigitalResumeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedeSocialController : ControllerBase
    {
        private readonly SqlConnection _connection;

        public RedeSocialController(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("MyDigitalResumeeDb"));
        }

        /// <summary>
        /// Retorna todas as redes sociais associadas a um determinado usuário
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ObterTodasRedesSociaisPorUsuarioId (int usuarioId)
        {
            try
            {
                IEnumerable<RedeSocial> redesSociais = _connection.Query<RedeSocial>("SELECT Titulo, Link FROM RedeSocial WHERE UsuarioId = @UsuarioId", new { @UsuarioId = usuarioId });

                if (redesSociais is null)
                    return Ok(Enumerable.Empty<RedeSocial>());

                return Ok(redesSociais);
            }
            catch (SqlException)
            {
                return BadRequest("Erro ao conectar com o banco de dados");
            }
            catch(Exception) 
            {
                return BadRequest("Sistema indisponível no momento");
            }
        }

        /// <summary>
        /// Inserir nova rede social 
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// {
        ///     "titulo":"Git Hub",
        ///     "link": "https://github.com/eltonpds/",
        ///     "usuarioId": 0
        /// }
        /// </remarks>
        /// <param name="redeSocial"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InserirNovaRedeSocial(RedeSocial redeSocial)
        {
            try
            {
                _connection.Execute("INSERT INTO RedeSocial VALUES (@Titulo, @Link, @UsuarioId)", redeSocial);
                return Ok();
            }
            catch (SqlException)
            {
                return BadRequest("Erro ao conectar com o banco de dados");
            }
            catch(Exception)
            {
                return BadRequest("Sistema indisponível no momento");
            }
        }

        [HttpPut]
        public ActionResult AtualizarRedeSocial(RedeSocial redeSocial)
        {
            try
            {
                _connection.Execute("UPDATE RedeSocial SET Titulo = @Titulo, Link = @Link WHERE UsuarioId = @UsuarioId", redeSocial);

                return Ok();
            }
            catch (SqlException)
            {
                return BadRequest("Erro ao conectar com o banco de dados");
            }
            catch (Exception)
            {
                return BadRequest("Sistema indisponível no momento");
            }
        }

        [HttpDelete]
        public ActionResult DeletarRedeSocial(int id)
        {
            try
            {
                _connection.Execute("DELETE FROM RedeSocial WHERE Id = @Id", new { @Id = id });
                return Ok();
            }
            catch (SqlException sql)
            {
                return BadRequest(sql.Message);
            }
            catch (Exception)
            {
                return BadRequest("Sistema indisponível no momento");
            }
        }
    }
}
