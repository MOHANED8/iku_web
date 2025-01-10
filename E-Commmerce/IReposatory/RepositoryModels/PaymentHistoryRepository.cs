<<<<<<< HEAD
﻿using E_Commmerce.Models;
using System.Collections.Generic;

namespace E_Commmerce.IReposatory.RepositoryModels
{
    public class PaymentHistoryRepository : IPaymentHistoryRepository
    {
        private static readonly List<PaymentHistory> _paymentHistories = new List<PaymentHistory>();

        public void SavePayment(PaymentHistory payment)
        {
            // Her ödeme için yeni bir ID atayın
            payment.Id = _paymentHistories.Count + 1;
            _paymentHistories.Add(payment);
        }

        public List<PaymentHistory> GetAllPayments()
        {
            return _paymentHistories;
        }
    }

=======
﻿using E_Commmerce.Models; // For PaymentHistory model
using System.Collections.Generic; // For List<T>

namespace E_Commmerce.IReposatory.RepositoryModels
{
    // Repository for managing payment history in-memory
    public class PaymentHistoryRepository : IPaymentHistoryRepository
    {
        // Static list to store payment histories in memory
        private static readonly List<PaymentHistory> _paymentHistories = new List<PaymentHistory>();

        // Saves a new payment history to the list
        public void SavePayment(PaymentHistory payment)
        {
            // Assign a unique ID to each payment
            payment.Id = _paymentHistories.Count + 1;

            // Add the payment to the in-memory list
            _paymentHistories.Add(payment);
        }

        // Retrieves all payment histories
        public List<PaymentHistory> GetAllPayments()
        {
            return _paymentHistories; // Return the list of payments
        }
    }
>>>>>>> 69e884f (Initial project upload)
}
