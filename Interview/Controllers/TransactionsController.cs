using Interview.Model;
using Interview.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Interview.Controllers
{
    //This API is working with Swagger by only adding /swagger at the end of the url you will get the desired result
    //TODO: we could have versioning of the API, in case the API is upgraded
    //TODO: With more time we could also add integration for the API and unit tests for more complicated classes, if there are any
    public class TransactionsController : ApiController
    {
        public TransactionsController()
        {
            //This is a very simple singletone that will be in memory, making it static will have the same result

            //If it would be a transaction that would depend on other contexts in EF we could use Unit of Work pattern so we
            // wouldn't endup with only part of the code updated
            if (TransactionsRepository.Transactions == null)
            {
                //In here we could use IoC, also we can have a real repository or in more complicated scenarios we could
                //have something like a Service layer to deal with business logic
                TransactionsRepository.Transactions = new List<TransactionModel>()
            {
                new TransactionModel()
                {
                        Id = "3f2b12b8-2a06-45b4-b057-45949279b4e5",
                        ApplicationId = 197104,
                        Type = "Debit",
                        Summary = "Payment",
                        Amount = (decimal)58.26,
                        PostingDate = DateTime.Parse("2016-07-01T00:00:00"),
                        IsCleared = true,
                        ClearedDate = DateTime.Parse("2016-07-02T00:00:00")
                },
                new TransactionModel()
                {
                        Id = "d2032222-47a6-4048-9894-11ab8ebb9f69",
                        ApplicationId = 197104,
                        Type = "Debit",
                        Summary = "Payment",
                        Amount = (decimal)58.09,
                        PostingDate = DateTime.Parse("2016-07-01T00:00:00"),
                        IsCleared = true,
                        ClearedDate = DateTime.Parse("2016-07-02T00:00:00")
                },
                new TransactionModel()
                {
                        Id = "194f0d46-6b87-4b59-a73c-e543f035ae1a",
                        ApplicationId = 197104,
                        Type = "Debit",
                        Summary = "Payment",
                        Amount = (decimal)59.43,
                        PostingDate = DateTime.Parse("2016-07-01T00:00:00"),
                        IsCleared = true,
                        ClearedDate = DateTime.Parse("2016-07-02T00:00:00")
                },
                new TransactionModel()
                {
                        Id = "ba67f438-c016-473d-93f8-373ca5a80567",
                        ApplicationId = 197104,
                        Type = "Debit",
                        Summary = "Payment",
                        Amount = (decimal)58.39,
                        PostingDate = DateTime.Parse("2016-07-01T00:00:00"),
                        IsCleared = true,
                        ClearedDate = DateTime.Parse("2016-07-02T00:00:00")
                }
            };
            }
        }

        // GET api/<controller>
        //TODO: Can be called by http://localhost:64874/api/transactions
        [HttpGet]
        //TODO: You can use routing in here for more complex scenarios
        public IHttpActionResult Get()
        {
            var transactions = TransactionsRepository.Transactions.ToList();
            return Json(transactions);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(string id)
        {
            var transaction = TransactionsRepository.Transactions.FirstOrDefault(x => x.Id == id);

            if (transaction == null)
                return NotFound();

            return Json(transaction);
        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult Post([FromBody] TransactionModel transactionModel)
        {
            try
            {
                //TODO: In More complicated scenarios where the ViewModel is different from the Model (Entity) we can
                // also use AutoMapper on the controller, we can create profiles that will help us to make those mappings
                var transaction = TransactionsRepository.Transactions.FirstOrDefault(x => x.Id == transactionModel.Id);

                if (transaction != null)
                    throw new Exception("The transaction with same id was already found");

                TransactionsRepository.Transactions.Add(transactionModel);
                return Json(transactionModel);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        public IHttpActionResult Put([FromBody] TransactionModel transactionModel)
        {
            try
            {
                var transaction = TransactionsRepository.Transactions.FirstOrDefault(x => x.Id == transactionModel.Id);

                if (transaction == null)
                    //TODO: we could also return more information together with the NotFound, depends how will be the usage
                    //of course taking into consideration the security
                    return NotFound();

                TransactionsRepository.Transactions[TransactionsRepository.Transactions.FindIndex(ind => ind.Id.Equals(transactionModel.Id))] = transactionModel;

                return Json(transactionModel);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                var transaction = TransactionsRepository.Transactions.FirstOrDefault(x => x.Id == id);

                if (transaction == null)
                    return NotFound();

                TransactionsRepository.Transactions.Remove(transaction);
                return Json(TransactionsRepository.Transactions);
            }
            catch (Exception ex)
            {
                //TODO: We could better format the message for the end-user
                return Json(ex.Message);
            }
        }
    }
}