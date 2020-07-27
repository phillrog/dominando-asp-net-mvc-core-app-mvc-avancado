using DevIO.Business.Notifications;
using System.Collections.Generic;

namespace DevIO.Business.Interfaces
{
	public interface INotificador
	{
		bool TemNotifcacao();
		List<Notificacao> ObterNotificacoes();
		void Handle(Notificacao notificacao);
	}
}
