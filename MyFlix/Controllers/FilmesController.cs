using Microsoft.AspNetCore.Mvc;
using MyFlix.AppServer.Interfaces;
using MyFlix.AppServer.ViewModel;
using MyFlix.Infra.Notificacoes;

namespace MyFlix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        private readonly IFilmesAppServe _filmesAppServe;

        public FilmesController(IFilmesAppServe filmesAppServe)
        {
            _filmesAppServe = filmesAppServe;
        }

        /// <summary>
        /// Busca todos filmes.
        /// </summary>
        /// <remarks> Busca todos filmes de modo assíncrono.</remarks>
        /// <returns>An <see cref="ActionResult"/> Retorna todos filmes cadastrado,  ou um objeto vazio  se o filme não existir </returns>
        [HttpGet("getFilmes")]
        public async Task<ActionResult> Get()
        {
            var listaFilmes = await _filmesAppServe.ObterTodosAsync();
            if(!Notificacao.IsValid())
                return BadRequest(Notificacao.GetErrors());

            return Ok(listaFilmes);
        }

        [HttpGet("validar-filmes")]
        public async Task<ActionResult> ValidarFilmes()
        {
            var existeFilmes = await _filmesAppServe.ValidarSeExisteFilmesAsync();
            return Ok(existeFilmes);
        }

        /// <summary>
        /// Busca um filme por id.
        /// </summary>
        /// <param name="id">Indentifica o filme por id </param>
        /// <returns>An <see cref="ActionResult"/> retorna um filme se encontrado, ou um objeto vazio  se o filme não existir.</returns>
        [HttpGet("getFilmes-por-id/{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var filme = await _filmesAppServe.ObterPorIdAsync(id);
            if (!Notificacao.IsValid())
                return BadRequest(Notificacao.GetErrors());

            return Ok(filme);
        }

        /// <summary>
        /// Cadastra um filme.
        /// </summary>
        /// <param name="filmesView"></param>
        /// <returns>Filme cadastrado com sucesso, ou mensagem de erro tradada </returns>
        /// <response code="400">Retorna erros de validação</response>
        /// <response code="200">Filme cadastrado com sucesso!</response>
        /// <param name="filmesView">Dados do filme a ser cadastro</param>
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
       // [Route("cadastrarFilme")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] FilmesViewModel filmesView)
        {
            await _filmesAppServe.CadastrarAsync(filmesView);
            if (!Notificacao.IsValid())
                return BadRequest(Notificacao.GetErrors());

            return Ok(new {mensagem = "Filme cadastrado com sucesso!" });
        }

        /// <summary>
        /// Atualiza um filme.
        /// </summary>
        /// <param name="filmesView"></param>
        /// <returns>Filme atualizado com sucesso, ou mensagem de erro tradada </returns>
        /// <response code="400">Retorna erros de validação</response>
        /// <response code="200">Filme atualizado com sucesso!</response>
        /// <param name="filmesView">Dados do filme a ser filme</param>
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[Route("atualizarFilme")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] FilmesViewModel filmesView)
        {
            var filmeAtualizado = await _filmesAppServe.AtualizarAsync(filmesView);
            if (!Notificacao.IsValid())
                return BadRequest(Notificacao.GetErrors());

            return Ok(new { filmeAtualizado, mensagem = "Filme atualizado com sucesso!" });
        }

        /// <summary>
        /// Deleta um filme.
        /// </summary>
        /// <param name="filmesView"></param>
        /// <returns>Filme Deletado com sucesso!</returns>
        /// <response code="400">Retorna erros de validação</response>
        /// <response code="200">Filme deletado com sucesso!</response>
        /// <param name="filmesView">Dados do filme a ser deletado</param>
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _filmesAppServe.DeletarAsync(id);
            if (!Notificacao.IsValid())
                return BadRequest(Notificacao.GetErrors());

            return Ok(new { mensagem = "Filme deletado com sucesso!" });
        }
    }
}
