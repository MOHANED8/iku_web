using E_Commmerce.Models;
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

}
