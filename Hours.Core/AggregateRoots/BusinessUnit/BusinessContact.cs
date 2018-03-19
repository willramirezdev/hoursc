using Hours.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hours.Core.AggregateRoots.BusinessUnit
{
    /// <summary>
    /// Represents the Business Contacts for a Business Unit.
    /// </summary>
    public class BusinessContact : ValueObject
    {
        private BusinessContact(string name, string phone, string email)
        {
            this.Name = name;
            this.Phone = phone;
            this.Email = email;
        }

        /// <summary>
        /// Creates an instance of the Business Contact.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="email"></param>
        /// <returns>BusinessContact or error</returns>
        public static Result<BusinessContact> Create(string name, string phone, string email)
        {
            var validationResult = Result.Combine(
                ValidationHelper.ValidateStringLengthAndNullOrEmpty(name, nameof(name), 50),
                ValidationHelper.ValidateStringLengthAndNullOrEmpty(phone, nameof(phone), 25),
                ValidationHelper.ValidateEmail(email));

            if(validationResult.IsFailure)
            {
                return Result<BusinessContact>.CreateError(validationResult.ErrorMessages);
            }

            return Result<BusinessContact>.Create(new BusinessContact(name, phone, email));
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
