<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Mvc;
using E_Commmerce.Models;
using E_Commmerce.IReposatory;
=======
﻿using Microsoft.AspNetCore.Mvc; // For controller functionalities and action result classes
using E_Commmerce.Models; // Models for payment history and related data
using E_Commmerce.IReposatory; // Interface for the PaymentHistory repository
>>>>>>> 69e884f (Initial project upload)

namespace E_Commmerce.Controllers
{
    public class PaymentHistoryController : Controller
    {
<<<<<<< HEAD
        private readonly IPaymentHistoryRepository _paymentHistoryRepository;

=======
        private readonly IPaymentHistoryRepository _paymentHistoryRepository; // Repository for managing payment history data

        // Constructor to initialize the repository
>>>>>>> 69e884f (Initial project upload)
        public PaymentHistoryController(IPaymentHistoryRepository paymentHistoryRepository)
        {
            _paymentHistoryRepository = paymentHistoryRepository;
        }

<<<<<<< HEAD
        public IActionResult Index()
        {
            // Fetch payment history
            var paymentHistories = _paymentHistoryRepository.GetAllPayments();
            return View(paymentHistories); // Correctly pass the model
        }
    }
}
=======
        // Displays the payment history page
        public IActionResult Index()
        {
            // Fetch all payment histories from the repository
            var paymentHistories = _paymentHistoryRepository.GetAllPayments();

            // Pass the payment history data to the view
            return View(paymentHistories);
        }
    }
}

>>>>>>> 69e884f (Initial project upload)
