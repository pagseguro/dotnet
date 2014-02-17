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
using System.Collections.Generic;

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
                // Definindo a data de ínicio da consulta 
                DateTime initialDate = new DateTime(2014, 02, 10, 08, 50, 0);

                // Definindo a data de término da consulta
                DateTime finalDate = DateTime.Now.AddHours(-5);

                // Definindo o número máximo de resultados por página
                int maxPageResults = 10;

                // Definindo o número da página
                int pageNumber = 1;

                // Realizando a consulta
                TransactionSearchResult result =
                    TransactionSearchService.SearchByDate(
                        credentials,
                        initialDate,
                        finalDate,
                        pageNumber,
                        maxPageResults);

                if (result.Transactions.Count <= 0)
                {
                    Console.WriteLine("Nenhuma transação");
                }

                if (result.PreApprovals.Count <= 0)
                {
                    Console.WriteLine("Nenhuma assinatura");
                }

                foreach (TransactionSummary transaction in result.Transactions)
                {
                    Console.WriteLine("Começando listagem de transações - \n");
                    Console.WriteLine(transaction.ToString());
                    Console.WriteLine(" - Terminando listagem de transações ");
                }

                foreach (TransactionSummary transaction in result.PreApprovals)
                {
                    Console.WriteLine("Começando listagem de assinaturas - \n");
                    Console.WriteLine(transaction.ToString());
                    Console.WriteLine(" - Terminando listagem de assinaturas ");
                }

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
