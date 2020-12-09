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

using System.Collections.Generic;
using Uol.PagSeguro.Util;
using Uol.PagSeguro.Domain.Direct;
using System.Linq;
using System;

namespace Uol.PagSeguro.Parse
{
    /// <summary>
    /// 
    /// </summary>
    internal static class TransactionParse
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="checkout"></param>
        /// <returns></returns>
        public static IDictionary<string, string> GetData(Checkout checkout)
        {
            IDictionary<string, string> data = new Dictionary<string, string>();

            // payment mode
            if (!string.IsNullOrEmpty(checkout.PaymentMode))
                data["paymentMode"] = checkout.PaymentMode;

            // receiver e-mail
            if (!string.IsNullOrEmpty(checkout.ReceiverEmail))
                data["receiverEmail"] = checkout.ReceiverEmail;
   
            // reference
            if (!string.IsNullOrEmpty(checkout.Reference))
                data["reference"] = checkout.Reference;

            // sender
            if (checkout.Sender != null)
            {
                if (!string.IsNullOrEmpty(checkout.Sender.Name))
                    data["senderName"] = checkout.Sender.Name;

                if (!string.IsNullOrEmpty(checkout.Sender.Email))
                    data["senderEmail"] = checkout.Sender.Email;

                if (!string.IsNullOrEmpty(checkout.Sender.Hash))
                    data["senderHash"] = checkout.Sender.Hash;

                // phone
                if (checkout.Sender.Phone != null)
                {
                    if (!string.IsNullOrEmpty(checkout.Sender.Phone.AreaCode))
                        data["senderAreaCode"] = checkout.Sender.Phone.AreaCode;

                    if (!string.IsNullOrEmpty(checkout.Sender.Phone.Number))
                        data["senderPhone"] = checkout.Sender.Phone.Number;
                }

                // documents
                if (checkout.Sender.Documents != null && checkout.Sender.Documents.Any())
                {
                    var documents = checkout.Sender.Documents;

                    if (documents.Count == 1)
                    {
                        foreach (var document in documents)
                        {
                            if (document != null)
                            {
                                if (document.Type.Equals("Cadastro de Pessoa Física"))
                                    data["senderCPF"] = document.Value;
                                else
                                    data["senderCNPJ"] = document.Value;
                            }
                        }
                    }
                }
            }

            // currency
            if (!string.IsNullOrEmpty(checkout.Currency))
                data["currency"] = checkout.Currency;

            // items
            if (checkout.Items.Count > 0)
            {
                var i = 0;
                var items = checkout.Items;
                foreach (var item in items)
                {
                    i++;

                    if (!string.IsNullOrEmpty(item.Id))
                        data["itemId" + i] = item.Id;

                    if (!string.IsNullOrEmpty(item.Description))
                        data["itemDescription" + i] = item.Description;

                    data["itemQuantity" + i] = item.Quantity.ToString();
                    data["itemAmount" + i] = PagSeguroUtil.DecimalFormat(item.Amount);

                    if (item.Weight != null)
                        data["itemWeight" + i] = item.Weight.ToString();

                    if (item.ShippingCost != null)
                        data["itemShippingCost" + i] = PagSeguroUtil.DecimalFormat((decimal)item.ShippingCost);
                }
            }

            // extraAmount
            if (checkout.ExtraAmount != null)
                data["extraAmount"] = PagSeguroUtil.DecimalFormat((decimal)checkout.ExtraAmount);

            // shipping
            if (checkout.Shipping != null)
            {
                if (checkout.Shipping.ShippingType.HasValue)
                    data["shippingType"] = checkout.Shipping.ShippingType.Value.ToString();

                if (checkout.Shipping.Cost.HasValue)
                    data["shippingCost"] = PagSeguroUtil.DecimalFormat(checkout.Shipping.Cost.Value);

                // address
                if (checkout.Shipping.Address != null)
                {
                    if (!string.IsNullOrEmpty(checkout.Shipping.Address.Street))
                        data["shippingAddressStreet"] = checkout.Shipping.Address.Street;

                    if (!string.IsNullOrEmpty(checkout.Shipping.Address.Number))
                        data["shippingAddressNumber"] = checkout.Shipping.Address.Number;

                    if (!string.IsNullOrEmpty(checkout.Shipping.Address.Complement))
                        data["shippingAddressComplement"] = checkout.Shipping.Address.Complement;

                    if (!string.IsNullOrEmpty(checkout.Shipping.Address.City))
                        data["shippingAddressCity"] = checkout.Shipping.Address.City;

                    if (!string.IsNullOrEmpty(checkout.Shipping.Address.State))
                        data["shippingAddressState"] = checkout.Shipping.Address.State;

                    if (!string.IsNullOrEmpty(checkout.Shipping.Address.District))
                        data["shippingAddressDistrict"] = checkout.Shipping.Address.District;

                    if (!string.IsNullOrEmpty(checkout.Shipping.Address.PostalCode))
                        data["shippingAddressPostalCode"] = checkout.Shipping.Address.PostalCode;

                    if (!string.IsNullOrEmpty(checkout.Shipping.Address.Country))
                        data["shippingAddressCountry"] = checkout.Shipping.Address.Country;
                }
            }

            // maxAge
            if (checkout.MaxAge != null)
                data["maxAge"] = checkout.MaxAge.ToString();

            // maxUses
            if (checkout.MaxUses != null)
                data["maxUses"] = checkout.MaxUses.ToString();

            // notificationURL
            if (!string.IsNullOrEmpty(checkout.NotificationUrl))
                data["notificationURL"] = checkout.NotificationUrl;

            // metadata
            if (checkout.MetaData.Items.Count > 0)
            {
                var i = 0;
                var metaDataItems = checkout.MetaData.Items;
                foreach (var item in metaDataItems)
                {
                    if (PagSeguroUtil.IsEmpty(item.Key) || PagSeguroUtil.IsEmpty(item.Value))
                        continue;

                    i++;
                    data["metadataItemKey" + i] = item.Key;
                    data["metadataItemValue" + i] = item.Value;

                    if (item.Group != null)
                        data["metadataItemGroup" + i] = item.Group.ToString();
                }
            }

            // parameter
            if (checkout.Parameter.Items.Count > 0)
            {
                var parameterItems = checkout.Parameter.Items;
                foreach (var item in parameterItems)
                {
                    if (PagSeguroUtil.IsEmpty(item.Key) || PagSeguroUtil.IsEmpty(item.Value))
                        continue;

                    {   if (item.Group != null)
                            data[item.Key + "" + item.Group] = item.Value;
                        else
                            data[item.Key] = item.Value;
                    }
                }
            }
            switch (checkout)
            {
                //Verify if exists the credit card checkout data
                case CreditCardCheckout creditcard:
                    // billing address
                    if (creditcard.Billing?.Address != null)
                    {
                        if (!string.IsNullOrEmpty(creditcard.Billing.Address.Street))
                            data["billingAddressStreet"] = creditcard.Billing.Address.Street;

                        if (!string.IsNullOrEmpty(creditcard.Billing.Address.Number))
                            data["billingAddressNumber"] = creditcard.Billing.Address.Number;

                        if (!string.IsNullOrEmpty(creditcard.Billing.Address.Complement))
                            data["billingAddressComplement"] = creditcard.Billing.Address.Complement;

                        if (!string.IsNullOrEmpty(creditcard.Billing.Address.City))
                            data["billingAddressCity"] = creditcard.Billing.Address.City;

                        if (!string.IsNullOrEmpty(creditcard.Billing.Address.State))
                            data["billingAddressState"] = creditcard.Billing.Address.State;

                        if (!string.IsNullOrEmpty(creditcard.Billing.Address.District))
                            data["billingAddressDistrict"] = creditcard.Billing.Address.District;

                        if (!string.IsNullOrEmpty(creditcard.Billing.Address.PostalCode))
                            data["billingAddressPostalCode"] = creditcard.Billing.Address.PostalCode;

                        if (!string.IsNullOrEmpty(creditcard.Billing.Address.Country))
                            data["billingAddressCountry"] = creditcard.Billing.Address.Country;
                             
                    }

                    // holder
                    if (creditcard.Holder != null)
                    {
                        //holder name
                        if (!string.IsNullOrEmpty(creditcard.Holder.Name))
                            data["creditCardHolderName"] = creditcard.Holder.Name;

                        //holder phone
                        if (creditcard.Holder.Phone != null)
                        {
                            if (!string.IsNullOrEmpty(creditcard.Holder.Phone.AreaCode))
                                data["creditCardHolderAreaCode"] = creditcard.Holder.Phone.AreaCode;

                            if (!string.IsNullOrEmpty(creditcard.Holder.Phone.Number))
                                data["creditCardHolderPhone"] = creditcard.Holder.Phone.Number;
                        }

                        //holder document
                        if (creditcard.Holder.Document?.Value != null)
                            data["creditCardHolderCPF"] = creditcard.Holder.Document.Value;

                        //holder birth date
                        if (!string.IsNullOrEmpty(creditcard.Holder.Birthdate))
                            data["creditCardHolderBirthDate"] = creditcard.Holder.Birthdate;
                    }

                    // token
                    if (!string.IsNullOrEmpty(creditcard.Token))
                        data["creditCardToken"] = creditcard.Token;

                    // installment
                    if (creditcard.Installment != null)
                    {
                        if (creditcard.Installment.Quantity > 0)
                            data["installmentQuantity"] = creditcard.Installment.Quantity.ToString();

                        if (creditcard.Installment.Value > 0)
                            data["installmentValue"] = PagSeguroUtil.DecimalFormat(creditcard.Installment.Value);

                        if (creditcard.Installment.NoInterestInstallmentQuantity > 0)
                            data["noInterestInstallmentQuantity"] = creditcard.Installment.NoInterestInstallmentQuantity.ToString();
                    }

                    // payment method
                    if (!string.IsNullOrEmpty(creditcard.PaymentMethod))
                        data["paymentMethod"] = creditcard.PaymentMethod;

                    break;

                default:
                throw new InvalidOperationException("Unexpected value");    

                //Verify if exists the boleto checkout data
                case BoletoCheckout _:
                    var boleto = (BoletoCheckout)checkout;

                    // payment method
                    if (!string.IsNullOrEmpty(boleto.PaymentMethod))
                        data["paymentMethod"] = boleto.PaymentMethod;

                    break;

                //Verify if exists the online debit checkout data
                case OnlineDebitCheckout _:
                    var onlineDebit = (OnlineDebitCheckout)checkout;

                    // payment method
                    if (!string.IsNullOrEmpty(onlineDebit.PaymentMethod))
                        data["paymentMethod"] = onlineDebit.PaymentMethod;

                    // bank name
                    if (!string.IsNullOrEmpty(onlineDebit.BankName))
                        data["bankName"] = onlineDebit.BankName;

                    break;
            }

            return data;
        }
    }
}
