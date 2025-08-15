
using MyFlix.Infra.Helpers;
using MyFlix.Infra.Notificacoes;

namespace MyFlix.Dados.Entidades
{
    public class Filmes
    {
        public int Id { get; set; }
        public int AvaliacaoId { get; set; }
        public string Nome { get; set; }
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public DateTime DataCadastro {
            get {  return DateTime.Now;}
            private set; 
        }
        public DateTime AnoLancamento { get; set; }
        public Avaliacao Avaliacao { get; set; }

        public bool Validate()
        {
            Extensioes.ValidateString(this.Nome, "Nome é obrigatório.");
            Extensioes.ValidateString(this.Titulo, "Titulo é obrigatório.");
            Extensioes.ValidateString(this.Genero, "Genero é obrigatório.");
            Extensioes.ValidateDate(this.AnoLancamento, "Ano de Lançamento é obrigatório.");
            return Notificacao.IsValid();
        }

    }


}
