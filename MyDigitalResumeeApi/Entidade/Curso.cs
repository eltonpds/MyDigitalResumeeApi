namespace MyDigitalResumeeApi.Entidade
{
    public class Curso
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Instituicao { get; set; }
        public double CargaHoraria { get; set; }
        public string Link { get; set; }
        public int UsuarioId { get; set; }
    }
}
