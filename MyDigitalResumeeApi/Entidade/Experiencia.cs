using MyDigitalResumeeApi.Enums;

namespace MyDigitalResumeeApi.Entidade
{
    public class Experiencia
    {
        public int Id { get; set; }
        public string Cargo { get; set; }
        public string Empresa { get; set; }
        public string Endereco { get; set; }
        public DateTime InicioJornada { get; set; }
        public DateTime? FimJornada { get; set; }
        public bool TrabalhaAtualmente { get; set; }
        public string Atividade { get; set; }
        public RegimeContratacaoEnum RegimeContratacao { get; set; }
        public ModalidadeTrabalhoEnum ModalidadeTrabalho { get; set; }
        public bool? MostrarNoCurriculo { get; set; }
        public int UsuarioId { get; set; }
    }
}
