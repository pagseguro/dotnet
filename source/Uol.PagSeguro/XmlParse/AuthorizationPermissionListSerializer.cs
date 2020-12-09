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
using Uol.PagSeguro.Domain.Authorization;

namespace Uol.PagSeguro.XmlParse
{
    /// <summary>
    /// 
    /// </summary>
    internal static class AuthorizationPermissionListSerializer
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="permissions">PagSeguro AuthorizationPermissions List</param>
        internal static void Read(XmlReader reader, IList<AuthorizationPermissions> permissions)
        {

            permissions.Clear();

            if (reader.IsEmptyElement)
            {
                XMLParserUtils.SkipNode(reader);
            }

            reader.ReadStartElement(SerializerHelper.Permissions);
            reader.MoveToContent();

            while (!reader.EOF)
            {

                if (XMLParserUtils.IsEndElement(reader, SerializerHelper.Permissions))
                {
                    XMLParserUtils.SkipNode(reader);
                    break;
                }

                if (reader.NodeType == XmlNodeType.Element)
                {
                    AuthorizationPermissions permission = new AuthorizationPermissions();
                    switch (reader.Name)
                    {
                        case SerializerHelper.Permission:
                            AuthorizationPermissionSerializer.Read(reader, permission);
                            permissions.Add(permission);
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
