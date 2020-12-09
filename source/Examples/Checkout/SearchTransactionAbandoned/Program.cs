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
using System.Net;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Service;
using System.Collections.Generic;

namespace SearchTransactionAbandoned
{
    class Program
    {
        static void Main(string[] args)
        {

            bool isSandbox = false;
            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            // Definindo a data de ínicio da consulta 
            DateTime initialDate = new DateTime(2015, 10, 14, 08, 50, 0);

            // Definindo a data de término da consulta
            DateTime finalDate = DateTime.Now.AddMinutes(-15);

            // Definindo o número máximo de resultados por página
            int maxPageResults = 10;

            // Definindo o número da página
            int pageNumber = 1;

            try
            {

                AccountCredentials credentials = PagSeguroConfiguration.GetAccountCredentials(isSandbox);

                // Realizando a consulta
                TransactionSearchResult result =
                    TransactionSearchService.SearchAbandoned(
                        credentials,
                        initialDate,
                        finalDate,
                        pageNumber,
                        maxPageResults);

                if (result.Transactions.Count <= 0)
                {
                    throw new Exception("Nenhuma transação abandonada");
                }

                foreach (TransactionSummary transaction in result.Transactions)
                {

                }

        

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