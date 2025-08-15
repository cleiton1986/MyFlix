using MyFlix.AppServer.ViewModel;
using MyFlix.Dados.Entidades;

namespace MyFilx.Teste.DadosFack
{
    public static class FilmesFactory
    {
        public static List<Filmes> DadosListaFilmes()
        {
            return new List<Filmes>
            {
                new Filmes
                {
                    Id = 1,
                    Nome = "João e o Pé de Feijão",
                    AnoLancamento = new DateTime(2024, 1, 1),
                    Titulo = "João e o Pé de Feijão - Aventura Mágica",
                    Genero = "Aventura",
                    Avaliacao = new Avaliacao{Nota = 4}

                },
                new Filmes
                {
                    Id = 2,
                    Nome = "missão impossivel 8",
                    AnoLancamento = new DateTime(2025, 1, 1),
                    Titulo = "missão impossivel 8 - Ação",
                    Genero = "Ação",
                    Avaliacao = new Avaliacao{Nota = 5}

                },
            };
        }

        public static FilmesViewModel DadosFilmesView()
        {
            return new FilmesViewModel
            {
                Id = 2,
                Nome = "missão impossivel 8",
                Titulo = "missão impossivel 8 - Ação",
                Genero = "Ação",
                AnoLancamento = "01/01/2025",
                Nota = 5
               
            };
        }
        public static Filmes DadosFilmes()
        {
            return new Filmes
            {
                Id = 2,
                Nome = "missão impossivel 8",
                Titulo = "missão impossivel 8 - Ação",
                Genero = "Ação",
                AnoLancamento = new DateTime(2025, 1, 1),
                Avaliacao = new Avaliacao { Nota = 5 }
            };
        }

        public static List<FilmesViewModel> DadosListaFilmesView()
        {
            return new List<FilmesViewModel>
            {
                new FilmesViewModel
                {
                    Id = 2,
                    Nome = "missão impossivel 8",
                    Titulo = "missão impossivel 8 - Ação",
                    Genero = "Ação",
                    AnoLancamento = "05/08/2000",
                    Nota = 5
                }
            };
        }
    }
}
