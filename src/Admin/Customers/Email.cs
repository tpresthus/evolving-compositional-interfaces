using System;
using System.Net.Mail;

namespace Admin.Customers
{
    public class Email
    {
        private readonly string email;

        public Email(string email)
        {
            if (email == null)
            {
                throw new ArgumentNullException(nameof(email));
            }

            try {
                new MailAddress(email);
            }
            catch (Exception exception)
            {
                throw new ArgumentException("Invalid e-mail address", nameof(email), exception);
            }

            this.email = email;
        }

        public override string ToString() => this.email;
    }
}
