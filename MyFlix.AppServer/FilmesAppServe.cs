using AutoMapper;
using MyFlix.AppServer.Interfaces;
using MyFlix.AppServer.ViewModel;
using MyFlix.Dados.Entidades;
using MyFlix.Serve.Interfaces;

namespace MyFlix.AppServer
{
    public class FilmesAppServe: IFilmesAppServe
    {
        private readonly IFilmesServe _filmesServe;
        private readonly IMapper _mapper;
        public FilmesAppServe(IFilmesServe filmesServe, IMapper mapper)
        {
            _filmesServe = filmesServe;
            _mapper = mapper;
        }

        public async Task<FilmesViewModel> AtualizarAsync(FilmesViewModel filmesView)
        {
            var filmes = _mapper.Map<Filmes>(filmesView);
            filmesView = _mapper.Map<FilmesViewModel>(await _filmesServe.AtualizarAsync(filmes));
            return filmesView;
        }

        public async Task CadastrarAsync(FilmesViewModel filmesView)
        {
            var filmes = _mapper.Map<Filmes>(filmesView);
            await _filmesServe.CadastrarAsync(filmes);
        }

        public async Task DeletarAsync(int id)
        {
            await _filmesServe.DeletarAsync(id);
        }

        public async Task<FilmesViewModel> ObterPorIdAsync(int id)
        {
            var filmes = await _filmesServe.ObterPorIdAsync(id);
            var filmesView = _mapper.Map<FilmesViewModel>(filmes);
            return filmesView;
        }

        public async Task<IList<FilmesViewModel>> ObterTodosAsync()
        {
            var listaFilmes = await _filmesServe.ObterTodosAsync();
            var listaFilmesView = _mapper.Map<List<FilmesViewModel>>(listaFilmes);
            return listaFilmesView;
        }

        public async Task<bool> ValidarSeExisteFilmesAsync()
        {
            return await _filmesServe.ValidarSeExisteFilmesAsync();
        }
    }
}
