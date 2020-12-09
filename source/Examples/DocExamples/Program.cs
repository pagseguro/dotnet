// Copyright [2011] [PagSeguro Internet Ltda.]
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

using System;
using System.Web;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Service;
using Uol.PagSeguro.Resources;

namespace DocExamples
{
    class Program
    {
        static void SenderExample()
        {
            PaymentRequest paymentRequest = new PaymentRequest();

            // Sender representa quem enviará dinheiro
            // na transação, normalmente o comprador
            Sender sender =
                new Sender(
                    "José Comprador",       // Nome 
                    "comprador@uol.com.br", // Email
                    new Phone("11", "56273440") // Telefone
                    );

            paymentRequest.Sender = sender;

            if (paymentRequest.Sender != null)
            {
                if (paymentRequest.Sender.Phone != null)
                {
                }
            }

        }

        static void PhoneExample()
        {
            Phone phone = new Phone("11", "56273440");
        }

        static void AddItemExample()
        {
            PaymentRequest paymentRequest = new PaymentRequest();
            paymentRequest.Items.Add(new Item("0001", "Notebook", 1, 2430.00m));
        }

        static void ShippingTypeExample()
        {
            PaymentRequest paymentRequest = new PaymentRequest();
            paymentRequest.Shipping = new Shipping();
            paymentRequest.Shipping.ShippingType = ShippingType.Pac;
        }

        static void ShippingAddressExample()
        {
            PaymentRequest paymentRequest = new PaymentRequest();
            paymentRequest.Shipping = new Shipping();
            paymentRequest.Shipping.Address = new Address(
                "BRA",
                "SP",
                "São Paulo",
                "Jardim Paulistano",
                "01452002",
                "Av. Brig. Faria Lima",
                "1384",
                "5o. Andar");
        }

        static void ExtraAmountExample()
        {
            PaymentRequest paymentRequest = new PaymentRequest();
            paymentRequest.ExtraAmount = 15.79m;
        }

        static void ReferenceExample()
        {
            PaymentRequest paymentRequest = new PaymentRequest();
            paymentRequest.Reference = "REF1234";
        }

        static void RedirectUriExample()
        {
            PaymentRequest paymentRequest = new PaymentRequest();
            paymentRequest.RedirectUri = new Uri("http://lojamodelo.com.br/conclusao.html");
        }

        static void MaxAgeUsesExample()
        {
            PaymentRequest paymentRequest = new PaymentRequest();
            paymentRequest.MaxAge = 2880; // 2 dias  
            paymentRequest.MaxUses = 15; // 15 vezes
        }

        static void RegisterExample()
        {
            PaymentRequest paymentRequest = new PaymentRequest();
            // Preencher propriedades da requisição do pagamento aqui

            bool isSandbox = false;

            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            AccountCredentials credentials = PagSeguroConfiguration.GetAccountCredentials(isSandbox);

            // Criando o código de requisição de pagamento
            // e obtendo a URL da página de pagamento
            // do PagSeguro
            Uri paymentRedirectUri = paymentRequest.Register(credentials);

            //Response.Redirect(paymentRedirectUri.ToString());
        }

        static void RequestExample(HttpRequest Request)
        {
            bool isSandbox = false;

            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            AccountCredentials credentials = PagSeguroConfiguration.GetAccountCredentials(isSandbox);

            string notificationType = Request.Form["notificationType"];
            string notificationCode = Request.Form["notificationCode"];

            if (notificationType == "transaction")
            {
                // obtendo o objeto transaction a partir do código de notificação
                Transaction transaction =
                    NotificationService.CheckTransaction(credentials, notificationCode);

                int status = transaction.TransactionStatus;
            }
        }

        static void SearchByCodeExample()
        {
            bool isSandbox = false;

            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            AccountCredentials credentials = PagSeguroConfiguration.GetAccountCredentials(isSandbox);

            string transactionCode = "59A13D84-52DA-4AB8-B365-1E7D893052B0";

            Transaction transaction =
                TransactionSearchService.SearchByCode(credentials, transactionCode);


        }

        static void PaymentMethodExample(Transaction transaction)
        {
            PaymentMethod paymentMethod = transaction.PaymentMethod;
            int code = transaction.PaymentMethod.PaymentMethodCode;
            int type = transaction.PaymentMethod.PaymentMethodType;
        }

        static void AddressExample()
        {
            Address address =
                new Address(
                    "BRA",
                    "SP",
                    "São Paulo",
                    "Jardim Paulistano",
                    "01452002",
                    "Av. Brig. Faria Lima",
                    "1384",
                    "5o. Andar");
        }

        static void Addres2sExample()
        {
            Address address = new Address();
            address.Country = "BRA";
            address.State = "SP";
            address.City = "São Paulo";
            address.District = "Jardim Paulistano";
            address.PostalCode = "01452002";
            address.Street = "Av. Brig. Faria Lima";
            address.Number = "1384";
            address.Complement = "5o. Andar";
        }

        static void ShippingExample()
        {
            PaymentRequest paymentRequest = new PaymentRequest();

            Shipping shipping = new Shipping();
            shipping.ShippingType = ShippingType.Pac;
            shipping.Address =
                new Address(
                    "BRA",
                    "SP",
                    "São Paulo",
                    "Jardim Paulistano",
                    "01452002",
                    "Av. Brig. Faria Lima",
                    "1384",
                    "5o. Andar");

            paymentRequest.Shipping = shipping;

        }

        static void Shipping2Example(Transaction transaction)
        {
            if (transaction.Shipping != null)
            {

                if (transaction.Shipping.Address != null)
                {

                }
            }
        }

        static void ItemExample(PaymentRequest paymentRequest)
        {
            Item item =
                new Item(
                    "0001",     // Identificador do item em seu site ou aplicação
                    "Notebook", // Descrição
                    1,          // Quantidade
                    2430.00m,   // Valor
                    1000,       // Peso em gramas
                    17.35m);    // Valor do frete

            paymentRequest.Items.Add(item);
        }

        static void ItemsExample(PaymentRequest paymentRequest)
        {
            foreach (Item item in paymentRequest.Items)
            {

            }
        }

        static void SearchByCodeExample2()
        {
            bool isSandbox = false;

            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            AccountCredentials credentials = PagSeguroConfiguration.GetAccountCredentials(isSandbox);

            // Código identificador da transação  
            string transactionCode = "59A13D84-52DA-4AB8-B365-1E7D893052B0";

            // Realizando uma consulta de transação a partir do código identificador 
            // para obter o objeto Transaction
            Transaction transaction =
                TransactionSearchService.SearchByCode(
                    credentials,
                    transactionCode);

            // Imprime o status da transação
      
        }

        static void SearchByDateExample()
        {
            bool isSandbox = false;

            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            AccountCredentials credentials = PagSeguroConfiguration.GetAccountCredentials(isSandbox);

            // Definindo a data de ínicio da consulta 
            DateTime initialDate = new DateTime(2011, 06, 1, 08, 50, 0);

            // Definindo a data de término da consulta
            DateTime finalDate = new DateTime(2011, 06, 29, 10, 30, 0);

            // Definindo o número máximo de resultados por página
            int maxPageResults = 20;

            // Definindo o número da página
            int pageNumber = 1;

            // Realizando a consulta
            TransactionSearchResult result =
                TransactionSearchService.SearchByDate(
                    credentials,
                    initialDate,
                    finalDate,
                    pageNumber,
                    maxPageResults);
        }

        static void TransactionSearchResultExample(TransactionSearchResult transactionSearchResult)
        {
            // Obtendo a data da realização da consulta  
            DateTime date = transactionSearchResult.Date;

            // Obtendo a quantidade de resultados na página 
            int resultsInThisPage = transactionSearchResult.Transactions.Count;

            // Obtendo a quantidade total de páginas 
            int totalPages = transactionSearchResult.TotalPages;

            // Obtendo o número da página consultada 
            int currentPage = transactionSearchResult.CurrentPage;

            // Iterando na lista de transações
            foreach (TransactionSummary transaction in transactionSearchResult.Transactions)
            {
                // Código da transação  
                string code = transaction.Code;
                // Status da transação 
                int status = transaction.TransactionStatus;
                // Refência da transação 
                string reference = transaction.Reference;
                // Valor bruto da transação 
                decimal amount = transaction.GrossAmount;
            }
        }

        static void TransactionSummaryExample(TransactionSummary transactionSummary)
        {
            // Data da criação 
            DateTime date = transactionSummary.Date;

            // Data da última atualização 
            DateTime lastEventDate = transactionSummary.LastEventDate;

            // Código da transação 
            string code = transactionSummary.Code;

            // Refência 
            string reference = transactionSummary.Reference;

            // Valor bruto  
            decimal grossAmount = transactionSummary.GrossAmount;

            // Tipo 
            int type = transactionSummary.TransactionType;

            // Status 
            int status = transactionSummary.TransactionStatus;

            // Valor líquido  
            decimal netAmount = transactionSummary.NetAmount;

            // Valor das taxas cobradas  
            decimal feeAmount = transactionSummary.FeeAmount;

            // Valor extra ou desconto
            decimal extraAmount = transactionSummary.ExtraAmount;

            // Tipo de meio de pagamento
            PaymentMethod paymentMethod = transactionSummary.PaymentMethod;
        }
        static void Main(string[] args)
        {

        }
    }
}
