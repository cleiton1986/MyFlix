using MyFlix.Infra.Helpers;
using MyFlix.Infra.Notificacoes;

namespace MyFlix.Dados.Entidades
{
    public class Avaliacao
    {
        public int Id { get; set; }
        public int Nota { get; set; }
        public bool Assistido { get; set; }
        public bool Validate()
        {
            var validacao = this.Nota <= 0 || this.Nota > 5;
            Extensioes.ValidateBooleam(!validacao, "Nota não deve ser maior que 5 e menor que 1");
            return Notificacao.IsValid();
        }
    }
}
