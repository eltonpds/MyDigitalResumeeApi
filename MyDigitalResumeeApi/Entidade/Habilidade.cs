using MyDigitalResumeeApi.Enums;

namespace MyDigitalResumeeApi.Entidade
{
    public class Habilidade
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public HabilidadeEnum Nivel { get; set; }
        public int UsuarioId { get; set; }
    }
}
