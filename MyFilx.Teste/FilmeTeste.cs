using AutoMapper;
using Moq;
using MyFilx.Teste.DadosFack;
using MyFlix.AppServer;
using MyFlix.AppServer.Interfaces;
using MyFlix.AppServer.ViewModel;
using MyFlix.Dados.Entidades;
using MyFlix.Infra.Helpers;
using MyFlix.Infra.Notificacoes;
using MyFlix.Repository.Interfaces;
using MyFlix.Serve;
using MyFlix.Serve.Interfaces;

namespace MyFilx.Teste
{
    public class FilmeTeste
    {
        private readonly Mock<IFilmesAppServe> _filmesAppServe;
        private readonly Mock<IFilmesServe> _filmesServe;
        private readonly Mock<IFilmesRepositorio> _filmesServeRepositorio;
        private readonly IMapper _mapper;
        public FilmeTeste()
        {
            _filmesAppServe = new Mock<IFilmesAppServe>();
            _filmesServe = new Mock<IFilmesServe>();
            _filmesServeRepositorio = new Mock<IFilmesRepositorio>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Filmes, FilmesViewModel>();
                cfg.CreateMap<FilmesViewModel, Filmes>();
            });

            _mapper = config.CreateMapper();

        }

        [Fact(DisplayName = "Buscar todos os Filmes")]
        [Trait("Filmes", "Filmes teste")]
        public async Task BuscarTodos_FilmesExistente_RetornaFilmesCorreta()
        {
            var listaFilmes = FilmesFactory.DadosListaFilmes();
            var listaFilmesView = FilmesFactory.DadosListaFilmesView();

            _filmesServeRepositorio.Setup(x => x.ObterTodosAsync()).ReturnsAsync(listaFilmes);
            _filmesAppServe.Setup(x => x.ObterTodosAsync()).ReturnsAsync(listaFilmesView);
            _filmesServe.Setup(x => x.ObterTodosAsync()).ReturnsAsync(listaFilmes);

            var filmesAppService = new FilmesAppServe(_filmesServe.Object, _mapper);

            var resultado = await filmesAppService.ObterTodosAsync();

            Assert.NotNull(resultado);
        }

        
        [Fact(DisplayName = "Buscar Filmes por Id")]
        [Trait("Filmes", "Filmes teste")]
        public async Task BuscarPorId_FilmesExistente_RetornaFilmesCorreto()
        {
            var filmesView = FilmesFactory.DadosFilmesView();
            var filmes = FilmesFactory.DadosFilmes();


            _filmesAppServe.Setup(x => x.ObterPorIdAsync(1)).ReturnsAsync(filmesView);
            _filmesServe.Setup(x => x.ObterPorIdAsync(1)).ReturnsAsync(filmes);

            var filmesAppService = new FilmesAppServe(_filmesServe.Object, _mapper);
            var resultado = await filmesAppService.ObterPorIdAsync(1);


            Assert.NotNull(resultado);
            Assert.Equal(filmesView.Id, resultado.Id);
            Assert.Equal(filmesView.Nome, resultado.Nome);
            Assert.Equal(filmesView.Titulo, resultado.Titulo);
            Assert.Equal(filmesView.Genero, resultado.Genero);
            Assert.Equal(filmesView.AnoLancamento, filmes.AnoLancamento.ConvertDateToString());

            _filmesServe.Verify(x => x.ObterPorIdAsync(1), Times.Once);
        }

        [Fact(DisplayName = "Filmes deve ser adicionado com sucesso")]
        [Trait("Filmes", "Filmes testes")]
        public async Task CadastarAsync_AdicionarFilmes_Correto()
        {
            var filmes = FilmesFactory.DadosFilmes();

            _filmesServeRepositorio.Setup(x => x.CadastrarAsync(filmes));
            _filmesServe.Setup(x => x.CadastrarAsync(filmes));

            var filmesService = new FilmesServe(_filmesServeRepositorio.Object);
            await filmesService.CadastrarAsync(filmes);
            var retornoNotificacao = Notificacao.GetErrors();


            Assert.True(!retornoNotificacao.Any());
            _filmesServeRepositorio.Verify(x => x.CadastrarAsync(It.IsAny<Filmes>()), Times.Once());
             
        }

        [Fact(DisplayName = "Filmes nao deve ser adicionado com sucesso")]
        [Trait("Filmes", "Filmes testes")]
        public async Task CadastarAsync_AdicionarFilmes_InCorreto()
        {

            var filmesView = new FilmesViewModel{};
            var filmes = _mapper.Map<Filmes>(filmesView);

            _filmesServeRepositorio.Setup(x => x.CadastrarAsync(filmes));
            _filmesServe.Setup(x => x.CadastrarAsync(filmes));

            var filmesService = new FilmesServe(_filmesServeRepositorio.Object);
            await filmesService.CadastrarAsync(filmes);
            var retornoNotificacao = Notificacao.GetErrors();

            var validarNome = retornoNotificacao.Contains("Nome é obrigatório.");
            var validarTitulo = retornoNotificacao.Contains("Titulo é obrigatório.");
            var validarGenero = retornoNotificacao.Contains("Genero é obrigatório.");
            var validarAnoLancamento = retornoNotificacao.Contains("Ano de Lançamento é obrigatório.");

            Assert.True(validarNome);
            Assert.True(validarTitulo);
            Assert.True(validarGenero);
            Assert.True(validarAnoLancamento);

        }

        [Fact(DisplayName = "Filmes deve ser atualizado com sucesso")]
        [Trait("Filmes", "Filmes testes")]
        public async Task AtualizarAsync_AtualizarFilmes_Correto()
        {

            var filmes = FilmesFactory.DadosFilmes();

            _filmesServeRepositorio.Setup(x => x.AtualizarAsync(filmes));
            _filmesServeRepositorio.Setup(x => x.ObterPorIdAsync(2)).ReturnsAsync(filmes);
            _filmesServe.Setup(x => x.AtualizarAsync(filmes));

            var filmesService = new FilmesServe(_filmesServeRepositorio.Object);
            var filmeCadastrado = await _filmesServeRepositorio.Object.ObterPorIdAsync(2);

            await filmesService.AtualizarAsync(filmeCadastrado);
            var retornoNotificacao = Notificacao.GetErrors();

            Assert.True(!retornoNotificacao.Any());
            _filmesServeRepositorio.Verify(x => x.AtualizarAsync(It.IsAny<Filmes>()), Times.Once());

        }

        [Fact(DisplayName = "Filmes não deve ser atualizado com sucesso")]
        [Trait("Filmes", "Filmes testes")]
        public async Task AtualizarAsync_AtualizarFilmes_InCorreto()
        {

            var filmesView = FilmesFactory.DadosFilmesView();
            var filmes = _mapper.Map<Filmes>(filmesView);

            _filmesServeRepositorio.Setup(x => x.AtualizarAsync(filmes));
            _filmesServeRepositorio.Setup(x => x.ObterPorIdAsync(2)).ReturnsAsync(filmes);
            _filmesServe.Setup(x => x.AtualizarAsync(filmes));

            var filmesService = new FilmesServe(_filmesServeRepositorio.Object);
            var filmeCadastrado = await _filmesServeRepositorio.Object.ObterPorIdAsync(2);

            await filmesService.AtualizarAsync(new Filmes { });
            var retornoNotificacao = Notificacao.GetErrors();
            var validarIdMensagem = retornoNotificacao.Contains("Filme não encontrado");

            Assert.True(validarIdMensagem);

        }



        [Fact(DisplayName = "Deve Deletar Filmes por Id")]
        [Trait("Filmes", "Filmes teste")]
        public async Task DeletarAsync_DeletarFilmes_Correto()
        {

            var filmes = FilmesFactory.DadosFilmes();

            _filmesServeRepositorio.Setup(x => x.ObterPorIdAsync(1)).ReturnsAsync(filmes);
            _filmesServeRepositorio.Setup(x => x.DeletarAsync(filmes));
            _filmesServe.Setup(x => x.DeletarAsync(1));

            var filmesServe = new FilmesServe(_filmesServeRepositorio.Object);
            var filmeCadastrado = await _filmesServeRepositorio.Object.ObterPorIdAsync(1);


            await filmesServe.DeletarAsync(filmeCadastrado.Id);
            var retornoNotificacao = Notificacao.GetErrors();

            Assert.True(!retornoNotificacao.Any());

        }


        [Fact(DisplayName = "Não deve Deletar Filmes por Id")]
        [Trait("Filmes", "Filmes teste")]
        public async Task DeletarAsync_DeletarFilmes_InCorreto()
        {

            var filmes = FilmesFactory.DadosFilmes();

            _filmesServeRepositorio.Setup(x => x.ObterPorIdAsync(1)).ReturnsAsync(filmes);
            _filmesServeRepositorio.Setup(x => x.DeletarAsync(filmes));
            _filmesServe.Setup(x => x.DeletarAsync(1));

            var filmesServe = new FilmesServe(_filmesServeRepositorio.Object);
            var filmeCadastrado = await _filmesServeRepositorio.Object.ObterPorIdAsync(1);

            await filmesServe.DeletarAsync(0);
            var retornoNotificacao = Notificacao.GetErrors();
            var validarMensagem = retornoNotificacao.Contains("Filme não encontrado");
            Assert.True(validarMensagem);

        }
    }
}
