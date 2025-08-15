using System.ComponentModel.DataAnnotations.Schema;

namespace MyFlix.Infra.Notificacoes
{
    public class Notificacao
    {
        public Notificacao()
        {
            
        }

        [NotMapped]
        public string NomePropriedade { get; set; }

        [NotMapped]
        public string Mensagem { get; set; }

        [NotMapped]
        public bool Sucesso { get; set; } = true;

        [NotMapped]
        private static List<Notificacao> ListaNotification { get; set; } = new List<Notificacao>();

        public static List<Notificacao> Notify(string mensagem, string nomePropriedade = null)
        {

            if (!string.IsNullOrWhiteSpace(mensagem) && IsValid())
            {
                ClearNotifications();

                ListaNotification.Add(new Notificacao
                {
                    Mensagem = mensagem,
                    NomePropriedade = nomePropriedade,
                    Sucesso = false
                });

            }
            return ListaNotification;
        }

        public static List<Notificacao> NotifyList(string mensagem, string nomePropriedade = null)
        {

            if (!string.IsNullOrWhiteSpace(mensagem))
            {
                ListaNotification.Add(new Notificacao
                {
                    Mensagem = mensagem,
                    NomePropriedade = nomePropriedade,
                    Sucesso = false
                });

            }
            return ListaNotification;
        }
        public static bool IsValid()
        {
            return !ListaNotification.Any();
        }
        public static void ClearNotifications()
        {
            ListaNotification.Clear();
        }
        public static List<string> GetErrors()
        {
            List<string> notifications = ListaNotification.Where(x => !x.Sucesso).Select(x => x.Mensagem).ToList();
            ClearNotifications();
            return notifications;
        }
    }
}

