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
//   limitation

using System;
using System.Xml;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Domain;

namespace Uol.PagSeguro.XmlParse
{
    /// <summary>
    /// 
    /// </summary>
    internal static class TransactionSerializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="transaction"></param>
        /// <param name="payment"></param>
        internal static void Read(XmlReader reader, Transaction transaction, bool payment = true)
        {

            if (reader.IsEmptyElement)
            {
                XMLParserUtils.SkipNode(reader);
                return;
            }

            reader.ReadStartElement(); 
            reader.MoveToContent();

            while (!reader.EOF)
            {

                if (XMLParserUtils.IsEndElement(reader, SerializerHelper.PreApproval))
                {
                    XMLParserUtils.SkipNode(reader);
                    break;
                }

                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case SerializerHelper.Code:
                            transaction.Code = reader.ReadElementContentAsString();
                            break;
                        case SerializerHelper.Date:
                            transaction.Date = reader.ReadElementContentAsDateTime();
                            break;
                        case SerializerHelper.Reference:
                            transaction.Reference = reader.ReadElementContentAsString();
                            break;
                        case SerializerHelper.TransactionType:
                            transaction.TransactionType = reader.ReadElementContentAsInt();
                            break;
                        case SerializerHelper.TransactionStatus:
                            transaction.TransactionStatus = reader.ReadElementContentAsInt();
                            break;
                        case SerializerHelper.PaymentLink:
                            transaction.PaymentLink = reader.ReadElementContentAsString();
                            break;
                        case SerializerHelper.GrossAmount:
                            transaction.GrossAmount = reader.ReadElementContentAsDecimal();
                            break;
                        case SerializerHelper.DiscountAmount:
                            transaction.DiscountAmount = reader.ReadElementContentAsDecimal();
                            break;
                        case SerializerHelper.CreditorFees:
                            CreditorFees creditorFees = new CreditorFees();
                            CreditorFeesSerializer.Read(reader, creditorFees);
                            transaction.CreditorFees = creditorFees;
                            break;
                        case SerializerHelper.FeeAmount:
                            transaction.FeeAmount = reader.ReadElementContentAsDecimal();
                            break;
                        case SerializerHelper.NetAmount:
                            transaction.NetAmount = reader.ReadElementContentAsDecimal();
                            break;
                        case SerializerHelper.ExtraAmount:
                            transaction.ExtraAmount = reader.ReadElementContentAsDecimal();
                            break;
                        case SerializerHelper.LastEventDate:
                            transaction.LastEventDate = reader.ReadElementContentAsDateTime();
                            break;
                        case SerializerHelper.InstallmentCount:
                            transaction.InstallmentCount = reader.ReadElementContentAsInt();
                            break;
                        case PaymentMethodSerializer.PaymentMethod:
                            PaymentMethod paymentMethod = new PaymentMethod();
                            PaymentMethodSerializer.Read(reader, paymentMethod);
                            transaction.PaymentMethod = paymentMethod;
                            break;
                        case ItemListSerializer.Items:
                            ItemListSerializer.Read(reader, transaction.Items);
                            break;
                        case SenderSerializer.Sender:
                            Sender sender = new Sender();
                            SenderSerializer.Read(reader, sender);
                            transaction.Sender = sender;
                            break;
                        case ShippingSerializer.Shipping:
                            Shipping shipping = new Shipping();
                            ShippingSerializer.Read(reader, shipping);
                            transaction.Shipping = shipping;
                            break;
                        default:
                            XMLParserUtils.SkipElement(reader);
                            throw new InvalidOperationException("Unexpected value");                    }
                }
                else
                {
                    XMLParserUtils.SkipNode(reader);
                }
            }
        }
    }
}
