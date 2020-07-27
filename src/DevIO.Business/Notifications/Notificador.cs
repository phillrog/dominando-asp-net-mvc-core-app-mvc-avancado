using System.Collections.Generic;
using System.Linq;
using DevIO.Business.Interfaces;

namespace DevIO.Business.Notifications
{
	public class Notificador : INotificador
	{
		private List<Notificacao> _notificacaoes;

		public Notificador()
		{
			_notificacaoes = new List<Notificacao>();
		}
		public void Handle(Notificacao notificacao)
		{
			_notificacaoes.Add(notificacao);
		}

		public List<Notificacao> ObterNotificacoes()
		{
			return _notificacaoes;
		}

		public bool TemNotifcacao()
		{
			return _notificacaoes.Any();
		}
	}
}
