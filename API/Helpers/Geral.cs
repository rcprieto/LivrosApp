using System.Globalization;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace API.Helpers
{
    #region GERAL

    public enum DateInterval
    {
        Day,
        DayOfYear,
        Hour,
        Minute,
        Month,
        Quarter,
        Second,
        Weekday,
        WeekOfYear,
        Year
    }

    public static class Geral
    {

        #region Criptografar
        /// <summary>
        /// Criptografa um texto utilizando o algor�timo SHA256 (Irrevers�vel pois � HASH).
        /// </summary>
        /// <param name="texto">O texto � ser criptografado.</param>
        /// <returns>O texto criptografado (Sempre com 44 caracteres).</returns>
        public static string Criptografar(string texto)
        {
            byte[] data = Encoding.UTF8.GetBytes(texto);
            SHA256 shaM = SHA256.Create();
            return Convert.ToBase64String(shaM.ComputeHash(data));
        }
        /// <summary>
        /// Criptografa um texto utilizando o algor�timo SHA256 (Irrevers�vel pois � HASH).
        /// </summary>
        /// <param name="conteudo_binario">O conteudo bin�rio � ser criptografado.</param>
        /// <returns>O texto criptografado (Sempre com 44 caracteres).</returns>
        public static string Criptografar(byte[] conteudo_binario)
        {
            SHA256 shaM = SHA256.Create();
            return Convert.ToBase64String(shaM.ComputeHash(conteudo_binario));
        }
        #endregion

        #region GerarSenhaRandomica
        /// <summary>
        /// Gera uma senha alfanum�rica rand�mica de 6 caracteres.
        /// </summary>
        /// <returns>Retorna uma string contendo a senha.</returns>
        public static string GerarSenhaRandomica()
        {
            string _caracteres_permitidos = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            Random gerar_num_aleatorio = new Random();
            char[] chars = new char[6];
            int contador = _caracteres_permitidos.Length;

            for (int i = 0; i < 6; i++)
                chars[i] = _caracteres_permitidos[(int)(_caracteres_permitidos.Length * gerar_num_aleatorio.NextDouble())];

            string retorno = new string(chars);

            return retorno;
        }

        public static string GerarSenhaRandomicaLonga()
        {
            string _caracteres_p1 = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
            string _caracteres_p2 = "abcdefghijkmnopqrstuvwxyz";
            string _caracteres_p3 = "0123456789";
            string _caracteres_p4 = "@#";
            Random gerar_num_aleatorio = new Random();
            char[] chars = new char[9];


            for (int i = 0; i < 9; i++)
            {
                if (i == 0 || i == 3 || i == 7)
                {
                    int contador = _caracteres_p1.Length;
                    chars[i] = _caracteres_p1[(int)(contador * gerar_num_aleatorio.NextDouble())];
                }
                else if (i == 2)
                {
                    int contador = _caracteres_p4.Length;
                    chars[i] = _caracteres_p4[(int)(contador * gerar_num_aleatorio.NextDouble())];
                }
                else if (i == 1 || i == 4)
                {
                    int contador = _caracteres_p3.Length;
                    chars[i] = _caracteres_p3[(int)(contador * gerar_num_aleatorio.NextDouble())];
                }
                else if (i == 6 || i == 8 || i == 5)
                {
                    int contador = _caracteres_p2.Length;
                    chars[i] = _caracteres_p2[(int)(contador * gerar_num_aleatorio.NextDouble())];
                }



            }

            string retorno = new string(chars);

            return retorno;
        }
        #endregion

        #region TraduzirMes
        /// <summary>
        /// Retorna o nome do m�s � partir de um numeral de 1 a 12.
        /// </summary>
        /// <param name="numeral">O n�mero correspondente ao m�s.</param>
        /// <param name="abreviado">Indica se deve ou n�o abreviar a resposta. Ex.: Jan ou Janeiro.</param>
        public static string TraduzirMes(int numeral, bool abreviado)
        {
            string retorno = "";
            switch (numeral)
            {
                case 1:
                    retorno = abreviado ? "Jan" : "Janeiro";
                    break;
                case 2:
                    retorno = abreviado ? "Fev" : "Fevereiro";
                    break;
                case 3:
                    retorno = abreviado ? "Mar" : "Mar�o";
                    break;
                case 4:
                    retorno = abreviado ? "Abr" : "Abril";
                    break;
                case 5:
                    retorno = abreviado ? "Mai" : "Maio";
                    break;
                case 6:
                    retorno = abreviado ? "Jun" : "Junho";
                    break;
                case 7:
                    retorno = abreviado ? "Jul" : "Julho";
                    break;
                case 8:
                    retorno = abreviado ? "Ago" : "Agosto";
                    break;
                case 9:
                    retorno = abreviado ? "Set" : "Setembro";
                    break;
                case 10:
                    retorno = abreviado ? "Out" : "Outubro";
                    break;
                case 11:
                    retorno = abreviado ? "Nov" : "Novembro";
                    break;
                case 12:
                    retorno = abreviado ? "Dez" : "Dezembro";
                    break;
            }
            return retorno;
        }

        public static string RetornaNomeMes(int mes, string lingua, bool abreviado = true)
        {
            switch (mes)
            {
                case 1:
                    if (lingua == "1")
                        return abreviado ? "Jan" : "Janeiro";
                    else if (lingua == "2")
                        return abreviado ? "Ene" : "Enero";
                    else
                        return abreviado ? "Jan" : "January";
                    break;

                case 2:
                    if (lingua == "1")
                        return abreviado ? "Fev" : "Fevereiro";
                    else if (lingua == "2")
                        return abreviado ? "Feb" : "Febrero";
                    else
                        return abreviado ? "Feb" : "";
                    break;

                case 3:
                    if (lingua == "1")
                        return abreviado ? "Mar" : "Mar�o";
                    else if (lingua == "2")
                        return abreviado ? "Mar" : "Marzo";
                    else
                        return abreviado ? "Mar" : "March";
                    break;

                case 4:
                    if (lingua == "1")
                        return abreviado ? "Abr" : "Abril";
                    else if (lingua == "2")
                        return abreviado ? "Abr" : "Abril";
                    else
                        return abreviado ? "Apr" : "April";
                    break;

                case 5:
                    if (lingua == "1")
                        return abreviado ? "Mai" : "Maio";
                    else if (lingua == "2")
                        return abreviado ? "May" : "Mayo";
                    else
                        return abreviado ? "May" : "May";
                    break;

                case 6:
                    if (lingua == "1")
                        return abreviado ? "Jun" : "Junho";
                    else if (lingua == "2")
                        return abreviado ? "Jun" : "Junio";
                    else
                        return abreviado ? "Jun" : "June";
                    break;

                case 7:
                    if (lingua == "1")
                        return abreviado ? "Jul" : "Julho";
                    else if (lingua == "2")
                        return abreviado ? "Jul" : "Julio";
                    else
                        return abreviado ? "Jul" : "July";
                    break;

                case 8:
                    if (lingua == "1")
                        return abreviado ? "Ago" : "Agosto";
                    else if (lingua == "2")
                        return abreviado ? "Ago" : "Agosto";
                    else
                        return abreviado ? "Aug" : "August";
                    break;

                case 9:
                    if (lingua == "1")
                        return abreviado ? "Set" : "Setembro";
                    else if (lingua == "2")
                        return abreviado ? "Sep" : "Septiembre";
                    else
                        return abreviado ? "Sep" : "September";
                    break;

                case 10:
                    if (lingua == "1")
                        return abreviado ? "Out" : "Outubro";
                    else if (lingua == "2")
                        return abreviado ? "Oct" : "Octubre";
                    else
                        return abreviado ? "Oct" : "October";
                    break;

                case 11:
                    if (lingua == "1")
                        return abreviado ? "Nov" : "Novembro";
                    else if (lingua == "2")
                        return abreviado ? "Nov" : "Noviembre";
                    else
                        return abreviado ? "Nov" : "November";
                    break;

                case 12:
                    if (lingua == "1")
                        return abreviado ? "Dez" : "Dezembro";
                    else if (lingua == "2")
                        return abreviado ? "Dic" : "Diciembre";
                    else
                        return abreviado ? "Dec" : "December";
                    break;

                default:
                    return "";
                    break;


            }
        }


        public static Dictionary<int, string> RetornaMeses(string lingua)
        {
            Dictionary<int, string> meses = new Dictionary<int, string>();
            if (lingua == "1")
            {
                meses.Add(1, "Jan");
                meses.Add(2, "Fev");
                meses.Add(3, "Mar");
                meses.Add(4, "Abr");
                meses.Add(5, "Mai");
                meses.Add(6, "Jun");
                meses.Add(7, "Jul");
                meses.Add(8, "Ago");
                meses.Add(9, "Set");
                meses.Add(10, "Out");
                meses.Add(11, "Nov");
                meses.Add(12, "Dez");
            }
            else if (lingua == "2")
            {
                meses.Add(1, "Ene");
                meses.Add(2, "Feb");
                meses.Add(3, "Mar");
                meses.Add(4, "Abr");
                meses.Add(5, "May");
                meses.Add(6, "Jun");
                meses.Add(7, "Jul");
                meses.Add(8, "Ago");
                meses.Add(9, "Sep");
                meses.Add(10, "Oct");
                meses.Add(11, "Nov");
                meses.Add(12, "Dic");
            }
            else if (lingua == "3")
            {
                meses.Add(1, "Jan");
                meses.Add(2, "Feb");
                meses.Add(3, "Mar");
                meses.Add(4, "Apr");
                meses.Add(5, "May");
                meses.Add(6, "Jun");
                meses.Add(7, "Jul");
                meses.Add(8, "Aug");
                meses.Add(9, "Sep");
                meses.Add(10, "Oct");
                meses.Add(11, "Nov");
                meses.Add(12, "Dec");
            }


            return meses;
        }

        public static Dictionary<int, string> RetornaDiaSemana()
        {
            Dictionary<int, string> diasemana = new Dictionary<int, string>();

            diasemana.Add(1, "Domingo");
            diasemana.Add(2, "Segunda");
            diasemana.Add(3, "Ter�a");
            diasemana.Add(4, "Quarta");
            diasemana.Add(5, "Quinta");
            diasemana.Add(6, "Sexta");
            diasemana.Add(7, "S�bado");

            return diasemana;
        }

        public static Dictionary<int, string> RetornaAnos(int qtd = 4)
        {
            Dictionary<int, string> anos = new Dictionary<int, string>();

            int anoAtual = DateTime.Now.Year + 1;
            for (int i = anoAtual - qtd; i <= anoAtual; i++)
            {
                anos.Add(i, i.ToString());
            }

            return anos;
        }

        #endregion

        public static string RemoveCaracteresEspeciais(string texto)
        {
            string retorno = texto;
            //Aqui voc� pode incluir os caracteres qeu deseja que sejam retirados	
            char[] trim = { '=', '\\', ';', '.', ':', ',', '+', '*', '-', '@', '/' };
            int pos;
            while ((pos = retorno.IndexOfAny(trim)) >= 0)
            {
                retorno = retorno.Remove(pos, 1);
            }
            return retorno;
        }

        public static string DiaDaSemana(DayOfWeek diaSemana, bool abreviado)
        {
            string dia = "";
            switch (diaSemana)
            {
                case DayOfWeek.Friday:
                    if (abreviado)
                        dia = "Sex";
                    else
                        dia = "Sexta";
                    break;

                case DayOfWeek.Monday:
                    if (abreviado)
                        dia = "Seg";
                    else
                        dia = "Segunda";
                    break;

                case DayOfWeek.Saturday:
                    if (abreviado)
                        dia = "S�b";
                    else
                        dia = "S�bado";
                    break;

                case DayOfWeek.Sunday:
                    if (abreviado)
                        dia = "Dom";
                    else
                        dia = "Domingo";
                    break;

                case DayOfWeek.Thursday:
                    if (abreviado)
                        dia = "Qui";
                    else
                        dia = "Quinta";
                    break;

                case DayOfWeek.Tuesday:
                    if (abreviado)
                        dia = "Ter";
                    else
                        dia = "Ter�a";
                    break;

                case DayOfWeek.Wednesday:
                    if (abreviado)
                        dia = "Qua";
                    else
                        dia = "Quarta";
                    break;

            }


            return dia;
        }

        public static string EscreverValorExtenso(double valor)
        {
            if (valor <= 0)
                return string.Empty;
            else
            {
                string montagem = string.Empty;
                if (valor > 0 & valor < 1)
                {
                    valor *= 100;
                }

                string strValor = Math.Truncate(valor).ToString("000");
                int a = Convert.ToInt32(strValor.Substring(0, 1));
                int b = Convert.ToInt32(strValor.Substring(1, 1));
                int c = Convert.ToInt32(strValor.Substring(2, 1));
                if (a == 1) montagem += b + c == 0 ? "CEM" : "CENTO";
                else if (a == 2) montagem += "DUZENTOS";
                else if (a == 3) montagem += "TREZENTOS";
                else if (a == 4) montagem += "QUATROCENTOS";
                else if (a == 5) montagem += "QUINHENTOS";
                else if (a == 6) montagem += "SEISCENTOS";
                else if (a == 7) montagem += "SETECENTOS";
                else if (a == 8) montagem += "OITOCENTOS";
                else if (a == 9) montagem += "NOVECENTOS";
                if (b == 1)
                {
                    if (c == 0) montagem += (a > 0 ? " E " : string.Empty) + "DEZ";
                    else if (c == 1) montagem += (a > 0 ? " E " : string.Empty) + "ONZE";
                    else if (c == 2) montagem += (a > 0 ? " E " : string.Empty) + "DOZE";
                    else if (c == 3) montagem += (a > 0 ? " E " : string.Empty) + "TREZE";
                    else if (c == 4) montagem += (a > 0 ? " E " : string.Empty) + "QUATORZE";
                    else if (c == 5) montagem += (a > 0 ? " E " : string.Empty) + "QUINZE";
                    else if (c == 6) montagem += (a > 0 ? " E " : string.Empty) + "DEZESSEIS";
                    else if (c == 7) montagem += (a > 0 ? " E " : string.Empty) + "DEZESSETE";
                    else if (c == 8) montagem += (a > 0 ? " E " : string.Empty) + "DEZOITO";
                    else if (c == 9) montagem += (a > 0 ? " E " : string.Empty) + "DEZENOVE";
                }
                else if (b == 2) montagem += (a > 0 ? " E " : string.Empty) + "VINTE";
                else if (b == 3) montagem += (a > 0 ? " E " : string.Empty) + "TRINTA";
                else if (b == 4) montagem += (a > 0 ? " E " : string.Empty) + "QUARENTA";
                else if (b == 5) montagem += (a > 0 ? " E " : string.Empty) + "CINQUENTA";
                else if (b == 6) montagem += (a > 0 ? " E " : string.Empty) + "SESSENTA";
                else if (b == 7) montagem += (a > 0 ? " E " : string.Empty) + "SETENTA";
                else if (b == 8) montagem += (a > 0 ? " E " : string.Empty) + "OITENTA";
                else if (b == 9) montagem += (a > 0 ? " E " : string.Empty) + "NOVENTA";

                if (strValor.Substring(1, 1) != "1" & c != 0 & montagem != string.Empty) montagem += " E ";
                if (strValor.Substring(1, 1) != "1")
                    if (c == 1) montagem += "UM";
                    else if (c == 2) montagem += "DOIS";
                    else if (c == 3) montagem += "TR�S";
                    else if (c == 4) montagem += "QUATRO";
                    else if (c == 5) montagem += "CINCO";
                    else if (c == 6) montagem += "SEIS";
                    else if (c == 7) montagem += "SETE";
                    else if (c == 8) montagem += "OITO";
                    else if (c == 9) montagem += "NOVE";



                string strValorCentavos = valor.ToString("000.0");
                int centavos = Convert.ToInt32(strValorCentavos.Substring(4, 1));

                if (centavos == 0)
                    return montagem.ToLower();
                else
                {

                    if (centavos == 1) montagem += " V�RGULA UM";
                    else if (centavos == 2) montagem += " V�RGULA DOIS";
                    else if (centavos == 3) montagem += " V�RGULA TR�S";
                    else if (centavos == 4) montagem += " V�RGULA QUATRO";
                    else if (centavos == 5) montagem += " V�RGULA CINTO";
                    else if (centavos == 6) montagem += " V�RGULA SEIS";
                    else if (centavos == 7) montagem += " V�RGULA SETE";
                    else if (centavos == 8) montagem += " V�RGULA OITO";
                    else if (centavos == 9) montagem += " V�RGULA NOVE";

                    return montagem.ToLower();
                }
            }
        }


        public static string RemoveAccents(string text)
        {
            string ext = Path.GetExtension(text);
            text = text.Replace(ext, "");
            text = RemoverAcentos(text);
            text = text + ext;

            text = text.Replace(",", "").Replace("-", "_").Replace("#", "").Replace("$", "").Replace("&", "").Replace("%", "").Replace("/", "").Replace(@"\", "").Replace(" ", "_");
            StringBuilder sbReturn = new StringBuilder();


            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();

            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.MathSymbol && CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.OtherSymbol)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString().Replace(":", "");
        }


        public static string RemoverAcentos(string texto)
        {
            string comAcentos = "����������������������������������������������";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (int i = 0; i < comAcentos.Length; i++)
            {
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }

            string retorno = new string(texto
                .Normalize(NormalizationForm.FormD)
                .Where(ch => char.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                .ToArray());

            return retorno;


        }



        public static string EnviarEmail(string Email, string EmailOrigem, string assunto, string mensagem, string ccoEmails = "", string senha = "", string remetente = "suporte@constructoit.com.br")
        {
            //Define os dados do e-mail
            string nomeRemetente = "Livros";

            var msg = new MailMessage();
            //MailAddress origem = new MailAddress();
            //msg.From = origem;
            msg.From = new MailAddress(nomeRemetente + "<" + remetente + ">");
            //msg.To.Add(Email);

            if (Email.Contains(";"))
            {
                var emails = Email.Split(';');
                foreach (var item in emails)
                {

                    msg.To.Add(item);
                }
            }
            else
                msg.To.Add(Email);

            if (!string.IsNullOrEmpty(ccoEmails))
            {
                var emails = ccoEmails.Replace(" ", "").Trim().Split(';');
                string emailEnvio = "";
                int cont = 0;
                foreach (var item in emails)
                {
                    if (cont == 0)
                        emailEnvio = item.Trim();
                    else if (!string.IsNullOrEmpty(item))
                    {
                        emailEnvio += "," + item.Trim();
                    }

                    cont++;
                }
                msg.Bcc.Add(emailEnvio);
            }
            msg.Priority = MailPriority.Normal;
            msg.IsBodyHtml = true;
            msg.Subject = assunto;
            msg.Body = mensagem;
            msg.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            msg.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            //Cria objeto com os dados do SMTP
            SmtpClient objSmtp = new SmtpClient();
            objSmtp.Host = "smtp.gmail.com";
            objSmtp.EnableSsl = true;
            objSmtp.Port = 587;
            objSmtp.UseDefaultCredentials = false;
            objSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            objSmtp.Credentials = new System.Net.NetworkCredential(remetente, senha);
            objSmtp.Timeout = 20000;
            try
            {
                objSmtp.Send(msg);
                return "ok";
            }
            catch (Exception err)
            {
                string erroMensagem = "Ocorreram problemas no envio do e-mail. Erro = " + err.Message;
                return erroMensagem;
            }


        }

        public static string LerHtml(string arquivo)
        {
            StringBuilder msg = new StringBuilder();
            foreach (var line in File.ReadLines(arquivo))
            {
                msg.AppendLine(line);
            }
            return msg.ToString();
        }
    }

    public class CriptografiaQueryString
    {
        private byte[] chave = { };
        private byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };
        private string chaveCriptografia = "0ry0nr1b";

        public CriptografiaQueryString()
        {

        }

        public CriptografiaQueryString(string chave)
        {
            chaveCriptografia = chave;
        }

        public string Criptografar(string valor)
        {
            try
            {
                chave = Encoding.UTF8.GetBytes(chaveCriptografia);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(valor);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms,
                  des.CreateEncryptor(chave, iv), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                var retorno = HttpUtility.UrlEncode(Convert.ToBase64String(ms.ToArray()));
                retorno = retorno.Replace("%", "__");
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Descriptografar(string valor)
        {
            byte[] inputByteArray = new byte[valor.Length + 1];

            try
            {
                if (valor.Contains("__"))
                    valor = HttpUtility.UrlDecode(valor.Replace("__", "%"));
                chave = Encoding.UTF8.GetBytes(chaveCriptografia);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(valor);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms,
                  des.CreateDecryptor(chave, iv), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                Encoding encoding = Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }



    #endregion

}