using MyFlix.AppServer.ViewModel;
using MyFlix.Dados.Entidades;

namespace MyFilx.Teste.DadosFack
{
    public class AvaliacaoFactory
    {
        public static List<Avaliacao> DadosListaAvaliacao()
        {
            return new List<Avaliacao>
            {
                new Avaliacao
                {
                    Id = 1,
                    Nota = 5,
                    Assistido = true,
                },
                new Avaliacao
                {
                    Id = 2,
                    Nota = 3,
                    Assistido = false,
                },
            };
        }
        public static AvaliacaoViewModel DadosAvaliacaoView()
        {
            return new AvaliacaoViewModel
            {
                Id = 1,
                Nota = 5,
                Assistido = true,
                FilmesId = 1
            };
        }

        public static Avaliacao DadosAvaliacao()
        {
            return new Avaliacao
            {
                Id = 1,
                Nota = 5,
                Assistido = true,
            };
        }
    }
}
