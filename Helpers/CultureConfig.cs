using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CarSales.Helpers
{
    /// <summary>
    /// Custom formating of numbers with a dot every 3 digits and a decimal coma.
    /// </summary>
    public static class CultureConfig
    {
        public static CultureInfo PriceCulture { get; } = CreatePriceCulture();

        private static CultureInfo CreatePriceCulture()
        {
            var culture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            culture.NumberFormat.NumberGroupSeparator = ".";
            culture.NumberFormat.NumberDecimalSeparator = ",";
            return culture;
        }

    }
}
