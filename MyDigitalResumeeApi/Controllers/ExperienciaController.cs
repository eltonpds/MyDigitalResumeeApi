using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDigitalResumeeApi.Entidade;
using System.Data.SqlClient;

namespace MyDigitalResumeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienciaController : ControllerBase
    {
        private readonly SqlConnection _sqlConnection;

        public ExperienciaController(IConfiguration configuration)
        {
            _sqlConnection = new SqlConnection(configuration.GetConnectionString("MyDigitalResumeeDb"));
        }

        /// <summary>
        /// Retorna todas as experiências por usuário
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ObterTodasExperienciasPorUsuario(int usuarioId)
        {
            try
            {
                IEnumerable<Experiencia> experiencias = _sqlConnection.Query<Experiencia>("SELECT Cargo, Empresa, Endereco, InicioJornada, FimJornada, TrabalhaAtualmente, Atividade, RegimeContratacao, ModalidadeTrabalho, MostrarNoCurriculo FROM Experiencia WHERE UsuarioId = @UsuarioId", new
                {
                    @UsuarioId = usuarioId
                });
                return Ok(experiencias);
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
        /// Inserir uma nova Experiência
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///     POST /Experiencia
        ///     {
        ///        "cargo": "Desenvolvedor",
        ///        "empresa": "Empresa de Tecnologia",
        ///        "Endereco": "Recife PE",
        ///        "inicioJornada": "2018-10-10",
        ///        "fimJornada": "2020-04-10",
        ///        "trabalhaAtualmente": false,
        ///        "atividade": "Auxiliar o Time Scrum na criação de novas funcionalidades e correções de bugs no Back-End utilizando C#, criar Interface baseada na nova funcionalidade desenvolvida usando Angular 8, Bootstrap 4 e Prime NG, criar relatórios usando Report Builder e SQL Server, prestar suporte a Usuários via Service Desk e elaborar documentação do sistema utilizando Wiki JS",
        ///        "regimeContratacao": 1,
        ///        "modalidadeTrabalho": 0,
        ///        "mostrarNoCurriculo": true,
        ///        "usuarioId": 0
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SalvarExperiencia(Experiencia experiencia)
        {
            try
            {
                _sqlConnection.Execute("INSERT INTO Experiencia VALUES (@Cargo, @Empresa, @Endereco, @InicioJornada, @FimJornada, @TrabalhaAtualmente, @Atividade, @RegimeContratacao, @ModalidadeTrabalho, @MostrarNoCurriculo, @UsuarioId)", experiencia);
                return Ok();
            }
            catch (SqlException)
            {
                return BadRequest("Erro ao se conectar com o banco de dados");
            }
            catch (Exception)
            {
                return BadRequest("Erro de sistema");
            }
        }

        [HttpPut]
        public ActionResult AtualizarExperiencia(Experiencia experiencia)
        {
            try
            {
                _sqlConnection.Execute("UPDATE Experiencia SET Cargo = @Cargo, Empresa = @Empresa, Endereco = @Endereco, InicioJornada = @InicioJornada, FimJornada = @FimJornada, TrabalhaAtualmente = @TrabalhaAtualmente, Atividade = @Atividade, RegimeContratacao = @RegimeContratacao, ModalidadeTrabalho = @ModalidadeTrabalho, MostrarNoCurriculo = @MostrarNoCurriculo", experiencia);

                return Ok();
            }
            catch (SqlException)
            {
                return BadRequest("Erro ao se conectar com o banco de dados");
            }
            catch(Exception) 
            {
                return BadRequest("Erro de sistema");
            }
        }

        [HttpDelete]
        public ActionResult DeletarExperiencia(int id)
        {
            try
            {
                _sqlConnection.Execute("DELETE FROM Experiencia WHERE Id = @Id", new { @Id = id });
                return Ok();
            }
            catch (SqlException)
            {
                return BadRequest("Erro ao se conectar com o banco de dados");
            }
            catch(Exception)
            {
                return BadRequest("Erro de sistema");
            }
        }
    }
}
