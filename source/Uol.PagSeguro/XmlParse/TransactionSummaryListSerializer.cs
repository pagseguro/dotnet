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
using System.Collections.Generic;
using System.Xml;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Domain;

namespace Uol.PagSeguro.XmlParse
{
    /// <summary>
    /// 
    /// </summary>
    internal static class TransactionSummaryListSerializer
    {
        internal const string Transactions = "transactions";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="transactions"></param>
        internal static void Read(XmlReader reader, IList<TransactionSummary> transactions)
        {

            transactions.Clear();

            if (reader.IsEmptyElement)
            {
                XMLParserUtils.SkipNode(reader);
            }

            reader.ReadStartElement(TransactionSummaryListSerializer.Transactions);
            reader.MoveToContent();

            while (!reader.EOF)
            {
                if (XMLParserUtils.IsEndElement(reader, TransactionSummaryListSerializer.Transactions))
                {
                    XMLParserUtils.SkipNode(reader);
                    break;
                }

                if (reader.NodeType == XmlNodeType.Element)
                {
                    TransactionSummary transaction = new TransactionSummary();
                    switch (reader.Name)
                    {
                        case SerializerHelper.Transaction:
                            TransactionSummarySerializer.Read(reader, transaction);
                            transactions.Add(transaction);
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
