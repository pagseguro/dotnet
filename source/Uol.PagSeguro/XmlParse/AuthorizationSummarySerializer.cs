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
using Uol.PagSeguro.Domain.Authorization;

namespace Uol.PagSeguro.XmlParse
{
    /// <summary>
    /// 
    /// </summary>
    internal static class AuthorizationSummarySerializer
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="result">PagSeguro AuthorizationSummary</param>
        internal static void Read(XmlReader reader, AuthorizationSummary result)
        {
            if (reader.IsEmptyElement)
            {
                XMLParserUtils.SkipNode(reader);
                return;
            }

            reader.ReadStartElement(SerializerHelper.Authorization);
 
            reader.MoveToContent();

            while (!reader.EOF)
            {

                if (XMLParserUtils.IsEndElement(reader, SerializerHelper.Authorization))
                {
                    XMLParserUtils.SkipNode(reader);
                    break;
                }

                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case SerializerHelper.Code:
                            result.Code = reader.ReadElementContentAsString();
                            break;
                        case SerializerHelper.CreationDate:
                            result.CreationDate = reader.ReadElementContentAsDateTime();
                            break;
                        case SerializerHelper.Reference:
                            result.Reference = reader.ReadElementContentAsString();
                            break;
                        case SerializerHelper.Account:
                            AuthorizationAccountSerializer.Read(reader, result);
                            break;
                        case SerializerHelper.Permissions:
                            AuthorizationPermissionListSerializer.Read(reader, result.Permissions);
                            break;
                        default:
                            XMLParserUtils.SkipElement(reader);
                            throw new InvalidOperationException("Unexpected value = " + reader.Name);
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
