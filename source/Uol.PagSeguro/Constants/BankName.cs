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

namespace Uol.PagSeguro.Constants
{
    /// <summary>
    /// Defines a list of known bank names.
    /// </summary>
    /// <remarks>
    /// This class is not an enum to enable the introduction of new bank names
    /// without breaking this version of the library.
    /// </remarks>
    public static class BankName
    {
        /// <summary>
        /// Bradesco
        /// </summary>
        public static string Bradesco

        {
            get { return "bradesco"; }
        }

        /// <summary>
        /// Itaú
        /// </summary>
        public static string Itau
        {
            get { return "itau"; }
        }
        /// <summary>
        /// Banco do Brasil
        /// </summary>
        public static string BancoDoBrasil
        {
            get { return "bancodobrasil"; }
        }

        /// <summary>
        /// Banrisul
        /// </summary>
        public static string Banrisul
        {
            get { return "banrisul"; }
        }
        /// <summary>
        /// HSBC
        /// </summary>
        public static string HSBC
        {
            get { return "hsbc"; }
        }

    }
}
