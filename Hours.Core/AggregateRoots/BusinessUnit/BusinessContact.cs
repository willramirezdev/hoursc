using Hours.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hours.Core.AggregateRoots.BusinessUnit
{
    public class BusinessContact : ValueObject
    {
        private BusinessContact(string name, string phone, string email)
        {
            this.Name = name;
            this.Phone = phone;
            this.Email = email;
        }

        public static BusinessContact Create(string name, string phone, string email)
        {
            ValidationHelper.ValidateStringLengthAndNullOrEmpty(name, nameof(name), 50);
            ValidationHelper.ValidateStringLengthAndNullOrEmpty(phone, nameof(phone), 50);
            ValidationHelper.ValidateEmail(email);

            return new BusinessContact(name, phone, email);
        }

        public string Name { get; private set; }

        public string Phone { get; private set; }

        public string Email { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Name;
            yield return this.Phone;
            yield return this.Email;
        }
    }
}
