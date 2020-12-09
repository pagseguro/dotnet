using System;
using System.Collections.Generic;
using System.Text;

namespace Uol.PagSeguro.Domain.Direct
{
    /// <summary>
    /// Represents the holder of the credit card payment
    /// </summary>
    public class Holder
    {
        /// <summary>
        /// Holder name
        /// </summary>
        public String Name
        {
            get;
            set;
        }

        /// <summary>
        /// Holder phone
        /// </summary>
        public Phone Phone
        {
            get;
            set;
        }

        /// <summary>
        /// Holder document
        /// </summary>
        public HolderDocument Document
        {
            get;
            set;
        }

        /// <summary>
        /// Holder birthdate
        /// </summary>
        public String Birthdate
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the Holder class
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="document"></param>
        /// <param name="birthDate"></param>
        public Holder(String name = null, Phone phone = null, HolderDocument document = null, String birthDate = null)
        {
            if (!String.IsNullOrEmpty(name))
            {
                this.Name = name;
            }
            if (phone != null)
            {
                this.Phone = phone;
            }
            if (document != null)
            {
                this.Document = document;
            }
            if (!String.IsNullOrEmpty(birthDate))
            {
                this.Birthdate = birthDate;
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
            builder.Append("Name=").Append(this.Name).Append(", ");
            builder.Append("Phone=").Append(this.Phone.ToString()).Append(", ");
            builder.Append("Document=").Append(this.Document.ToString());
            builder.Append("Birthdate=").Append(this.Birthdate);
            builder.Append(')');
            return builder.ToString();
        }
    }
}
