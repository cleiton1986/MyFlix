using MyFlix.Dados.Entidades;
using MyFlix.Infra.Helpers;
using MyFlix.Repository.Interfaces;
using MyFlix.Serve.Interfaces;

namespace MyFlix.Serve
{
    public class AvaliacaoServe : IAvaliacaoServe
    {
        private readonly IAvaliacaoRepositorio _avaliacaoRepositorio;

        public AvaliacaoServe(IAvaliacaoRepositorio avaliacaoRepositorio)
        {
            _avaliacaoRepositorio = avaliacaoRepositorio;
        }
        public async Task<Avaliacao> ObterPorIdAsync(int id)
        {
            return await _avaliacaoRepositorio.ObterPorIdAsync(id);
        }

        public async Task<IList<Avaliacao>> ObterTodosAsync()
        {
            var listaAvaliacao = await _avaliacaoRepositorio.ObterTodosAsync();
            return  listaAvaliacao.ToList();
        }
    }
}
