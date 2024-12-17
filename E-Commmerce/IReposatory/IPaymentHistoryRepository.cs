using E_Commmerce.Models;
using System.Collections.Generic;

namespace E_Commmerce.IReposatory
{
    public interface IPaymentHistoryRepository
    {
        void SavePayment(PaymentHistory payment);
        List<PaymentHistory> GetAllPayments();
    }
}
