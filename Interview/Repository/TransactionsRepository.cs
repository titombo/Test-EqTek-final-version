using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interview.Model;

namespace Interview.Repository
{
    //TODO: You can have an interface where it will be using IoC
    public static class TransactionsRepository
    {
        public static List<TransactionModel> Transactions { get; set; }
    }
}