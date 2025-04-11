using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FetchAssessment.Models
{
    public class Receipt
    {
        public required string Retailer { get; set; }
        public required DateTime PurchaseDate { get; set; }
        public required string PurchaseTime { get; set; }
        public required List<Item> Items { get; set; }
        public required double Total { get; set; }
    }
}
