using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Interview.Model
{
    public class TransactionModel
    {
        //TODO: It can be Guid
        public string Id { get; set; }
        public int ApplicationId { get; set; }
        //TODO: It can be of type enum
        public string Type { get; set; }
        public string Summary { get; set; }
        public decimal Amount { get; set; }
        public DateTime PostingDate { get; set; }
        public bool IsCleared { get; set; }
        public DateTime ClearedDate { get; set; }
    }
}