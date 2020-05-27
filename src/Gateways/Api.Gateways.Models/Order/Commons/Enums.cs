using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Gateways.Models.Order.Commons
{
    public class Enums
    {
        public enum OrderStatus
        {
            Cancel,
            Pending,
            Approved
        }

        public enum OrderPayment
        {
            CreditCard,
            PayPal,
            BankTransfer
        }
    }
}
