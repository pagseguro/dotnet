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
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Util;
using Uol.PagSeguro.Constants.PreApproval;
using Uol.PagSeguro.Constants;
using System.Linq;

namespace Uol.PagSeguro.Parse
{
    /// <summary>
    /// 
    /// </summary>
    internal static class PaymentParse
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        public static IDictionary<string, string> GetData(PaymentRequest payment)
        {
            IDictionary<string, string> data = new Dictionary<string, string>();

            // reference
            if (!string.IsNullOrEmpty(payment.Reference))
                data["reference"] = payment.Reference;

            // sender
            if (payment.Sender != null)
            {
                if (!string.IsNullOrEmpty(payment.Sender.Name))
                    data["senderName"] = payment.Sender.Name;

                if (!string.IsNullOrEmpty(payment.Sender.Email))
                    data["senderEmail"] = payment.Sender.Email;

                // phone
                if (payment.Sender.Phone != null)
                {
                    if (!string.IsNullOrEmpty(payment.Sender.Phone.AreaCode))
                        data["senderAreaCode"] = payment.Sender.Phone.AreaCode;

                    if (!string.IsNullOrEmpty(payment.Sender.Phone.Number))
                        data["senderPhone"] = payment.Sender.Phone.Number;
                }

                // documents
                if (payment.Sender.Documents != null && payment.Sender.Documents.Any())
                {
                    var documents = payment.Sender.Documents;
                    if (documents.Count == 1)
                    {
                        foreach (var document in documents)
                        {
                            if (document == null)
                                continue;

                            if (document.Type.Equals("Cadastro de Pessoa Física"))
                                data["senderCPF"] = document.Value;
                            else
                                data["senderCNPJ"] = document.Value;
                        }
                    }
                }
            }

            // currency
            if (!string.IsNullOrEmpty(payment.Currency))
                data["currency"] = payment.Currency;

            // items
            if (payment.Items.Count > 0)
            {
                var i = 0;
                var items = payment.Items;
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

                    if (item.ShippingCost.HasValue)
                        data["itemShippingCost" + i] = PagSeguroUtil.DecimalFormat(item.ShippingCost.Value);
                }
            }

            //preApproval
            if (payment.PreApproval != null)
            {
                data["preApprovalCharge"] = payment.PreApproval.Charge;
                data["preApprovalName"] = payment.PreApproval.Name;
                data["preApprovalDetails"] = payment.PreApproval.Details;
                data["preApprovalPeriod"] = payment.PreApproval.Period;
                data["preApprovalFinalDate"] = payment.PreApproval.FinalDate.ToString("yyyy-MM-dd") + "T01:00:00.45-03:00";
                data["preApprovalMaxTotalAmount"] = payment.PreApproval.MaxTotalAmount.ToString("F").Replace(",", ".");
                data["preApprovalAmountPerPayment"] = payment.PreApproval.AmountPerPayment.ToString("F").Replace(",", ".");

                if (payment.PreApproval.Charge == Charge.Manual)
                {
                    data["preApprovalInitialDate"] = payment.PreApproval.InitialDate.ToString("yyyy-MM-dd") + "T01:00:00.45-03:00";
                    data["preApprovalMaxAmountPerPeriod"] = payment.PreApproval.MaxAmountPerPeriod.ToString("F").Replace(",", ".");
                    data["preApprovalMaxPaymentsPerPeriod"] = payment.PreApproval.MaxPaymentsPerPeriod.ToString();

                    if (payment.PreApproval.Period == Period.Yearly)
                        data["preApprovalDayOfYear"] = payment.PreApproval.DayOfYear.ToString();

                    if (payment.PreApproval.Period == Period.Monthly ||
                        payment.PreApproval.Period == Period.Bimonthly ||
                        payment.PreApproval.Period == Period.Trimonthly ||
                        payment.PreApproval.Period == Period.SemiAnnually)
                        data["preApprovalDayOfMonth"] = payment.PreApproval.DayOfMonth.ToString();

                    if (payment.PreApproval.Period == Period.Weekly)
                        data["preApprovalDayOfWeek"] = payment.PreApproval.DayOfWeek.ToString();
                }

                data["reviewUrl"] = payment.ReviewUri.ToString();
            }

            //preApproval payment
            if (!string.IsNullOrEmpty(payment.PreApprovalCode))
                data["preApprovalCode"] = payment.PreApprovalCode;

            // extraAmount
            if (payment.ExtraAmount.HasValue)
                data["extraAmount"] = PagSeguroUtil.DecimalFormat(payment.ExtraAmount.Value);

            // shipping
            if (payment.Shipping != null)
            {
                if (payment.Shipping.ShippingType.HasValue)
                    data["shippingType"] = payment.Shipping.ShippingType.Value.ToString();

                if (payment.Shipping.Cost.HasValue)
                    data["shippingCost"] = PagSeguroUtil.DecimalFormat(payment.Shipping.Cost.Value);

                // address
                if (payment.Shipping.Address != null)
                {
                    if (!string.IsNullOrEmpty(payment.Shipping.Address.Street))
                        data["shippingAddressStreet"] = payment.Shipping.Address.Street;

                    if (!string.IsNullOrEmpty(payment.Shipping.Address.Number))
                        data["shippingAddressNumber"] = payment.Shipping.Address.Number;

                    if (!string.IsNullOrEmpty(payment.Shipping.Address.Complement))
                        data["shippingAddressComplement"] = payment.Shipping.Address.Complement;

                    if (!string.IsNullOrEmpty(payment.Shipping.Address.City))
                        data["shippingAddressCity"] = payment.Shipping.Address.City;

                    if (!string.IsNullOrEmpty(payment.Shipping.Address.State))
                        data["shippingAddressState"] = payment.Shipping.Address.State;

                    if (!string.IsNullOrEmpty(payment.Shipping.Address.District))
                        data["shippingAddressDistrict"] = payment.Shipping.Address.District;

                    if (!string.IsNullOrEmpty(payment.Shipping.Address.PostalCode))
                        data["shippingAddressPostalCode"] = payment.Shipping.Address.PostalCode;

                    if (!string.IsNullOrEmpty(payment.Shipping.Address.Country))
                        data["shippingAddressCountry"] = payment.Shipping.Address.Country;
                }
            }

            // maxAge
            if (payment.MaxAge != null)
                data["maxAge"] = payment.MaxAge.ToString();

            // maxUses
            if (payment.MaxUses != null)
                data["maxUses"] = payment.MaxUses.ToString();

            // redirectURL
            if (payment.RedirectUri != null)
                data["redirectURL"] = payment.RedirectUri.ToString();

            // notificationURL
            if (!string.IsNullOrEmpty(payment.NotificationUrl))
                data["notificationURL"] = payment.NotificationUrl;

            // metadata
            if (payment.MetaData.Items.Count > 0)
            {
                var i = 0;
                var metaDataItems = payment.MetaData.Items;
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
            if (payment.Parameter.Items.Count > 0)
            {
                var parameterItems = payment.Parameter.Items;
                foreach (var item in parameterItems)
                {
                    if (PagSeguroUtil.IsEmpty(item.Key) || PagSeguroUtil.IsEmpty(item.Value))
                        continue;

                    if (item.Group != null)
                        data[item.Key + "" + item.Group] = item.Value;
                    else
                        data[item.Key] = item.Value;
                }
            }

            // paymentMethodConfig 
            if (payment.PaymentMethodConfig.Items.Count > 0)
            {
                var i = 0;
                var configItems = payment.PaymentMethodConfig.Items;
                foreach (var item in configItems)
                {
                    if (PagSeguroUtil.IsEmpty(item.Key) || PagSeguroUtil.IsEmpty(item.Group))
                        continue;

                    i++;
                    data["paymentMethodGroup" + i] = item.Group;
                    data["paymentMethodConfigKey" + i + "_1"] = item.Key;

                    if (item.Key.Equals(PaymentMethodConfigKeys.DiscountPercent))
                        data["paymentMethodConfigValue" + i + "_1"] = PagSeguroUtil.DecimalFormat(item.Value);
                    else
                        data["paymentMethodConfigValue" + i + "_1"] = PagSeguroUtil.DoubleToInt(item.Value);
                }
            }

            // paymentMethodConfig 
            if (payment.AcceptedPaymentMethods.Items.Count <= 0)
                return data;

            var acceptGroupList = new List<string>();
            var acceptNameList = new List<string>();
            var excludeGroupList = new List<string>();
            var excludeNameList = new List<string>();
            var config = payment.AcceptedPaymentMethods.Items;

            foreach (var item in config)
            {
                if (item.GetType() == typeof(AcceptPaymentMethod))  
                {
                    if (!acceptGroupList.Contains(item.Group))
                        acceptGroupList.Add(item.Group);

                    acceptNameList = item.Name;
                }

                if (item.GetType() == typeof(ExcludePaymentMethod))
                {
                    if (!excludeGroupList.Contains(item.Group))
                        excludeGroupList.Add(item.Group);

                    excludeNameList = item.Name;
                }
            }

            if (acceptGroupList.Count > 0 && acceptNameList.Count > 0)
            {
                data["acceptPaymentMethodGroup"] = string.Join(",", acceptGroupList.ToArray());
                data["acceptPaymentMethodName"] = string.Join(",", acceptNameList.ToArray());
            }

            if (excludeGroupList.Count > 0 && excludeNameList.Count > 0)
            {
                data["excludePaymentMethodGroup"] = string.Join(",", excludeGroupList.ToArray());
                data["excludePaymentMethodName"] = string.Join(",", excludeNameList.ToArray());
            }

            return data;
        }
    }
}
