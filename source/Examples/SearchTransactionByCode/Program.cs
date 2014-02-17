﻿// Copyright [2011] [PagSeguro Internet Ltda.]
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
using System.Net;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Service;

namespace SearchTransactionByCode
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: Substitute the parameters below with your credentials
            //AccountCredentials credentials = new AccountCredentials("your@email.com", "your_token_here");
            AccountCredentials credentials = PagSeguroConfiguration.Credentials;

            try
            {
                // TODO: Substitute the code below with a valid transaction code for your transaction
                Transaction transaction = TransactionSearchService.SearchByCode(credentials, "59A13D84-52DA-4AB8-B365-1E7D893052B0");

                Console.WriteLine(transaction);
                Console.ReadKey();

                Transaction preApprovalTransaction = TransactionSearchService.SearchByCode(credentials, "C08984179E9EDF3DD4023F87B71DE349", true);

                Console.WriteLine(preApprovalTransaction);
                Console.ReadKey();
            }
            catch (PagSeguroServiceException exception)
            {
                if (exception.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized: please verify if the credentials used in the web service call are correct.\n");
                }
                Console.ReadKey();
            }
        }
    }
}
