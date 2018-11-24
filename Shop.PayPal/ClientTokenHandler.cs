using Braintree;
using Microsoft.AspNetCore.Http;
using System;

namespace Shop.PayPal
{
    
    public class ClientTokenHandler
    {
        public const string AccessToken = "access_token$sandbox$zc7tphw8z3sf9fj2$abf54faaf986dcc9f81410291219c636";
        public const string Token = "sandbox_qzg8kjkq_wfnsgbhdn9wr8mmb";

        public int MyProperty { get; set; } = 228;

        BraintreeGateway /*gateway = new BraintreeGateway(Token);*/
            gateway = new BraintreeGateway
            {
                //Environment = Braintree.Environment.SANDBOX,
                //MerchantId = "wfnsgbhdn9wr8mmb",
                //PublicKey = "hvzg64tm86rwy5qj",
                //PrivateKey = "fe6ea02b34261edfee2391c310095435",
                //ClientId= "AfiR9nRAINIge3oLebYlir59tQdlCcGXFk0yWusDzlKM3mFwEgfAxX_2eCMU0eL6XHRyzxL8TS7GggKN",
                //ClientSecret = "EJZmMJ9v6mvyZROxqfMs6Q_8s_7SgJwvGR-hDHADAUz6BWPvmBDQ2dAZZKBo_-YO16o7bqtU2w0p2G32",
                AccessToken = AccessToken
            };
        public void ProcessRequest(HttpContext context)
        {
            var clientToken = gateway.ClientToken.Generate();
            context.Response.WriteAsync(clientToken);
        }

        public string GetServerToken()
        {
            return gateway.ClientToken.Generate();
        }

        public void Pay(string nonce, double price)
        {
            BraintreeGateway gateway = new BraintreeGateway(AccessToken);            

            TransactionRequest request = new TransactionRequest
            {
                Amount = (decimal)price,
                MerchantAccountId = "USD",
                PaymentMethodNonce = nonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
                //Amount = 1000.0M,
                //MerchantAccountId = "USD",
                //PaymentMethodNonce = nonce,
                ////OrderId = "Mapped to PayPal Invoice Number",
                //Descriptor = new DescriptorRequest
                //{
                //    Name = "QQQ"                    
                //},
                //ShippingAddress = new AddressRequest
                //{
                //    FirstName = "Jen",
                //    LastName = "Smith",
                //    Company = "Bra",
                //    StreetAddress = "1 E 1st St",
                //    ExtendedAddress = "Suite 403",
                //    Locality = "Bartlett",
                //    Region = "IL",
                //    PostalCode = "60103",
                //    CountryCodeAlpha2 = "US"
                //},
                //Options = new TransactionOptionsRequest
                //{
                //    PayPal = new TransactionOptionsPayPalRequest
                //    {
                //        CustomField = "PayPal",
                //        Description = "Description",

                //    },
                //    SubmitForSettlement = true
                //}
            };

            Result<Transaction> result = gateway.Transaction.Sale(request);
            string id = null;
            if (result.Errors.Count == 0)
            {
                System.Console.WriteLine("Transaction ID: " + result.Transaction.Id);
                id = result.Transaction.Id;
            }
            else
            {
                System.Console.WriteLine(result.Message);
            }

            //Transaction transaction = gateway.Transaction.Find("8UT535263C985361U");

            //var x = transaction.PayPalDetails.SellerProtectionStatus;
        }
    }
}
