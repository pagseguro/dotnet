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
using System.Text;
using Uol.PagSeguro.Domain.Authorization;

namespace Uol.PagSeguro.Domain.Authorization
{
    /// <summary>
    /// Represents a summary of a PagSeguro authorization, typically returned by search services.
    /// </summary>
    public class AuthorizationSummary 
    {

        private List<AuthorizationPermissions> permissions = new List<AuthorizationPermissions>();

        /// <summary>
        /// Code
        /// </summary>
        public string Code
        {
            get;
            internal set;
        }

        /// <summary>
        /// Date
        /// </summary>
        public DateTime CreationDate
        {
            get;
            internal set;
        }

        /// <summary>
        /// Reference code
        /// </summary>
        /// <remarks>
        /// You can use the reference code to store an identifier so you can 
        /// associate the PagSeguro transaction to a transaction in your system.
        /// </remarks>
        public string Reference
        {
            get;
            internal set;
        }

        /// <summary>
        /// Account
        /// </summary>
        public AuthorizationAccount Account
        {
            get;
            set;
        }

        /// <summary>
        /// Authorizations in this page
        /// </summary>
        public IList<AuthorizationPermissions> Permissions
        {
            get
            {
                return this.permissions;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.GetType().Name);
            builder.Append('(');
            builder.Append("Code=").Append(this.Code).Append(", ");
            builder.Append("CreationDate=").Append(this.CreationDate).Append(", ");
            builder.Append("Reference=").Append(this.Reference).Append(", ");
            if (this.Account != null) {
                builder.Append("PublicKey=").Append(this.Account.PublicKey).Append(", ");
            } else  {
                builder.Append("PublicKey=").Append("").Append(", ");
            }
            builder.Append("Permissions for this authorization=").Append("(");
            int counter = 1;
            foreach (AuthorizationPermissions permission in this.Permissions)
            {
                if (this.Permissions.Count == counter)
                {
                    builder.Append(permission.Code);
                }
                else
                {
                    builder.Append(permission.Code).Append(", ");
                    counter++;
                }
            }
            builder.Append(')');
            return builder.ToString();
        }
    }
}
