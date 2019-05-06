using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WL.Application.Documents.Commands {

  public class IssueAnalizer {

    public static string ObtenerAsunto(string texto)
   => LimpiarAsunto(ObtenerAsuntoSinLimpiar(texto));

    static string LimpiarAsunto(string asuntoSinLimpiar)
       => asuntoSinLimpiar.Trim().Replace("\r\n", string.Empty);

    static string ObtenerAsuntoSinLimpiar(string text) {
      try {
        var paragraphs = SplitInParagraphs(text);
        int i;
        for (i = 0; i < paragraphs.Length(); i++) {
          var (coincideConAsunto, coincidencia) = CoincideConAlgunAsunto(paragraphs[i]);
          if (coincideConAsunto) {
            if (TieneMuyPocaLongitud(paragraphs[i], coincidencia)) {
              return paragraphs[i + 1];
            }
            if (paragraphs[i].Length < 90 && SiguienteRenglonTienePuntoAlFinal(paragraphs, i)) {
              return paragraphs[i].Trim() + " " + paragraphs[i + 1].Trim();
            }
            return paragraphs[i];
          }
        }
        return "";
      } catch (Exception ex) {
        Console.WriteLine(ex);
        return "";
      }
    }

    static (bool coincide, string textoCoincidente) CoincideConAlgunAsunto(string parrafo) {
      var match = new Regex(@"ASUNTO|Por el cual|Por la cual|Por lo cual",
         RegexOptions.IgnoreCase).Match(parrafo);
      return (match.Success, match.Value);
    }

    static bool SiguienteRenglonTienePuntoAlFinal(List<string> paragraphs, int actualIndex)
       => paragraphs[actualIndex + 1].Trim()
          .EndsWith(".", StringComparison.CurrentCulture);

    static bool TieneMuyPocaLongitud(string textoLinea, string cadenaQueDeterminaLaLongitud) {
      var longitudRequisito = cadenaQueDeterminaLaLongitud.Length + 3;
      var longitudLinea = textoLinea.Length;
      return longitudLinea <= longitudRequisito;
    }

    static List<string> SplitInParagraphs(string text)
       => text.Split(new[] { Environment.NewLine },
              StringSplitOptions.RemoveEmptyEntries)
           .ToList();
  }
}