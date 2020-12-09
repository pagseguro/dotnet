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
using System.Linq;
using Uol.PagSeguro.Domain.Authorization;

namespace Uol.PagSeguro.Parse
{
    /// <summary>
    /// 
    /// </summary>
    internal static class AuthorizationParse
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorizationRequest">PagSeguro AuthorizationRequest</param>
        /// <returns></returns>
        public static IDictionary<string, string> GetData(AuthorizationRequest authorizationRequest)
        {
            IDictionary<string, string> data = new Dictionary<string, string>();

            // Reference
            if (!string.IsNullOrEmpty(authorizationRequest.Reference))
                data["reference"] = authorizationRequest.Reference;

            // RedirectURL
            if (!string.IsNullOrEmpty(authorizationRequest.RedirectURL))
                data["redirectURL"] = authorizationRequest.RedirectURL;

            // NotificationURL
            if (!string.IsNullOrEmpty(authorizationRequest.NotificationURL))
                data["notificationURL"] = authorizationRequest.NotificationURL;

            // Permissions
            if (authorizationRequest.Permissions != null && authorizationRequest.Permissions.Any())
                data["permissions"] = string.Join(",", authorizationRequest.Permissions.ToArray());

            return data;
        }
    }
}
