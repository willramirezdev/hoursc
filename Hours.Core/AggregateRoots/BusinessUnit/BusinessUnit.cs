using Hours.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hours.Core.AggregateRoots.BusinessUnit
{
    /// <summary>
    /// Holds the details of a business unit.
    /// </summary>
    public class BusinessUnit : AggregateRoot
    {
        public BusinessUnit(
            string name,
            string department,
            string description,
            IEnumerable<BusinessContact> businessContacts,
            int aggregateVersion = 0)
        {
            this.Name = name;
            this.Department = department;
            this.Description = description;
            this.businessContacts = businessContacts.ToList();
            this.AggregateVersion = 0; 
        }

        public string Name { get; protected set; }

        public string Department { get; protected set; }

        public string Description { get; protected set; }

        private readonly List<BusinessContact> businessContacts;
        public IReadOnlyCollection<BusinessContact> BusinessContacts => this.businessContacts;

        public static BusinessUnit Create(string name, string department, string description, List<BusinessContact> businessContacts = null)
        {
            ValidationHelper.ValidateStringLengthAndNullOrEmpty(name, nameof(name), 50);
            ValidationHelper.ValidateStringLengthAndNullOrEmpty(department, nameof(department), 100);
            ValidationHelper.ValidateStringLengthAndNullOrEmpty(description, nameof(description), 100);

            if(businessContacts?.Count > 3)
            {
                throw new ArgumentException($"You cannot have more than 3 business contacts for the {name} business unit.");
            }

            return new BusinessUnit(name, department, description, businessContacts);
        }

        public void UpdateBusinessUnit()
        {
            
        }
    }
}
