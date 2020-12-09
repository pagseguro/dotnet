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
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Service;

namespace Uol.PagSeguro.Domain.Authorization
{
    /// <summary>
    /// Authorization request
    /// </summary>
    public class AuthorizationRequest
    {

        /// <summary>
        /// Reference code
        /// </summary>
        /// <remarks>
        /// Optional. You can use the reference code to store an identifier so you can associate the PagSeguro authorization
        /// to a authorization in your system.
        /// </remarks>
        public String Reference
        {
            get;
            set;
        }

        /// <summary>
        /// List of permissions in this authorization
        /// </summary>
        public List<String> Permissions
        {
            get;
            set;
        }

        /// <summary>
        /// Uri to where the PagSeguro checkout page should redirect the user after the payment information is processed.
        /// Typically this is a confirmation page on your web site.
        /// </summary>
        public String RedirectURL
        {
            get;
            set;
        }

        /// <summary>
        /// Determines for which url PagSeguro will send the order related notifications changes.
        /// </summary>
        /// <remarks>
        /// Optional. A new notification will be send to this url if any change happens in the transaction status. You can
        /// use that for update the related order.
        /// </remarks>
        public String NotificationURL
        {
            get;
            set;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        /// Create a new permission list
        /// </remarks>
        public AuthorizationRequest()
        {
            this.Permissions = new List<String>();
        }

        /// <summary>
        /// Add a permission to the permissions list
        /// </summary>
        public void addPermission(String permission)
        {
            if (Permissions.Count > 0)
            {
                this.Permissions.Add(permission);
            }
        }

        /**
         * Calls the PagSeguro web service and register this request for authorization
         * 
         * @param credentials
         * @return The URL to where the user needs to be redirected to in order to complete the authorization process
         * @throws PagSeguroServiceException
         */
        public String Register(Credentials credentials, Boolean onlyAuthorizationCode = false)
        {
            try
            {
                return AuthorizationService.CreateAuthorizationRequest(credentials, this, onlyAuthorizationCode);
            }
            catch (PagSeguroServiceException pse)
            {
                throw pse;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.GetType().Name).Append("(");
            builder.Append("Reference=").Append(this.Reference).Append(", ");
            builder.Append("Permissions=").Append(this.Permissions).Append(", ");
            builder.Append("RedirectURL=").Append(this.Reference).Append(", ");
            builder.Append("NotificationURL=").Append(this.NotificationURL).Append(")");
            return builder.ToString();
        }
    }
}
