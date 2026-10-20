using System;
using System.Collections.Generic;
using System.Text;

namespace CarSales.Models
{
    /// <summary>
    /// Individual car sales.
    /// </summary>
    public class Sale
    {
        public string Name { get; set; } = string.Empty;
        public DateTime Date {  get; set; }
        public double Price { get; set; }
        public double Tax { get; set; }


        public double PriceWithTax => Price * (1 + Tax/100);
        public string FormattedPrice => (Price % 1 == 0) ? string.Format("{0},-", Price.ToString("#,##0", Helpers.CultureConfig.PriceCulture)) : Price.ToString("#,##0.00", Helpers.CultureConfig.PriceCulture);
    }
}
