using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FetchAssessment.Models;
using Microsoft.AspNetCore.Mvc;

namespace FetchAssessment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReceiptsController : ControllerBase
    {
        private static Dictionary<string, Receipt> ReceiptStorage = [];

        // Post "receipts/process"
        [HttpPost("process")]
        public IActionResult ProcessReceipts([FromBody] Receipt receipt)
        {
            if (receipt == null)
            {
                return BadRequest("Receipt is null");
            }

            var guid = Guid.NewGuid().ToString();

            ReceiptStorage.Add(guid, receipt);

            return Ok(
                new Dictionary<string, string>()
                {
                   {"id", guid}
                }
            );
        }

        // Get "receipts/{id}/process"
        [HttpGet("{id}/process")]
        public IActionResult GetPoints(string id)
        {
            if (!int.TryParse(id, out var _))
            {
                return BadRequest("The receipt is invalid");
            }
            if (ReceiptStorage.TryGetValue(id, out var receipt))
            {
                int totalPoints = 0;

                // One point for every alphanumeric character in the retailer name.
                totalPoints += Regex.Count(receipt.Retailer, "[a-zA-Z0-9]");
                // 50 points if the total is a round dollar amount with no cents.
                totalPoints += receipt.Total % 1 == 0 ? 50 : 0;
                // 25 points if the total is a multiple of 0.25.
                totalPoints += receipt.Total % 0.25 == 0 ? 25 : 0;
                // 5 points for every two items on the receipt.
                totalPoints += 5 * (int)Math.Floor((double)receipt.Items.Count/2);
                // If the trimmed length of the item description is a multiple of 3, multiply the price by 0.2 and round up to the nearest integer. The result is the number of points earned.
                foreach( var item in receipt.Items)
                {
                    totalPoints += item.ShortDescription.Trim().Length % 3 == 0 ? (int)Math.Ceiling(0.2 * item.Price) : 0;
                }
                // 6 points if the day in the purchase date is odd.
                totalPoints += receipt.PurchaseDate.Day % 2 == 1 ? 6 : 0;

                // 10 points if the time of purchase is after 2:00pm and before 4:00pm.
                var purchaseTime = DateTime.ParseExact(receipt.PurchaseTime, "HH:mm", CultureInfo.InvariantCulture);
                totalPoints += purchaseTime.Hour >= 14 && purchaseTime.Hour < 16 ? 10 : 0;

                
                return Ok(
                    new Dictionary<string, int>
                    {
                        { "points", totalPoints}
                    }
                    );
            }
            return NotFound("The receipt is invalid");
        }
    }
}
