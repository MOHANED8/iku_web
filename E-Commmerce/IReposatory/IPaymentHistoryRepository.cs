<<<<<<< HEAD
﻿using E_Commmerce.Models;
using System.Collections.Generic;

namespace E_Commmerce.IReposatory
{
    public interface IPaymentHistoryRepository
    {
        void SavePayment(PaymentHistory payment);
=======
﻿using E_Commmerce.Models; // For PaymentHistory model
using System.Collections.Generic; // For List<T>

namespace E_Commmerce.IReposatory
{
    // Interface for managing payment history operations
    public interface IPaymentHistoryRepository
    {
        /// <summary>
        /// Saves a new payment history record.
        /// </summary>
        /// <param name="payment">The PaymentHistory object to be saved.</param>
        void SavePayment(PaymentHistory payment);

        /// <summary>
        /// Retrieves all saved payment history records.
        /// </summary>
        /// <returns>A list of all PaymentHistory objects.</returns>
>>>>>>> 69e884f (Initial project upload)
        List<PaymentHistory> GetAllPayments();
    }
}
