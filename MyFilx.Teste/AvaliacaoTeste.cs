using AutoMapper;
using Moq;
using MyFilx.Teste.DadosFack;
using MyFlix.AppServer;
using MyFlix.AppServer.Interfaces;
using MyFlix.AppServer.ViewModel;
using MyFlix.Dados.Entidades;
using MyFlix.Infra.Notificacoes;
using MyFlix.Repository.Interfaces;
using MyFlix.Serve;
using MyFlix.Serve.Interfaces;

namespace MyFilx.Teste
{
    public class AvaliacaoTeste
    {
        private readonly Mock<IAvaliacaoAppServe> _favaliacaoAppServe;
        private readonly Mock<IAvaliacaoServe> _avaliacaoServe;
        private readonly Mock<IAvaliacaoRepositorio> _avaliacaoRepositorio;
        private readonly IMapper _mapper;
        public AvaliacaoTeste()
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Avaliacao, AvaliacaoViewModel>();
                cfg.CreateMap<AvaliacaoViewModel, Avaliacao>();
            });

            _mapper = config.CreateMapper();
            _avaliacaoServe = new Mock<IAvaliacaoServe>();
            _avaliacaoRepositorio = new Mock<IAvaliacaoRepositorio>();
            _favaliacaoAppServe = new Mock<IAvaliacaoAppServe>();
        }



        [Fact(DisplayName = "Buscar todas as Avaliacoes")]
        [Trait("Avaliacao", "Avaliacao teste")]
        public async Task BuscarTodos_AvaliacaoExistente_RetornaAvaliacaoCorreta()
        {
         
            var listaAvaliacao = AvaliacaoFactory.DadosListaAvaliacao();
            var listaAvaliacaoView = _mapper.Map<List<AvaliacaoViewModel>>(listaAvaliacao);

            _avaliacaoRepositorio.Setup(x => x.ObterTodosAsync()).ReturnsAsync(listaAvaliacao);
            _favaliacaoAppServe.Setup(x => x.ObterTodosAsync()).ReturnsAsync(listaAvaliacaoView);

            _avaliacaoServe.Setup(x => x.ObterTodosAsync()).ReturnsAsync(listaAvaliacao);
            var avaliacaoAppService = new AvaliacaoAppServe(_avaliacaoServe.Object, _mapper);

            var resultado = await avaliacaoAppService.ObterTodosAsync();
            Assert.NotNull(resultado);
        }

        [Fact(DisplayName = "Buscar  Avaliacoes por Id")]
        [Trait("Avaliacao", "Avaliacao teste")]
        public async Task BuscarAvaliacao_AvaliacaoPorId_Correta()
        {

            var avaliacaoView = AvaliacaoFactory.DadosAvaliacaoView();
            var avaliacao = _mapper.Map<Avaliacao>(avaliacaoView);

            _avaliacaoRepositorio.Setup(x => x.ObterPorIdAsync(1)).ReturnsAsync(avaliacao);
            _favaliacaoAppServe.Setup(x => x.ObterPorIdAsync(1)).ReturnsAsync(avaliacaoView);

            _avaliacaoServe.Setup(x => x.ObterPorIdAsync(1)).ReturnsAsync(avaliacao);
            var avaliacaoAppService = new AvaliacaoAppServe(_avaliacaoServe.Object, _mapper);

            var resultado = await avaliacaoAppService.ObterTodosAsync();

            var retornoNotificacao = Notificacao.GetErrors();

            Assert.True(!retornoNotificacao.Any());
            Assert.NotNull(resultado);
        }


    }

}
