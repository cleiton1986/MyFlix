namespace MyFlix.AppServer.ViewModel
{
    public class FilmesViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public string AnoLancamento { get; set; }
        public bool Assistido { get; set; }
        public int? Nota { get; set; }
    }
}
