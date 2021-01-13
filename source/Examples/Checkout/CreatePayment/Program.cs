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
using System.Collections.Generic;
using System.Net;
using Uol.PagSeguro;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Resources;

namespace CreatePayment
{
    class Program
    {
        static void Main(string[] args)
        {

            //Use global configuration
            //PagSeguroConfiguration.UrlXmlConfiguration = "../../../../../Configuration/PagSeguroConfig.xml";

            bool isSandbox = false;
            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            // Instantiate a new payment request
            PaymentRequest payment = new PaymentRequest();

            // Sets the currency
            payment.Currency = Currency.Brl;

            // Add an item for this payment request
            payment.Items.Add(new Item("0001", "Notebook Prata", 1, 2430.00m));


            // Add another item for this payment request
            payment.Items.Add(new Item("0002", "Notebook Rosa", 2, 150.99m));

            // Sets a reference code for this payment request, it is useful to identify this payment in future notifications.
            payment.Reference = "REF1234";

            // Sets shipping information for this payment request
            payment.Shipping = new Shipping();
            payment.Shipping.ShippingType = ShippingType.Sedex;

            //Passando valor para ShippingCost
            payment.Shipping.Cost = 10.00m;

            payment.Shipping.Address = new Address(
                "BRA",
                "SP",
                "Sao Paulo",
                "Jardim Paulistano",
                "01452002",
                "Av. Brig. Faria Lima",
                "1384",
                "5o andar"
            );

            // Sets your customer information.
            payment.Sender = new Sender(
                "Joao Comprador",
                "comprador@uol.com.br",
                new Phone("11", "56273440")
            );

            SenderDocument document = new SenderDocument(Documents.GetDocumentByType("CPF"), "12345678909");
            payment.Sender.Documents.Add(document);

            // Sets the url used by PagSeguro for redirect user after ends checkout process
            payment.RedirectUri = new Uri("http://www.lojamodelo.com.br");

            // Add checkout metadata information
            payment.AddMetaData(MetaDataItemKeys.GetItemKeyByDescription("CPF do passageiro"), "123.456.789-09", 1);
            payment.AddMetaData("PASSENGER_PASSPORT", "23456", 1);

            // Another way to set checkout parameters
            payment.AddParameter("senderBirthday", "07/05/1980");
            payment.AddIndexedParameter("itemColor", "verde", 1);
            payment.AddIndexedParameter("itemId", "0003", 3);
            payment.AddIndexedParameter("itemDescription", "Mouse", 3);
            payment.AddIndexedParameter("itemQuantity", "1", 3);
            payment.AddIndexedParameter("itemAmount", "200.00", 3);

            // Add discount per payment method
            payment.AddPaymentMethodConfig(PaymentMethodConfigKeys.DiscountPercent, 50.00, PaymentMethodGroup.CreditCard);

            // Add installment without addition per payment method
            payment.AddPaymentMethodConfig(PaymentMethodConfigKeys.MaxInstallmentsNoInterest, 6, PaymentMethodGroup.CreditCard);

            // Add installment limit per payment method
            payment.AddPaymentMethodConfig(PaymentMethodConfigKeys.MaxInstallmentsLimit, 8, PaymentMethodGroup.CreditCard);

            // Add and remove groups and payment methods
            List<string> accept = new List<string>();
            accept.Add(ListPaymentMethodNames.DebitoItau);
            accept.Add(ListPaymentMethodNames.DebitoHSBC);
            payment.AcceptPaymentMethodConfig(ListPaymentMethodGroups.CreditCard, accept);

            List<string> exclude = new List<string>();
            exclude.Add(ListPaymentMethodNames.Boleto);
            payment.ExcludePaymentMethodConfig(ListPaymentMethodGroups.Boleto, exclude);

            try
            {
                /// Create new account credentials
                /// This configuration let you set your credentials from your ".cs" file.
                AccountCredentials credentials = new AccountCredentials("backoffice@lojamodelo.com.br", "256422BF9E66458CA3FE41189AD1C94A");

                /// @todo with you want to get credentials from xml config file uncomment the line below and comment the line above.
                //AccountCredentials credentials = PagSeguroConfiguration.Credentials(isSandbox);

                Uri paymentRedirectUri = payment.Register(credentials);


            }
            catch (PagSeguroServiceException exception)
            {


                foreach (ServiceError element in exception.Errors)
                {

                }

            }
        }
    }
}
