using System;
using System.Collections.Generic;
using System.Text;

namespace CarSales.ViewModels
{
    /// <summary>
    /// Represents individual models of a car (such as Fabia or Favorit).
    /// </summary>
    /// <remarks>
    /// I didn't want to name this class Model even though it refers to a specific model of a car, Series was the best synonym I found.
    /// </remarks>
    public class SeriesSale
    {
        public string Name { get; set; } = string.Empty;
        public double TotalPrice {  get; set; }
        public double TotalPriceWithTax {  get; set; }

        public string FormattedTotalPrice => TotalPrice.ToString("#,##0.00", Helpers.CultureConfig.PriceCulture);
        public string FormattedTotalPriceWithTax => TotalPriceWithTax.ToString("#,##0.00", Helpers.CultureConfig.PriceCulture);
    }
}
