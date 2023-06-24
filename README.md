# Forconsol
Una biblioteca originalmente pensada para aplicaciones de consola, pero contiene programas también válidos para otros tipos de proyectos de .NET 7.0. Uso libre por cualquiera. Información y algunos ejemplos sencillos disponibles en YouTube:
- Presentación: https://youtu.be/_mHUmgFVNyw
- SolicitudDeEntrada\<TResultado\> Clase: https://youtu.be/F9BnFdjLLiE
- Fraccion y NumeroRacional: https://youtu.be/x5zicIp8NLo
- MensajeDeCarga Clase: https://youtu.be/rwM7ZbNtBK8
- MenuDeOpciones Clase: https://youtu.be/zpZRbROXMdw
- TablaImprimible\<T\> Clase: https://youtu.be/Vgpd9qRPNgA

# Fraccion Estructura
Ejemplos de formatos de Fraccion válidos (métodos Fraccion.Parse(String) y Fraccion.TryParse(String, Fraccion)):
- `"15"`
- `"15 / 11"`
- `" 15 / 11 "`
- `"-15/11"`
- `"15 /-11"`
- `"-15/   -11"`

## Importante
Al crear un arreglo de Fraccion (o cualquier value type en general) todos sus elementos tienen el valor predeterminado de Fraccion, eso es la unión de todos los valores predeterminados de los campos definidos en Fraccion. Esto significa que el constructor de Fraccion que no recibe parámetros, aquel que establece a Denominador como 1, no es invocado, trayendo varios problemas. Considerar el uso del método Array.Fill\<T\>(T[], T) antes de realizar operaciones con el arreglo de Fraccion.
Fuentes (en inglés):
- https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-10.0/parameterless-struct-constructors#array-allocation
- https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/default-values
