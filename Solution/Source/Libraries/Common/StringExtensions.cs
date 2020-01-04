using System.Globalization;
using System.Linq;

namespace System
{
    /// <summary>
    ///     Extends the <see cref="string" /> type.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Formats the given template by placing the given arguments in order in the placeholders that template may
        ///     contain.
        /// </summary>
        /// <param name="template">The <see cref="string" /> being the template to format.</param>
        /// <param name="arguments">
        ///     An <see cref="Array" /> of objects which string representation will be
        ///     included in the template's placeholders.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="template" /> or
        ///     <paramref name="arguments" /> is null.
        /// </exception>
        /// <returns>A <see cref="string" /> being the formatted string.</returns>
        public static string EasyFormat(this string template, params object[] arguments)
        {
            return template.EasyFormat(CultureInfo.InvariantCulture, arguments);
        }

        /// <summary>
        ///     Formats the given template by placing the given arguments in order in the placeholders that template may
        ///     contain, all this using the format provider specified by the given culture information.
        /// </summary>
        /// <param name="template">The <see cref="string" /> being the template to format.</param>
        /// <param name="culture">
        ///     A <see cref="CultureInfo" /> instance to get a format provider from to be
        ///     used in the formatting.
        /// </param>
        /// <param name="arguments">
        ///     An <see cref="Array" /> of objects which string representation will be
        ///     included in the template's placeholders.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="template" />, <paramref name="arguments" /> or <paramref name="culture" /> is null.
        /// </exception>
        /// <returns>A <see cref="System.String" /> being the formatted string.</returns>
        public static string EasyFormat(this string template, CultureInfo culture, params object[] arguments)
        {
            if (template == null) throw new ArgumentNullException("template");
            if (culture == null) throw new ArgumentNullException("culture");
            if (arguments == null) throw new ArgumentNullException("arguments");

            return string.Format(culture, template, arguments);
        }

        /// <summary>
        ///     Capitalizes the given text.
        /// </summary>
        /// <param name="text">The <see cref="string" /> to capitalize.</param>
        /// <exception cref="ArgumentNullException"><paramref name="text" /> is null.</exception>
        /// <returns>A copy of the given <see cref="string" /> with the first letter in upper-case.</returns>
        public static string Capitalize(this string text)
        {
            if (text == null) throw new ArgumentNullException("text");
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException(Properties.Resources.MustBeNonEmptyString, "text");
            if (!char.IsLetter(text[0]))
                throw new ArgumentException(Properties.Resources.CannotCapitalizeStringNotStartingWithLetter);

            return text.Aggregate(string.Empty, (s, c) =>
            {
                string cs = c.ToString(CultureInfo.InvariantCulture);
                return s += s.Length == 0 ? cs.ToUpper() : cs;
            });
        }
    }
}