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
using Uol.PagSeguro.Domain.Installment;

namespace Uol.PagSeguro.XmlParse
{
    internal static class InstallmentSerializer
    {
        internal static void Read(XmlReader reader, Installment installment)
        {

            if (reader.IsEmptyElement)
            {
                XMLParserUtils.SkipNode(reader);
                return;
            }

            reader.ReadStartElement(SerializerHelper.Installment);
            reader.MoveToContent();

            while (!reader.EOF)
            {
                if (XMLParserUtils.IsEndElement(reader, SerializerHelper.Installment))
                {
                    XMLParserUtils.SkipNode(reader);
                    break;
                }

                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case SerializerHelper.CreditCardBrand:
                            installment.CardBrand = reader.ReadElementContentAsString();
                            break;

                        case SerializerHelper.Quantity:
                            installment.Quantity = reader.ReadElementContentAsInt();
                            break;

                        case SerializerHelper.Amount:
                            installment.Amount = reader.ReadElementContentAsDecimal();
                            break;

                        case SerializerHelper.TotalAmount:
                            installment.TotalAmount = reader.ReadElementContentAsDecimal();
                            break;

                        case SerializerHelper.InterestFree:
                            installment.InterestFree = reader.ReadElementContentAsBoolean();
                            break;

                        default:
                            XMLParserUtils.SkipElement(reader);
                            throw new InvalidOperationException("Unexpected value");
                    }
                }
                else
                {
                    XMLParserUtils.SkipNode(reader);
                }
            }
        }
    }
}
