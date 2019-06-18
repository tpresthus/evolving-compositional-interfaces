using System;
using System.Threading.Tasks;
using Admin.Authorization;
using Admin.Navigation;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Transactions
{
    [Route("transactions")]
    public class TransactionController : Controller
    {
        private readonly TransactionService transactionService;
        private readonly NavigationService navigationService;
        private readonly AuthorizationService authorizationService;

        public TransactionController(TransactionService transactionService, NavigationService navigationService, AuthorizationService authorizationService)
        {
            this.transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
            this.navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            this.authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        }

        public async Task<IActionResult> Index()
        {
            var menu = await this.navigationService.GetMenuAsync();
            var user = await this.authorizationService.GetAuthorizedUserAsync();
            var transactions = await this.transactionService.GetTransactions();
            var urlFactory = new UrlFactory(Url);
            var transactionsViewModel = new TransactionsViewModel(transactions, menu, user, urlFactory);

            return View(transactionsViewModel);
        }

        [Route("{id}")]
        public async Task<IActionResult> CustomerTransactions(string id)
        {
            var menu = await this.navigationService.GetMenuAsync();
            var user = await this.authorizationService.GetAuthorizedUserAsync();
            var transactions = await this.transactionService.GetTransactionsForCustomer(id);
            var urlFactory = new UrlFactory(Url);
            var transactionsViewModel = new TransactionsViewModel(transactions, menu, user, urlFactory);

            return View("Index", transactionsViewModel);
        }
        
        [Route("t/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var menu = await this.navigationService.GetMenuAsync();
            var user = await this.authorizationService.GetAuthorizedUserAsync();
            var transaction = await this.transactionService.GetTransaction(id);
            var urlFactory = new UrlFactory(Url);
            var transactionViewModel = new TransactionDetailsViewModel(transaction, menu, user, urlFactory);

            return View("Details", transactionViewModel);
        }
        
        [HttpPost]
        [Route("t/{id}/capture")]
        public async Task<IActionResult> Capture([FromRoute] int id, [FromForm] CaptureFormModel captureFormModel)
        {
            await this.transactionService.CaptureTransaction(id, captureFormModel.AmountToCapture);
            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        [Route("t/{id}/reversal")]
        public async Task<IActionResult> Reverse([FromRoute] int id, [FromForm] ReverseFormModel reverseFormModel)
        {
            await this.transactionService.ReverseTransaction(id, reverseFormModel.AmountToReverse);
            return RedirectToAction("Details", new { id });
        }
    }
}
