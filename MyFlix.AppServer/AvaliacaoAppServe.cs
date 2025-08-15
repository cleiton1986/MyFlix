using AutoMapper;
using MyFlix.AppServer.Interfaces;
using MyFlix.AppServer.ViewModel;
using MyFlix.Serve.Interfaces;

namespace MyFlix.AppServer
{
    public class AvaliacaoAppServe: IAvaliacaoAppServe
    {
        private readonly IAvaliacaoServe _avaliacaoServe;
        private readonly IMapper _mapper;
        public AvaliacaoAppServe(IAvaliacaoServe avaliacaoServe, IMapper mapper)
        {
            _avaliacaoServe = avaliacaoServe;
            _mapper = mapper;
        }

        public async Task<AvaliacaoViewModel> ObterPorIdAsync(int id)
        {
            var avaliacao = await _avaliacaoServe.ObterPorIdAsync(id);
            var avaliacaoView = _mapper.Map<AvaliacaoViewModel>(avaliacao);
            return avaliacaoView;
        }

        public async Task<IList<AvaliacaoViewModel>> ObterTodosAsync()
        {
            var avaliacao = await _avaliacaoServe.ObterTodosAsync();
            var avaliacaoView = _mapper.Map<List<AvaliacaoViewModel>>(avaliacao);
            return avaliacaoView;
        }
    }
}
