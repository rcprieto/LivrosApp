using System;
using System.Text;
using API.Helpers;
using API.Interfaces;

namespace API.Services;

public class EmailService : IEmailService
{
    private readonly string _pass;
    private readonly string _remetente;
    public EmailService(IConfiguration configuration)
    {
        CriptografiaQueryString cripto = new CriptografiaQueryString();

        _remetente = cripto.Descriptografar(configuration["Remetente"]);
        _pass = cripto.Descriptografar(configuration["RemetenteSenha"]);
    }
    public string ResetarSenhaEmail(string emailDestino, string novaSenha, string caminhoHtml)
    {
        StringBuilder corpoPt = new StringBuilder($@" Uma nova senha foi criada para seu usuário: {emailDestino}
                                        <br/>Nova Senha: <b>{novaSenha}</b>
                                        <br />Acesse o sistema e altere sua senha.");

        string titulo = "Livros Senha Redefinida";
        string corpo = ConstroiEmail(titulo, corpoPt.ToString(), caminhoHtml);
        return Geral.EnviarEmail(emailDestino, _remetente, titulo, corpo, "", _pass, _remetente);

    }

    private static string ConstroiEmail(string titulo, string corpoPt, string caminho)
    {

        //string msg = Geral.LerHtml(caminho);
        StringBuilder msg = new StringBuilder();
        msg.Append($@"<html>
<head>
    <title>Livros</title>
    <style>
        .titulo {{
            font-family: 'Times New Roman', Times, serif;
        }}

        .divTopo {{
            background-color: #14143a;
            height: 201px;
            text-align: center;
        }}

        .divRodape {{
            background-color: #050033;
            height: 340px;
            text-align: center;
        }}

        .imgTopo {{
            margin-top: 50px;
        }}

        .imgRodape {{
            margin-top: 30px;
        }}

        .corpo {{
            text-align: center;
        }}

        .imgMqm {{
            margin-top: 30px;
        }}

        h1 {{
            font-size: 10pt;
            font-family: Verdana, sans-serif;
            color: rgb(167, 167, 167);
        }}

        h2 {{
            font-size: 28pt;
            font-family: 'Times New Roman', serif;
            color: #050033;
            font-style: italic;
            margin-top: 30px;
        }}

        .corpoTexto {{
            font-family: Verdana;
            text-align: left;
            padding: 40px;
            font-size: 10pt;
            color: #050033;
        }}

        h5 {{
            font-style: italic;
            font-weight: normal;
            font-size: 10px;
            text-align: center;
        }}

        .botao {{
            background-color: #050033;
            color: white;
            text-decoration: none;
            border-radius: 7px;
            padding: 13px 20px 13px 20px;
        }}

            .botao:hover {{
                background-color: #6d6d6d;
            }}

        .itensOffice {{
            margin-top: 25px;
            padding: 10px;
            border: 1px solid #9e9e9e;
            border-radius: 5px;
        }}

        hr {{
            border-top: 1px solid #050033;
            color: #050033;
            width: 50px;
        }}
    </style>
</head>
<body bgcolor='#f7f7f7;' height='1800px' style='background-color: #f7f7f7'>
    <label style='display:none; color:white'>Livros</label>
    <table width='680px'
           style='width: 680px; background-color: #ffffff; margin-left: 50px'>
        <tr height='200px' style='height: 200px'>
            <td align='center' bgcolor='#050033' style='background-color: #14143a'>
                <img src='data:image/jpg;base64,' />
            </td>
        </tr>
        <tr>
            <td style='text-align: center; padding-top: 20px' align='center'>
                <img src='data:image/jpg;base64,' />
            </td>
        </tr>
        <tr>
            <td style='
            text-align: center;
            padding-left: 50px;
            padding-right: 50px;
            padding-top: 10px;
            background-color: #ffffff;
          '
                align='center'
                bgcolor='#ffffff'>
                <div class='corpo' align='center'>

                    <h2>::titulo</h2>
                    <div class='corpoTexto'>
                        <hr />
                        <b>Prezado(a),</b><br /><br />

                        <br />
                        <br />

                        ::corpoPt

                        <br /><br />
                        <br /><br />
                        <div style='width: 100%; text-align: center'>

                            <a href='https://livro.citapps.com'>
                                <img style='width:150px; '  src='data:image/png;base64,' />
                            </a>
                        </div>

                        <br /><br />
                        <br />
                        <br />

                        Atenciosamente,
                        <br /><br />
                              <b> Livros </b>
                        <br /><br />
                        <hr />
                        <br />

                        
                        <div style='width: 100%; text-align: center'>

                            <a href='https://livros.citapps.com.br'>
                                <img style='width:150px; '  src='data:image/png;base64,' />
                            </a>
                        </div>

                        <br /><br />
                        <br />

                    </div>
                </div>
            </td>
        </tr>
        <tr style='height: 300px'>
            <td align='center' bgcolor='#050033' style='background-color: #050033'>
                <img src='data:image/jpg;base64,'
                     class='imgRodape' />

                <br />
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
    </table>
</body>
</html>
");
        msg = msg.Replace("::titulo", titulo);
        msg = msg.Replace("::corpoPt", corpoPt);

        return msg.ToString();

    }
}
