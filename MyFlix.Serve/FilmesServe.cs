using MyFlix.Dados.Entidades;
using MyFlix.Infra.Helpers;
using MyFlix.Infra.Notificacoes;
using MyFlix.Repository.Interfaces;
using MyFlix.Serve.Interfaces;

namespace MyFlix.Serve
{
    public class FilmesServe : IFilmesServe
    {
        private readonly IFilmesRepositorio _filmesRepositorio;

        public FilmesServe(IFilmesRepositorio filmesRepositorio)
        {
            _filmesRepositorio = filmesRepositorio;
        }

        public async Task<Filmes> AtualizarAsync(Filmes filmes)
        {
            var filmesAtualizar = new Filmes();

            try
            {
                if (filmes == null || Extensioes.ValidateInt(filmes.Id, "Filme não encontrado"))
                    return filmesAtualizar;

                filmesAtualizar = await ObterPorIdAsync(filmes.Id);

                if (filmesAtualizar != null && (filmes.Validate() && filmes.Avaliacao.Validate()))
                {
                    filmesAtualizar.Nome = filmes.Nome;
                    filmesAtualizar.Titulo = filmes.Titulo;
                    filmesAtualizar.Genero = filmes.Genero;
                    filmesAtualizar.AnoLancamento = filmes.AnoLancamento;
                    filmesAtualizar.Avaliacao = filmes.Avaliacao;
                    await _filmesRepositorio.AtualizarAsync(filmesAtualizar);
                }

            }
            catch (Exception ex)
            {
                Notificacao.Notify($"Erro ao atualizar filmes. : {ex.Message} ", "AtualizarAsync");
            }
         
            return filmesAtualizar;
        }

        public async Task CadastrarAsync(Filmes filmes)
        {
            try
            {
                if (filmes != null && (filmes.Validate() && filmes.Avaliacao.Validate()))
                {
                    await _filmesRepositorio.CadastrarAsync(filmes);
                }
            }
            catch (Exception ex)
            {
                Notificacao.Notify($"Erro ao cadastrar filmes. : {ex.Message} ", "CadastrarAsync");
            }

        }

        public async Task DeletarAsync(int id)
        {
            var filmeCadastrado = await ObterPorIdAsync(id);
            try
            {
                if (Extensioes.ValidateInt(id, "Filme não encontrado"))
                    return;
            }
            catch (Exception ex)
            {

                Notificacao.Notify($"Erro ao deletar filmes. : {ex.Message} ", "DeletarAsync");
            }
          

            await _filmesRepositorio.DeletarAsync(filmeCadastrado);
        }

        public async Task<Filmes> ObterPorIdAsync(int id)
        {
            return await _filmesRepositorio.ObterPorIdAsync(id);
        }

        public async Task<IList<Filmes>> ObterTodosAsync()
        {
            var ListaFilmes = await _filmesRepositorio.ObterTodosAsync();
            return ListaFilmes.ToList();
        }
        public async Task<bool> ValidarSeExisteFilmesAsync()
        {
            return await _filmesRepositorio.ValidarSeExisteFilmesAsync();
        }
    }
}
