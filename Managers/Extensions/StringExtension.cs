using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SirenixPowered.ExtensionMethods
{
    public static class StringExtension
    {
        #region RemoveString
        public static string RemoveString(this string s, params string [] words)
        {
            string newValue = s;
            foreach (var item in words)
            {
                newValue = newValue.Replace(item, "");
            }

            return newValue;
        }
        #endregion

        #region RemoveChar
        /// <summary>
        /// Remove o caractere fornecido da string fornecida e retorna a nova string.
        /// </summary>
        /// <param name = "s"> A string fornecida. </param>
        /// <param name = "c"> O caractere a ser removido. </param>
        /// <returns> A nova string. </returns>
        public static string RemoveChar(this string s, params char[] c)
        {
            string newString = s;
            foreach (var item in c)
            {
                s = s.Replace(item.ToString(), string.Empty);
            }

            return s;
        }
        #endregion

        #region Take
        /// <summary>
        /// Como LINQ take - pega os primeiros x caracteres
        /// </summary>
        /// <param name = "value"> a string </param>
        /// <param name = "count"> número de caracteres a serem usados ​​</param>
        /// <param name = "ellipsis"> true para adicionar reticências (...) no final da string </param>
        /// <returns></returns>
        public static string Take(this string value, int count, bool ellipsis = false)
        {
            // obter o número de caracteres que podemos realmente pegar
            int lengthToTake = Math.Min(count, value.Length);

            // Pegue e volte
            return (ellipsis && lengthToTake < value.Length) ?
                string.Format("{0}...", value.Substring(0, lengthToTake)) :
                value.Substring(0, lengthToTake);
        }
        #endregion

        #region Skip
        /// <summary>
        /// como LINQ skip - pula os primeiros x caracteres e retorna a string restante
        /// </summary>
        /// <param name = "value"> a string </param>
        /// <param name = "count"> número de caracteres a pular </param>
        /// <returns></returns>
        public static string Skip(this string value, int count)
        {
            return value.Substring(Math.Min(count, value.Length) - 1);
        }
        #endregion

        #region Reverse
        /// <summary>
        /// Inverte a string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Reverse(this string input)
        {
            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return new String(chars);
        }
        #endregion

        #region IsNullOrEmpty
        /// <summary>
        /// Cheque nulo ou vazio como extensão
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        #endregion

        #region IsNOTNullOrEmpty
        /// <summary>
        /// Retorna verdadeiro se a string não é nula ou vazia
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNOTNullOrEmpty(this string value)
        {
            return (!string.IsNullOrEmpty(value));
        }
        #endregion

        #region Formatted
        /// <summary>
        /// "uma string {0}".Formatted ("blah") vs string.Format ("uma string {0}", "blah")
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Formatted(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
        #endregion

        #region StripHtml
        /// <summary>
        /// dispensa as tags html - observe que não se livra de coisas como nbsp;
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string StripHtml(this string html)
        {
            if (html.IsNullOrEmpty())
                return string.Empty;

            return Regex.Replace(html, @"<[^>]*>", string.Empty);
        }
        #endregion

        #region Match
        /// <summary>
        /// Retorna verdadeiro se o padrão corresponder
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool Match(this string value, string pattern)
        {
            return Regex.IsMatch(value, pattern);
        }
        #endregion

        #region RemoveSpaces
        /// <summary>
        /// Remover espaço em branco, não fim de linha
        /// Útil ao analisar a entrada do usuário como telefone,
        /// preço int.Parse ("1 000 000" .RemoveSpaces (), .....
        /// </summary>
        /// <param name="value"></param>
        public static string RemoveSpaces(this string value)
        {
            return value.Replace(" ", string.Empty);
        }
        #endregion

        #region ReplaceRNWithBr
        /// <summary>
        /// Substitui finais de linha (\ r \ n) por & lt; br / & gt;
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ReplaceRNWithBr(this string value)
        {
            return value.Replace("\r\n", "<br />").Replace("\n", "<br />");
        }
        #endregion

        #region ToEmptyString
        /// <summary>
        /// Converte um nulo ou "" em string.empty. Útil para código XML. Não faz nada se <paramref name = "value" /> já tiver um valor
        /// </summary>
        /// <param name = "value"> string para converter </param>
        public static string ToEmptyString(string value)
        {
            return (string.IsNullOrEmpty(value)) ? string.Empty : value;
        }
        #endregion

        #region ToStringPretty
        /*
        * Converter uma sequência em uma string bem formatada é um pouco chato.
        * O método String.Join definitivamente ajuda, mas infelizmente aceita um
        * array de strings, então ele não compõe com LINQ muito bem.
        *
        * Minha biblioteca inclui várias sobrecargas do operador ToStringPretty que
        * esconde o código desinteressante. Aqui está um exemplo de uso:
        *
        * Console.WriteLine (Enumerable.Range (0, 10) .ToStringPretty ("De 0 a 9: [", ",", "]"));
        *
        * O resultado deste programa é:
        *
        * De 0 a 9: [0,1,2,3,4,5,6,7,8,9]
        */

        /// <summary>
        /// Retorna uma string delimitada por vírgulas
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        public static string ToStringPretty<T>(this IEnumerable<T> source)
        {
            return (source == null) ? string.Empty : ToStringPretty(source, ",");
        }

        /// <summary>
        /// Retorna uma única string, delimitada com <paramref name = "delimiter" /> da fonte
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static string ToStringPretty<T>(this IEnumerable<T> source, string delimiter)
        {
            return (source == null) ? string.Empty : ToStringPretty(source, string.Empty, delimiter, string.Empty);
        }

        /// <summary>
        /// Retorna uma string delimitada, acrescentando <paramref name = "before" /> no início,
        /// e <paramref name = "after" /> no final da string
        /// Ex: Enumerable.Range (0, 10) .ToStringPretty ("De 0 a 9: [", ",", "]")
        /// retorna: De 0 a 9: [0,1,2,3,4,5,6,7,8,9]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="before"></param>
        /// <param name="delimiter"></param>
        /// <param name="after"></param>
        /// <returns></returns>
        public static string ToStringPretty<T>(this IEnumerable<T> source, string before, string delimiter, string after)
        {
            if (source == null)
                return string.Empty;

            StringBuilder result = new StringBuilder();
            result.Append(before);

            bool firstElement = true;
            foreach (T elem in source)
            {
                if (firstElement) firstElement = false;
                else result.Append(delimiter);

                result.Append(elem.ToString());
            }

            result.Append(after);
            return result.ToString();
        }
        #endregion

        #region InvertCase
        /// <summary>
        /// Inverte a caixa de cada caractere na string fornecida e retorna a nova string.
        /// </summary>
        /// <param name = "s"> A string fornecida. </param>
        /// <returns> A string convertida. </returns>
        public static string InvertCase(this string s)
        {
            return new string(
                s.Select(c => char.IsLetter(c) ? (char.IsUpper(c) ?
                      char.ToLower(c) : char.ToUpper(c)) : c).ToArray());
        }
        #endregion

        #region IsNullOrEmptyAfterTrimmed
        /// <summary>
        /// Verifica se a string fornecida é nula, caso contrário, se vazia após o corte.
        /// </summary>
        /// <param name = "s"> A string fornecida. </param>
        /// <returns> Verdadeiro se string for Nulo ou Vazio, caso contrário, falso. </returns>
        public static bool IsNullOrEmptyAfterTrimmed(this string s)
        {
            return (s.IsNullOrEmpty() || s.Trim().IsNullOrEmpty());
        }
        #endregion

        #region ToCharList
        /// <summary>
        /// Converte a string dada em <see cref = "List {Char}" />.
        /// </summary>
        /// <param name = "s"> A string fornecida. </param>
        /// <returns> Retorna uma lista de char (ou null se string for null). </returns>
        public static List<char> ToCharList(this string s)
        {
            return (s.IsNOTNullOrEmpty()) ?
                s.ToCharArray().ToList() :
                null;
        }
        #endregion

        #region SubstringFromXToY
        /// <summary>
        /// Extrai a substring começando da posição 'inicial' até a posição 'final'.
        /// </summary>
        /// <param name = "s"> A string fornecida. </param>
        /// <param name = "start"> A posição inicial. </param>
        /// <param name = "end"> A posição final. </param>
        /// <returns> A substring. </returns>
        public static string SubstringFromXToY(this string s, int start, int end)
        {
            if (s.IsNullOrEmpty())
                return string.Empty;

            // se o início for além do comprimento da corda
            if (start >= s.Length)
                return string.Empty;

            // se a extremidade estiver além do comprimento da corda, redefina
            if (end >= s.Length)
                end = s.Length - 1;

            return s.Substring(start, end - start);
        }
        #endregion

        #region GetWordCount
        /// <summary>
        /// Retorna o número de palavras na string fornecida.
        /// </summary>
        /// <param name = "s"> A string fornecida. </param>
        /// <returns> A contagem de palavras. </returns>
        public static int GetWordCount(this string s)
        {
            return (new Regex(@"\w+")).Matches(s).Count;
        }
        #endregion

        #region IsPalindrome
        /// <summary>
        /// Verifica se a string fornecida é um palíndromo.
        /// </summary>
        /// <param name = "s"> A string fornecida. </param>
        /// <returns> Verdadeiro se a string fornecida for palíndromo, falso caso contrário. </returns>
        public static bool IsPalindrome(this string s)
        {
            return s.Equals(s.Reverse());
        }
        #endregion

        #region IsNotPalindrome
        /// <summary>
        /// Verifica se a string fornecida NÃO é um palíndromo.
        /// </summary>
        /// <param name = "s"> A string fornecida. </param>
        /// <returns> Verdadeiro se a string fornecida NÃO for palíndromo, falso caso contrário. </returns>
        public static bool IsNotPalindrome(this string s)
        {
            return s.IsPalindrome().Toggle();
        }
        #endregion

        #region IsValidIPAddress
        /// <summary>
        /// Verifica se a string fornecida é um endereço IP válido usando expressões regulares.
        /// </summary>
        /// <param name = "s"> A string fornecida. </param>
        /// <returns> Verdadeiro se for um endereço IP válido, caso contrário, falso. </returns>
        public static bool IsValidIPAddress(this string s)
        {
            return Regex.IsMatch(s, @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b");
        }
        #endregion

        #region AppendSep
        /// <summary>
        /// Anexa o separador fornecido à string fornecida.
        /// </summary>
        /// <param name = "s"> A string fornecida. </param>
        /// <param name = "sep"> O separador a ser anexado. </param>
        /// <returns> A string anexada. </returns>
        public static string AppendSep(this string s, string sep)
        {
            return s + sep;
        }
        #endregion

        #region AppendComma
        /// <summary>
        /// Anexa uma vírgula à string necessária.
        /// </summary>
        /// <param name = "s"> Uma string necessária. </param>
        /// <returns> Uma string anexada. </returns>
        public static string AppendComma(this string s)
        {
            return s.AppendSep(",");
        }
        #endregion

        #region AppendNewLine
        /// <summary>
        /// Anexa \ r \ n a uma string
        /// </summary>
        /// <param name = "s"> A string fornecida. </param>
        /// <returns> A string anexada. </returns>
        public static string AppendNewLine(this string s)
        {
            return s.AppendSep("\r\n");
        }
        #endregion

        #region AppendHtmlBr
        /// <summary>
        /// Anexa \ r \ n a uma string
        /// </summary>
        /// <param name = "s"> A string fornecida. </param>
        /// <returns> A string anexada. </returns>
        public static string AppendHtmlBr(this string s)
        {
            return s.AppendSep("<br />");
        }
        #endregion

        #region AppendSpace
        /// <summary>
        /// Anexa um espaço à string fornecida.
        /// </summary>
        /// <param name = "s"> A string fornecida. </param>
        /// <returns> A string anexada. </returns>
        public static string AppendSpace(this string s)
        {
            return s.AppendSep(" ");
        }
        #endregion

        #region AppendHyphen
        /// <summary>
        /// Anexa um hífen à string fornecida.
        /// </summary>
        /// <param name = "s"> A string fornecida. </param>
        /// <returns> A string anexada. </returns>
        public static string AppendHyphen(this string s)
        {
            return s.AppendSep("-");
        }
        #endregion

        #region AppendSep
        /// <summary>
        /// Anexa o caractere fornecido à string fornecida e retorna a nova string.
        /// </summary>
        /// <param name = "s"> A string fornecida. </param>
        /// <param name = "sep"> O caractere a ser anexado. </param>
        /// <returns> A string anexada. </returns>
        public static string AppendSep(this string s, char sep)
        {
            return s.AppendSep(sep.ToString());
        }
        #endregion

        #region AppendWithSep
        /// <summary>
        /// Retorna esta string + sep + newString.
        /// </summary>
        /// <param name = "s"> A string fornecida. </param>
        /// <param name = "newString"> A string a ser concatenada. </param>
        /// <param name = "sep"> O separador a ser introduzido entre essas duas strings. </param>
        /// <returns> A string anexada. </returns>
        /// <remarks> Isso pode dar um desempenho ruim para um grande número de strings usadas no loop. Use <see cref = "StringBuilder" /> em vez disso. </remarks>
        public static string AppendWithSep(this string s, string newString, string sep)
        {
            return s.AppendSep(sep) + newString;
        }
        #endregion

        #region AppendWithSep
        /// <summary>
        /// Retorna esta string + sep + newString.
        /// </summary>
        /// <param name = "s"> A string fornecida. </param>
        /// <param name = "newString"> A string a ser concatenada. </param>
        /// <param name = "sep"> O separador a ser introduzido entre essas duas strings. </param>
        /// <returns> A string anexada. </returns>
        /// <remarks> Isso pode dar um desempenho ruim para um grande número de strings usadas no loop. Use <see cref = "StringBuilder" /> em vez disso. </remarks>
        public static string AppendWithSep(this string s, string newString, char sep)
        {
            return s.AppendSep(sep) + newString;
        }
        #endregion

        #region AppendWithComma
        /// <summary>
        /// Retorna esta string + "," + newString.
        /// </summary>
        /// <param name = "s"> A string fornecida. </param>
        /// <param name = "newString"> A string a ser concatenada. </param>
        /// <returns> A string anexada. </returns>
        /// <remarks> Isso pode dar um desempenho ruim para um grande número de strings usadas no loop. Use <see cref = "StringBuilder" /> em vez disso. </remarks>
        public static string AppendWithComma(this string s, string newString)
        {
            return s.AppendWithSep(newString, ",");
        }
        #endregion

        #region AppendWithNewLine
        /// <summary>
        /// Retorna esta string + "\ r \ n" + newString.
        /// </summary>
        /// <param name = "s"> A string fornecida. </param>
        /// <param name = "newString"> A string a ser concatenada. </param>
        /// <returns> A string anexada. </returns>
        /// <remarks> Isso pode dar um desempenho ruim para um grande número de strings usadas no loop. Use <see cref = "StringBuilder" /> em vez disso. </remarks>
        public static string AppendWithNewLine(this string s, string newString)
        {
            return s.AppendWithSep(newString, "\r\n");
        }
        #endregion

        #region AppendWithHtmlBr
        /// <summary>
        /// Retorna esta string + "\ r \ n" + newString.
        /// </summary>
        /// <param name = "s"> A string fornecida. </param>
        /// <param name = "newString"> A string a ser concatenada. </param>
        /// <returns> A string anexada. </returns>
        /// <remarks> Isso pode dar um desempenho ruim para um grande número de strings usadas no loop. Use <see cref = "StringBuilder" /> em vez disso. </remarks>
        public static string AppendWithHtmlBr(this string s, string newString)
        {
            return s.AppendWithSep(newString, "<br />");
        }
        #endregion

        #region AppendWithHyphen
        /// <summary>
        /// Retorna esta string + "-" + newString.
        /// </summary>
        /// <param name = "s"> A string fornecida. </param>
        /// <param name = "newString"> A string a ser concatenada. </param>
        /// <returns> A string anexada. </returns>
        /// <remarks> Isso pode dar um desempenho ruim para um grande número de strings usadas no loop. Use <see cref = "StringBuilder" /> em vez disso. </remarks>
        public static string AppendWithHyphen(this string s, string newString)
        {
            return s.AppendWithSep(newString, "-");
        }
        #endregion

        #region AppendWithSpace
        /// <resumo>
        /// Retorna esta string + "" + newString.
        /// </summary>
        /// <param name = "s"> A string fornecida. </param>
        /// <param name = "newString"> A string a ser concatenada. </param>
        /// <returns> A string anexada. </returns>
        /// <remarks> Isso pode dar um desempenho ruim para um grande número de strings usadas no loop. Use <see cref = "StringBuilder" /> em vez disso. </remarks>
        public static string AppendWithSpace(this string s, string newString)
        {
            return s.AppendWithSep(newString, " ");
        }
        #endregion

        #region ToTitleCase
        /// <summary>
        /// Converte a string especificada para maiúsculas e minúsculas
        /// (exceto para palavras que estão totalmente em maiúsculas, que são consideradas acrônimos).
        /// </summary>
        /// <param name="mText"></param>
        /// <returns></returns>
        public static string ToTitleCase(this string mText)
        {
            if (mText.IsNullOrEmpty())
                return mText;

            // obter informações de globalização
            System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;

            // converter para caixa do título
            return textInfo.ToTitleCase(mText);
        }
        #endregion

        #region SubsetString
        /// <summary>
        /// Encontra o texto inicial e o texto final especificados nesta instância de string e retorna uma string
        /// contendo todo o texto a partir de startText, até o início de endText. (endText não é
        /// incluído.)
        /// usage: "Este é um testador para meu método de extensão legal !!". Subsetstring ("tester", "cool", true);
        /// Resultado: "testador para meu"
        /// </summary>
        /// <param name = "s"> A string da qual recuperar o subconjunto. </param>
        /// <param name = "startText"> O texto inicial a partir do qual iniciar o subconjunto. </param>
        /// <param name = "endText"> Texto final para onde o subconjunto vai. </param>
        /// <param name = "ignoreCase"> Se deve ou não ignorar maiúsculas e minúsculas ao comparar startText / endText com a string. </param>
        /// <returns> Uma string contendo todo o texto começando de startText, até o início de endText. </returns>
        public static string SubsetString(this string s, string startText, string endText, bool ignoreCase)
        {
            if (s.IsNullOrEmpty())
                return string.Empty;

            if (startText.IsNullOrEmpty() || endText.IsNullOrEmpty())
                throw new ArgumentException("Start Text and End Text cannot be empty.");

            // definir nossos valores iniciais
            string tempStr = ignoreCase ? s.ToUpperInvariant() : s;
            int start = ignoreCase ? tempStr.IndexOf(startText.ToUpperInvariant()) : tempStr.IndexOf(startText);
            int end = ignoreCase ? tempStr.IndexOf(endText.ToUpperInvariant(), start) : tempStr.IndexOf(endText, start);

            // pegue a substring
            return SubstringFromXToY(tempStr, start, end);
        }
        #endregion

        #region PadRightEx
        /// <summary>
        /// Adiciona espaços extras para atender ao comprimento total
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string PadRightEx(this string s, int length)
        {
            // sai se a string já está em comprimento
            if ((!s.IsNullOrEmpty()) && (s.Length >= length))
                return s;

            // se a string é nula, então retorna uma string vazia
            // senão, adicione espaços e saia
            return (s != null) ?
                string.Format("{0}{1}", s, new string(' ', length - s.Length)) :
                new string(' ', length);
        }
        #endregion

        #region AddSpacesBeforeUpperCase
        public static string AddSpacesBeforeUpperCase(this string nonSpacedString)
        {
            if (string.IsNullOrEmpty(nonSpacedString))
                return string.Empty;

            StringBuilder newText = new StringBuilder(nonSpacedString.Length * 2);
            newText.Append(nonSpacedString[0]);

            for (int i = 1; i < nonSpacedString.Length; i++)
            {
                char currentChar = nonSpacedString[i];

                // If it is whitespace, we do not need to add another next to it
                if (char.IsWhiteSpace(currentChar))
                {
                    continue;
                }

                char previousChar = nonSpacedString[i - 1];
                char nextChar = i < nonSpacedString.Length - 1 ? nonSpacedString[i + 1] : nonSpacedString[i];

                if (char.IsUpper(currentChar) && !char.IsWhiteSpace(nextChar)
                    && !(char.IsUpper(previousChar) && char.IsUpper(nextChar)))
                {
                    newText.Append(' ');
                }
                else if (i < nonSpacedString.Length)
                {
                    if (char.IsUpper(currentChar) && !char.IsWhiteSpace(nextChar) && !char.IsUpper(nextChar))
                    {
                        newText.Append(' ');
                    }
                }

                newText.Append(currentChar);
            }

            return newText.ToString();
        }
        #endregion

        #region UpperFirst
        public static string UpperFirst(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);

            return new string(a);
        }
        #endregion

        #region NameFormat
        public static string NameFormat(this string s)
        {
            string newString = s;
           
            newString = newString.AddSpacesBeforeUpperCase();
            newString = newString.UpperFirst();

            return newString;
        }
        #endregion
    }
}