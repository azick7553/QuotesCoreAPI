using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuotesCoreAPI.Models;

namespace QuotesCoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotesAPIController : ControllerBase
    {
        public static List<Quotes> QuotesList = new List<Quotes>()
        {
            new Quotes()
            {
                Id = 1,
                Author = "Mahatma Gandhi",
                Quote = "Live as if you were to die tomorrow. Learn as if you were to live forever.",
                Category = "Philosophy",
                CreatedDate = DateTime.Now.AddMinutes(-50)
            },
            new Quotes()
            {
                Id = 2,
                Author = "Theodore Roosevelt",
                Quote = "Do what you can, with what you have, where you are.",
                Category = "Classic",
                CreatedDate = DateTime.Now.AddHours(-1)

            },
            new Quotes()
            {
                Id = 3,
                Author = "Charles Kettering",
                Quote = "Believe and act as if it were impossible to fail.",
                Category = "Philosophy",
                CreatedDate = DateTime.Now
            }
        };

        [HttpGet]
        [Route("GetRandomQuote")]
        public IActionResult GetRandomQuote()
        {
            var quote = QuotesList[new Random().Next(QuotesList.Count)];
            return Ok(quote);
        }

        [HttpGet]
        [Route("GetAllQuotes")]
        public IActionResult GetAllQuotes()
        {
            return Ok(QuotesList);
        }

        [HttpGet]
        [Route("GetAllQuotesByCategory")]
        public IActionResult GetAllQuotesByCategory(string category)
        {
            return Ok(QuotesList.Where(p => p.Category.ToLower().Contains(category.ToLower())));
        }

        [Route("AddQuote")]
        [HttpPost]
        public IActionResult AddQuote([FromBody] Quotes quote)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var incrId = QuotesList.Select(p => p.Id).Max();
            incrId++; //Imitation of auto increment
            quote.Id = incrId;
            quote.CreatedDate = DateTime.Now;
            QuotesList.Add(quote);
            return Ok(new { massage = "Successfully added!" });
        }
        [Route("DeleteQuoteByID/{id}")]
        [HttpDelete]
        public IActionResult DeleteQuoteByID(int id)
        {
            if (QuotesList.Remove(QuotesList.Find(p => p.Id == id)))
            {
                return Ok(new { massage = "Successfully removed!" });
            }
            else
            {
                return BadRequest(new { massage = "Quote not found!" });
            }
        }
        [Route("EditQuote/{id}")]
        [HttpPut]
        public IActionResult EditQuote([FromRoute]int id, [FromBody] Quotes quote)
        {
            bool isModified = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            QuotesList.ForEach(p =>
            {
                if (p.Id == id)
                {
                    p.Quote = quote.Quote;
                    p.Author = quote.Author;
                    p.Category = quote.Category;
                    isModified = true;
                }
            });

            if (isModified)
            {
                return Ok(new { massage = "Successfully updated!" });
            }
            else
            {
                return BadRequest(new { massage = "Not found" });
            }
        }
    }
}
