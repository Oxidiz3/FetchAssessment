using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchAssessment.Models
{
    public class Item
    {
        public required string ShortDescription { get; set; }
        public required double Price { get; set; }
    }
}
