namespace Forconsol
{
    /// <summary>
    /// Proporciona métodos estáticos para convertir números de coma flotante y fracciones. Esta clase no puede ser heredada.
    /// </summary>
    public static class NumeroRacional
    {
        private static string cadenaReal;
        private static bool hasSigno;

        internal static void GetNumeradorYDenominador(decimal decimal_, out int numerador, out int denominador)
        {
            if (decimal_ <= int.MinValue - 1m || decimal_ >= int.MaxValue + 1m)
            {
                throw new OverflowException("El valor fue o demasiado grande o demasiado pequeño para una Fraccion.");
            }
            decimal_ = (decimal_ < -100000000 || decimal_ > 100000000) ? Math.Truncate(decimal_) : Math.Round(decimal_, 9);

            if (decimal.IsInteger(decimal_))
            {
                numerador = (int)decimal_;
                denominador = 1;
            }
            else
            {
                hasSigno = false;

                if (decimal.IsNegative(decimal_))
                {
                    decimal_ *= -1;
                    hasSigno = true;
                }
                if (decimal.Parse(decimal_.ToString().Replace(".", null)) > int.MaxValue)
                {
                    decimal_ = Math.Round(decimal_, 9 - decimal_.ToString().IndexOf('.'));
                }
                denominador = (int)Math.Pow(10, GetParteDecimal(decimal_).Length);
                numerador = (int)(Math.Truncate(decimal_) * denominador + GetParteDecimalSinCerosALaIzquierda(decimal_));

                if (hasSigno)
                {
                    numerador *= -1;
                }
            }
        }

        /// <summary>
        /// Devuelve la parte decimal del decimal especificado.
        /// </summary>
        /// <param name="decimal_">El decimal para el cual obtener su parte decimal.</param>
        /// <returns>La parte decimal del decimal especificado.</returns>
        public static string? GetParteDecimal(decimal decimal_)
        {
            return decimal.IsInteger(decimal_) ? null : Math.Abs(decimal_ - Math.Truncate(decimal_)).ToString().Substring(2);
        }

        /// <summary>
        /// Devuelve la parte decimal sin ceros a la izquierda del decimal especificado.
        /// </summary>
        /// <param name="decimal_">El decimal para el cual obtener su parte decimal sin ceros a la izquierda.</param>
        /// <returns>La parte decimal sin ceros a la izquierda dek decimal especificado.</returns>
        public static decimal? GetParteDecimalSinCerosALaIzquierda(decimal decimal_)
        {
            return decimal.IsInteger(decimal_) ? null : decimal.Parse(Math.Abs(decimal_ - Math.Truncate(decimal_)).ToString().Substring(2));
        }

        /// <summary>
        /// Determina si un double tiene notación científica.
        /// </summary>
        /// <param name="real">El double a comprobar.</param>
        /// <returns><see langword="true"/> si la <see cref="double"/> tiene notación científica; en caso contrario, <see langword="false"/>.</returns>
        public static bool HasNotacionCientifica(double real)
        {
            return real.ToString().Contains('E');
        }

        /// <summary>
        /// Determina si un float tiene notación científica.
        /// </summary>
        /// <param name="real">El float a comprobar.</param>
        /// <returns><see langword="true"/> si la <see cref="float"/> tiene notación científica; en caso contrario, <see langword="false"/>.</returns>
        public static bool HasNotacionCientifica(float real)
        {
            return real.ToString().Contains('E');
        }

        /// <summary>
        /// Determina si un double tiene notación decimal.
        /// </summary>
        /// <param name="real">El double a comprobar.</param>
        /// <returns><see langword="true"/> si la <see cref="double"/> tiene notación decimal; en caso contrario, <see langword="false"/>.</returns>
        public static bool HasNotacionDecimal(double real)
        {
            return !HasNotacionCientifica(real);
        }

        /// <summary>
        /// Determina si un float tiene notación decimal.
        /// </summary>
        /// <param name="real">El float a comprobar.</param>
        /// <returns><see langword="true"/> si la <see cref="float"/> tiene notación decimal; en caso contrario, <see langword="false"/>.</returns>
        public static bool HasNotacionDecimal(float real)
        {
            return !HasNotacionCientifica(real);
        }

        /// <summary>
        /// Convierte un double expresado en notación científica a una cadena equivalente a la notación decimal de dicho double.
        /// </summary>
        /// <param name="real">Un double a convertir.</param>
        /// <returns>Una cadena equivalente a la notación decimal de <paramref name="real"/>.</returns>
        public static string NotacionCientificaADecimal(double real)
        {
            if (HasNotacionCientifica(real))
            {
                short exponente;

                hasSigno = false;

                if (double.IsNegative(real))
                {
                    real *= -1;
                    hasSigno = true;
                }
                cadenaReal = real.ToString();
                short.TryParse(cadenaReal.Substring(cadenaReal.IndexOf('E') + 1), out exponente);
                cadenaReal = cadenaReal.Remove(cadenaReal.IndexOf('E')).Replace(".", null);
                cadenaReal = short.IsPositive(exponente) ? cadenaReal.PadRight(exponente + 1, '0') : "0." + cadenaReal.PadLeft(-exponente - 1 + cadenaReal.Length, '0');
                if (hasSigno)
                {
                    cadenaReal = '-' + cadenaReal;
                }
                return cadenaReal;
            }
            else
            {
                return real.ToString();
            }
        }

        /// <summary>
        /// Convierte un float expresado en notación científica a una cadena equivalente a la notación decimal de dicho float.
        /// </summary>
        /// <param name="real">Un float a convertir.</param>
        /// <returns>Una cadena equivalente a la notación decimal de <paramref name="real"/>.</returns>
        public static string NotacionCientificaADecimal(float real)
        {
            if (HasNotacionCientifica(real))
            {
                sbyte exponente;

                hasSigno = false;

                if (float.IsNegative(real))
                {
                    real *= -1;
                    hasSigno = true;
                }
                cadenaReal = real.ToString();
                sbyte.TryParse(cadenaReal.Substring(cadenaReal.IndexOf('E') + 1), out exponente);
                cadenaReal = cadenaReal.Remove(cadenaReal.IndexOf('E')).Replace(".", null);
                cadenaReal = sbyte.IsPositive(exponente) ? cadenaReal.PadRight(exponente + 1, '0') : "0." + cadenaReal.PadLeft(-exponente - 1 + cadenaReal.Length, '0');
                if (hasSigno)
                {
                    cadenaReal = '-' + cadenaReal;
                }
                return cadenaReal;
            }
            else
            {
                return real.ToString();
            }
        }

        /// <summary>
        /// Convierte la representación en cadena de un número de coma flotante expresado en notación científica a una cadena equivalente o aproximada a la notación decimal de dicho número.
        /// </summary>
        /// <param name="real">Una cadena a convertir.</param>
        /// <returns>Una cadena equivalente o aproximada a la notación decimal de <paramref name="real"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        public static string NotacionCientificaADecimal(string real)
        {
            return NotacionCientificaADecimal(double.Parse(real));
        }

        /// <summary>
        /// Convierte un double expresado en notación decimal a una cadena equivalente a la notación científica de dicho double.
        /// </summary>
        /// <param name="real">Un double a convertir.</param>
        /// <returns>Una cadena equivalente a la notación científica de <paramref name="real"/>.</returns>
        public static string NotacionDecimalACientifica(double real)
        {
            if (real == 0 || (real >= 1 && real < 10) || (real > -10 && real <= -1))
            {
                return real + "E+00";
            }
            else if (HasNotacionDecimal(real))
            {
                hasSigno = false;

                if (double.IsNegative(real))
                {
                    real *= -1;
                    hasSigno = true;
                }
                cadenaReal = real.ToString();
                cadenaReal =
                    (real >= 10)
                    ?
                        double.IsInteger(real)
                        ?
                            (cadenaReal[cadenaReal.Length - 1] == '0') ? (double.Parse(cadenaReal.Insert(1, ".")) + "E+").Replace(".", null) + (cadenaReal.Length - 1).ToString().PadLeft(2, '0') : cadenaReal + "E+" + (cadenaReal.Length - 1).ToString().PadLeft(2, '0')
                        :
                            cadenaReal.Replace(".", null) + "E+" + (cadenaReal.IndexOf('.') - 1).ToString().PadLeft(2, '0')
                    :
                        GetParteDecimalSinCerosALaIzquierda((decimal)real) + "E-" + (cadenaReal.IndexOf(GetParteDecimalSinCerosALaIzquierda((decimal)real).ToString()[0]) - 1).ToString().PadLeft(2, '0')
                    ;
                if (cadenaReal[1] != 'E')
                {
                    cadenaReal = cadenaReal.Insert(1, ".");
                }
                if (hasSigno)
                {
                    cadenaReal = '-' + cadenaReal;
                }
                return cadenaReal;
            }
            else
            {
                return real.ToString();
            }
        }

        /// <summary>
        /// Convierte un float expresado en notación decimal a una cadena equivalente a la notación científica de dicho float.
        /// </summary>
        /// <param name="real">Un float a convertir.</param>
        /// <returns>Una cadena equivalente a la notación científica de <paramref name="real"/>.</returns>
        public static string NotacionDecimalACientifica(float real)
        {
            if (real == 0 || (real >= 1 && real < 10) || (real > -10 && real <= -1))
            {
                return real + "E+00";
            }
            else if (HasNotacionDecimal(real))
            {
                hasSigno = false;

                if (float.IsNegative(real))
                {
                    real *= -1;
                    hasSigno = true;
                }
                cadenaReal = real.ToString();
                cadenaReal =
                    (real >= 10)
                    ?
                        float.IsInteger(real)
                        ?
                            (cadenaReal[cadenaReal.Length - 1] == '0') ? (float.Parse(cadenaReal.Insert(1, ".")) + "E+").Replace(".", null) + (cadenaReal.Length - 1).ToString().PadLeft(2, '0') : cadenaReal + "E+" + (cadenaReal.Length - 1).ToString().PadLeft(2, '0')
                        :
                            cadenaReal.Replace(".", null) + "E+" + (cadenaReal.IndexOf('.') - 1).ToString().PadLeft(2, '0')
                    :
                        GetParteDecimalSinCerosALaIzquierda((decimal)real) + "E-" + (cadenaReal.IndexOf(GetParteDecimalSinCerosALaIzquierda((decimal)real).ToString()[0]) - 1).ToString().PadLeft(2, '0')
                    ;
                if (cadenaReal[1] != 'E')
                {
                    cadenaReal = cadenaReal.Insert(1, ".");
                }
                if (hasSigno)
                {
                    cadenaReal = '-' + cadenaReal;
                }
                return cadenaReal;
            }
            else
            {
                return real.ToString();
            }
        }

        /// <summary>
        /// Convierte la representación en cadena de un número de coma flotante expresado en notación decimal a una cadena equivalente o aproximada a la notación científica de dicho número.
        /// </summary>
        /// <param name="real">Una cadena a convertir.</param>
        /// <returns>Una cadena equivalente o aproximada a la notación científica de <paramref name="real"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        public static string NotacionDecimalACientifica(string real)
        {
            return NotacionDecimalACientifica(double.Parse(real));
        }

        /// <summary>
        /// Convierte un decimal en una fracción.
        /// </summary>
        /// <param name="decimal_">Un decimal a convertir.</param>
        /// <returns>Una fracción equivalente o aproximada al número contenido en <paramref name="decimal_"/>.</returns>
        /// <exception cref="OverflowException"></exception>
        public static Fraccion ToFraccion(this decimal decimal_)
        {
            GetNumeradorYDenominador(decimal_, out int numerador, out int denominador);

            return new(numerador, denominador);
        }
    }
}
