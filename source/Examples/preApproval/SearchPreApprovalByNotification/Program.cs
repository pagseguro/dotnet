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

using Uol.PagSeguro.Resources;
using System;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Service;
using Uol.PagSeguro.Exception;

namespace FindPreApprovalByNotification
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isSandbox = true;
            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            // TODO: Substitute the code below with a valid code notification for your transaction
            String notificationCode = "29B0BEC9D653D653435EE42F3FAEF4461091";

            try
            {
                AccountCredentials credentials = PagSeguroConfiguration.GetAccountCredentials(isSandbox);

                PreApprovalTransaction result = PreApprovalSearchService.SearchByNofication(credentials, notificationCode);


            }
            catch (PagSeguroServiceException exception)
            {
                foreach (ServiceError element in exception.Errors)
                {

                }
            }
        }
    }
}
