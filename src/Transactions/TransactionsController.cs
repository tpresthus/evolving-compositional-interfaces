using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Transactions
{
    [Route("/")]
    public class TransactionsController : Controller
    {
        private readonly TransactionRepository repository;

        public TransactionsController(TransactionRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult List()
        {
            return Ok(new { Transactions = repository.All() });
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var transaction = repository.Get(id);
            if (transaction == null)
                return NotFound(new {Error = "No such transaction"});

            return Ok(transaction);
        }

        [HttpPost]
        [Route("{id}/capture")]
        public IActionResult Capture(int id, [FromBody] AmountRequest request)
        {
            var transaction = repository.Get(id);
            if (transaction == null)
                return NotFound(new {Error = "No such transaction"});

            if (!transaction.CaptureAllowed)
                return BadRequest(new {Error = "Requested amount can not be captured"});

            transaction.Capture(request.Amount);

            return RedirectToAction("get", new {id});
        }

        [HttpPost]
        [Route("{id}/reversal")]
        public IActionResult Reverse(int id, [FromBody] AmountRequest request)
        {
            var transaction = repository.Get(id);
            if (transaction == null)
                return NotFound(new {Error = "No such transaction"});

            if (!transaction.ReversalAllowed)
                return BadRequest(new {Error = "Requested amount can not be reversed"});

            transaction.Reverse(request.Amount);

            return RedirectToAction("get", new {id});
        }

        public class AmountRequest
        {
            [Required]
            public decimal Amount { get; set; }
        }
    }
}