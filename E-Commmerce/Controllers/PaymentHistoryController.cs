using Microsoft.AspNetCore.Mvc;
using E_Commmerce.Models;
using E_Commmerce.IReposatory;

namespace E_Commmerce.Controllers
{
    public class PaymentHistoryController : Controller
    {
        private readonly IPaymentHistoryRepository _paymentHistoryRepository;

        public PaymentHistoryController(IPaymentHistoryRepository paymentHistoryRepository)
        {
            _paymentHistoryRepository = paymentHistoryRepository;
        }

        public IActionResult Index()
        {
            // Fetch payment history
            var paymentHistories = _paymentHistoryRepository.GetAllPayments();
            return View(paymentHistories); // Correctly pass the model
        }
    }
}
