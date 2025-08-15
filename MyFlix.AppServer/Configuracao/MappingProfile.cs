using AutoMapper;
using MyFlix.AppServer.ViewModel;
using MyFlix.Dados.Entidades;
using MyFlix.Infra.Helpers;

namespace MyFlix.AppServer.Configuracao
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Filmes, FilmesViewModel>()
              .ForMember(dest => dest.AnoLancamento, opt => opt.MapFrom(src => src.AnoLancamento.ConvertDateToString()))
              .AfterMap((opt, dest) =>
              {
                  dest.Assistido = opt.Avaliacao != null && opt.Avaliacao.Assistido ? true : false ;
              })
              .AfterMap((opt, dest) =>
              {
                  dest.Nota = opt.Avaliacao != null ? opt.Avaliacao.Nota : 0;
              });

            CreateMap<FilmesViewModel, Filmes>()
               .ForMember(dest => dest.AnoLancamento, opt => opt.MapFrom(src => src.AnoLancamento.ConvertStringToDate()))
               .ForMember(dest => dest.DataCadastro, opt => opt.Ignore())
               .ForMember(dest => dest.AvaliacaoId, opt => opt.Ignore())
                .AfterMap((opt, dest) =>
                {
                    dest.Avaliacao = new Avaliacao { Nota = opt.Nota.HasValue ? opt.Nota.Value : 0, Assistido = opt.Assistido};
                });
              
            CreateMap<Avaliacao, AvaliacaoViewModel>();

            CreateMap<AvaliacaoViewModel, Avaliacao>();

        }
    }
}
