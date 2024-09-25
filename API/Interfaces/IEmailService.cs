using System;

namespace API.Interfaces;

public interface IEmailService
{
	string ResetarSenhaEmail(string emailDestino, string novaSenha, string caminhoHtml);

}
