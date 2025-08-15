using Microsoft.AspNetCore.Mvc;
using MyFlix.AppServer.Interfaces;
using MyFlix.Infra.Notificacoes;

namespace MyFlix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvaliacaoController : ControllerBase
    {
        private readonly IAvaliacaoAppServe _avaliacaoAppServe;

        public AvaliacaoController(IAvaliacaoAppServe avaliacaoAppServe)
        {
            _avaliacaoAppServe = avaliacaoAppServe;
        }

        /// <summary>
        /// Busca todas avaliacoes.
        /// </summary>
        /// <remarks> Busca todas avaliações de modo assíncrono.</remarks>
        /// <returns>An <see cref="ActionResult"/> Retorna todas avaliações cadastradas,  ou um objeto vazio  se o filme não existir </returns>
        [HttpGet("getAvaliacao")]
        public async Task<ActionResult> Get()
        {
            var listaFilmes = await _avaliacaoAppServe.ObterTodosAsync();
            if (!Notificacao.IsValid())
                return BadRequest(Notificacao.GetErrors());

            return Ok(listaFilmes);
        }
        /// <summary>
        /// Busca uma avaliacao por id.
        /// </summary>
        /// <param name="id">Indentifica a avaliacao por id </param>
        /// <returns>An <see cref="ActionResult"/> retorna uma avaliação se encontrada, ou um objeto vazio  se o filme não existir.</returns>
        [HttpGet("getAvaliacao-por-id/{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var filme = await _avaliacaoAppServe.ObterPorIdAsync(id);
            if (!Notificacao.IsValid())
                return BadRequest(Notificacao.GetErrors());

            return Ok(filme);
        }

 

    }
}
