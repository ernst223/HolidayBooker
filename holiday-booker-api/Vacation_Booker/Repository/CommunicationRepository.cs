using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vacation_Booker.Entities;
using Vacation_Booker.Models;
using PostmarkDotNet;

namespace Vacation_Booker.Repository
{
    public class CommunicationRepository
    {
        private MyContext dc;
        public CommunicationRepository(MyContext T)
        {
            dc = T;
        }
        //private string EmailFrom = "info@dankospark.co.za";
        private string EmailFrom = "info@holidaybooker.co.za";
        public bool Enquiry(EnquiryDto T)
        {
            try
            {
                var stock = dc.Vacations.Where(o => o.Id == T.StockId).FirstOrDefault();
                var resort = dc.Resorts.Where(o => o.Id == stock.ResortId).FirstOrDefault();
                var supplier = dc.Suppliers.Where(o => o.Id == stock.SupplierId).FirstOrDefault();
                var unitSize = dc.UnitSizes.Where(o => o.Id == stock.UnitSizeId).FirstOrDefault();
                EmailToAdminAsync(T, stock, resort, supplier, unitSize);
                //EmailToProviderAsync(T, stock, resort, supplier, unitSize);
                EmailToClientAsync(T, stock, resort, supplier, unitSize);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool NotifyClient(int id, string email, string name, string lastname)
        {
            try
            {
                //Setting the vacations status to On Hold
                dc.Vacations.Remove(dc.Vacations.Where(o => o.Id == id).First());
                dc.SaveChanges();

                //Send Email
                SendNotificationAsync(id, email, name, lastname);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool InvoiceClient(int id, string email, string name, string lastname, string dob
            , string cell, int kids, int adults)
        {
            try
            {
                //Setting the vacations status to Sold
                var data = dc.Vacations.Where(o => o.Id == id).First();
                data.Hold = true;
                dc.SaveChanges();

                //Send Email to Client
                SendInvoiceAsync(id, email, name, lastname, dob, cell, kids, adults);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool NewEmailSubmitted(string email)
        {
            try
            {
                SendNewEmailAsync(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task SendNewEmailAsync(string email)
        {
            var message = new TemplatedPostmarkMessage
            {
                To = EmailFrom,
                From = EmailFrom,
                TemplateId = 13593403,
                TemplateModel = new Dictionary<string, object>
                {
                    { "email", email }
                }
            };

            var client = new PostmarkClient("8d8e1a07-e84c-409c-ab38-3e0a14b7407f");
            var sendResult = await client.SendMessageAsync(message);

            if (sendResult.Status == PostmarkStatus.Success)
            {
                Console.WriteLine(sendResult.Status);
            }
            else
            {
                Console.WriteLine(sendResult.Status);
            }
        }
        public async Task SendInvoiceAsync(int Id, string email, string name, string lastname, string dob
            ,string cell, int kids, int adults)
        {
            var stock = dc.Vacations.Where(o => o.Id == Id).FirstOrDefault();
            var resort = dc.Resorts.Where(o => o.Id == stock.ResortId).FirstOrDefault();
            var unitSize = dc.UnitSizes.Where(o => o.Id == stock.UnitSizeId).FirstOrDefault();

            var message = new TemplatedPostmarkMessage
            {
                To = email,
                From = EmailFrom,
                TemplateId = 13370996,
                TemplateModel = new Dictionary<string, object>
                {
                    { "name", name },
                    { "lastname" , lastname},
                    { "dob" , dob},
                    { "cell" , cell },
                    { "kids" , kids },
                    { "adults", adults},
                    { "date", stock.Arrival.Date.ToString("dd/MM/yyyy")},
                    { "resort" , resort.Description},
                    {  "unitSize", unitSize.Description},
                    { "stockId", addInvoice(stock.Id) },
                    { "price2Pay", stock.Price2Pay },
                    { "adminFee", stock.AdminFee },
                    { "total", stock.Price2Pay + stock.AdminFee }
                }
            };

            var client = new PostmarkClient("ba4b7707-54c8-49d7-83d5-e215483fe37c");
            var sendResult = await client.SendMessageAsync(message);

            if (sendResult.Status == PostmarkStatus.Success)
            {
                Console.WriteLine(sendResult.Status);
            }
            else
            {
                Console.WriteLine(sendResult.Status);
            }
        }
        public string addInvoice(int stockId)
        {
            var lastInvoice = dc.Invoices.OrderByDescending(o => o.Id).FirstOrDefault();
            if(lastInvoice == null)
            {
                dc.Invoices.Add(new Invoice()
                {
                    StockId = stockId,
                    InvoiceNumber = "ER6000"
                });
                dc.SaveChanges();
                NewInvoiceGenerated("ER6000", stockId);
                return "ER6000";
            }
            else
            {
                var onlyNumber = Convert.ToInt32(lastInvoice.InvoiceNumber.Substring(2));
                dc.Invoices.Add(new Invoice()
                {
                    StockId = stockId,
                    InvoiceNumber = "ER" + Convert.ToString(onlyNumber + 1)
                });
                dc.SaveChanges();
                NewInvoiceGenerated("ER" + Convert.ToString(onlyNumber + 1), stockId);
                return "ER" + Convert.ToString(onlyNumber + 1);
            }
        }
        public async Task SendNotificationAsync(int Id, string email, string name, string lastname)
        {
            var stock = dc.Vacations.Where(o => o.Id == Id).FirstOrDefault();
            var resort = dc.Resorts.Where(o => o.Id == stock.ResortId).FirstOrDefault();
            var unitSize = dc.UnitSizes.Where(o => o.Id == stock.UnitSizeId).FirstOrDefault();

            var message = new TemplatedPostmarkMessage
            {
                To = email,
                From = EmailFrom,
                TemplateId = 13371666,
                TemplateModel = new Dictionary<string, object>
                {
                    { "name", name },
                    { "lastname" , lastname},
                    { "date", stock.Arrival.Date.ToString("dd/MM/yyyy")},
                    { "resort" , resort.Description},
                    {  "unitSize", unitSize.Description},
                }
            };

            var client = new PostmarkClient("ba4b7707-54c8-49d7-83d5-e215483fe37c");
            var sendResult = await client.SendMessageAsync(message);

            if (sendResult.Status == PostmarkStatus.Success)
            {
                Console.WriteLine(sendResult.Status);
            }
            else
            {
                Console.WriteLine(sendResult.Status);
            }
        }
        public async Task EmailToProviderAsync(EnquiryDto T, Vacation stock, Resort resort, Supplier supplier, UnitSizes unitSize)
        {
            var message = new TemplatedPostmarkMessage
            {
                To = supplier.Email,
                From = EmailFrom,
                TemplateId = 13249489,
                TemplateModel = new Dictionary<string, object>
                {
                    { "provider", supplier.Description },
                    { "resort" , resort.Description},
                    { "arrival", stock.Arrival.Date.ToString("dd/MM/yyyy")},
                    { "nights" , stock.Nights},
                    {  "unitSize", unitSize.Description},
                    { "product_name", "Holiday Booker" },
                    { "company_name" , "Holiday Booker"}
                }
            };

            var client = new PostmarkClient("ba4b7707-54c8-49d7-83d5-e215483fe37c");
            var sendResult = await client.SendMessageAsync(message);

            if (sendResult.Status == PostmarkStatus.Success)
            {
                Console.WriteLine(sendResult.Status);
            }
            else
            {
                Console.WriteLine(sendResult.Status);
            }
        }

        public async Task EmailToClientAsync(EnquiryDto T, Vacation stock, Resort resort, Supplier supplier, UnitSizes unitSize)
        {
            var message = new TemplatedPostmarkMessage
            {
                To = T.Email,
                From = EmailFrom,
                TemplateId = 20752183,
                TemplateModel = new Dictionary<string, object>
                {
                    { "name", T.Name },
                    { "resort" , resort.Description},
                    { "date", stock.Arrival.Date.ToString("dd/MM/yyyy")},
                    { "nights" , stock.Nights},
                    {  "unitsize", unitSize.Description},
                    { "priceToPay", stock.Price2Pay },
                    { "link", resort.Link },
                    { "adults", T.Adults },
                    { "kids", T.Under12 },
                    { "note", T.Note }
                }
            };

            var client = new PostmarkClient("ba4b7707-54c8-49d7-83d5-e215483fe37c");
            var sendResult = await client.SendMessageAsync(message);

            if (sendResult.Status == PostmarkStatus.Success)
            {
                Console.WriteLine(sendResult.Status);
            }
            else
            {
                Console.WriteLine(sendResult.Status);
            }
        }
        public async Task EmailToAdminAsync(EnquiryDto T, Vacation stock, Resort resort, Supplier supplier, UnitSizes unitSize)
        {
            //var temp = dc.Invoices.Where(o => o.StockId == stock.Id).FirstOrDefault();
            //string invoiceNumber = "";
            //if (temp == null)
            //{
            //    invoiceNumber = "System Lost invoice Number. (Stock Id = " + Convert.ToString(stock.Id) + ")";
            //}
            //else
            //{
            //    invoiceNumber = temp.InvoiceNumber;
            //}
             
            var message = new TemplatedPostmarkMessage
            {
               // To = "ernst.blignaut0@gmail.com",
                //To = "elke@holidaybooker.co.za",
                To = "info@holidaybooker.co.za",
                From = EmailFrom,
                TemplateId = 20661200,
                TemplateModel = new Dictionary<string, object>
                {
                    { "name", T.Name },
                    { "lastname", T.Lastname },
                    { "resort" , resort.Description},
                    { "date", stock.Arrival.Date.ToString("dd/MM/yyyy")},
                    {  "unitSize", unitSize.Description},
                    { "priceToPay", stock.Price2Pay },
                    { "dob", T.Dob },
                    { "email", T.Email },
                    { "cell", T.Cell },
                    { "id" ,  stock.Id},
                    { "adults", T.Adults },
                    { "kids", T.Under12 },
                    { "note", T.Note }
                }
            };

            var client = new PostmarkClient("ba4b7707-54c8-49d7-83d5-e215483fe37c");
            var sendResult = await client.SendMessageAsync(message);

            if (sendResult.Status == PostmarkStatus.Success)
            {
                Console.WriteLine(sendResult.Status);
            }
            else
            {
                Console.WriteLine(sendResult.Status);
            }
        }

        //New Invoice Generated
        public async Task NewInvoiceGenerated(string invoiceNum, int stockId)
        {
            var message = new TemplatedPostmarkMessage
            {
                To = "info@holidaybooker.co.za",
                From = EmailFrom,
                TemplateId = 16198462,
                TemplateModel = new Dictionary<string, object>
                {
                    { "invoiceNum", invoiceNum },
                    { "stockId" , stockId},
                }
            };

            var client = new PostmarkClient("ba4b7707-54c8-49d7-83d5-e215483fe37c");
            var sendResult = await client.SendMessageAsync(message);

            if (sendResult.Status == PostmarkStatus.Success)
            {
                Console.WriteLine(sendResult.Status);
            }
            else
            {
                Console.WriteLine(sendResult.Status);
            }
        }
    }
}
