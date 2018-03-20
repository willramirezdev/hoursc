using Hours.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hours.Core.AggregateRoots.BusinessUnitAggregate
{
    /// <summary>
    /// Holds the details of a business unit.
    /// </summary>
    public class BusinessUnit : AggregateRoot
    {
        private BusinessUnit(
            string name,
            string department,
            string description,
            List<BusinessContact> businessContacts,
            int aggregateVersion = 0)
        {
            this.Name = name;
            this.Department = department ?? string.Empty;
            this.Description = description ?? string.Empty;
            
            this.businessContacts = businessContacts ?? new List<BusinessContact>();
            this.AggregateVersion = aggregateVersion; 
        }

        /// <summary>
        /// Gets the business unit name.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets the business unit department.
        /// </summary>
        public string Department { get; protected set; }

        /// <summary>
        /// Gets the business unit description.
        /// </summary>
        public string Description { get; protected set; }

        private List<BusinessContact> businessContacts;
        /// <summary>
        /// Gets the list of business contacts.
        /// </summary>
        public IReadOnlyCollection<BusinessContact> BusinessContacts => this.businessContacts;

        /// <summary>
        /// Creates a new instance of a business unit.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="department"></param>
        /// <param name="description"></param>
        /// <param name="businessContacts"></param>
        /// <param name="aggregateVersion"></param>
        /// <returns>Business unit or error</returns>
        public static Result<BusinessUnit> Create(
            string name,
            string department,
            string description,
            List<BusinessContact> businessContacts = null,
            int aggregateVersion = 0)
        {
            var result = ValidateName(name);

            if(businessContacts?.Count > 3)
            {
                result = Result.Combine(
                        result,
                        Result.CreateError($"You cannot have more than 3 business contacts for a business unit.")
                        );
            }

            if (result.IsFailure)
            {
                return Result<BusinessUnit>.CreateError(result.ErrorMessages);
            }            

            return Result<BusinessUnit>.Create(new BusinessUnit(name, department, description, businessContacts, aggregateVersion));
        }

        /// <summary>
        /// Validates the name of the business unit.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static Result ValidateName(string name)
        {
            return ValidationHelper.ValidateStringLengthAndNullOrEmpty(name, nameof(name), 50);
        }

        /// <summary>
        /// Updates the text fields of the business unit.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="department"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public Result UpdateTextFields(string name, string department, string description)
        {
            var result = ValidateName(name);

            if(result.IsFailure)
            {
                return result;
            }

            this.Name = name;
            this.Department = department ?? string.Empty;
            this.Description = description ?? string.Empty;

            this.HasChanges();

            //TODO: raise domain event

            return Result.Create();
        }

        /// <summary>
        /// Updates the business contacts of the business unit.
        /// </summary>
        /// <param name="businessContacts"></param>
        /// <returns></returns>
        public Result UpdateBusinessContents(List<BusinessContact> businessContacts)
        {
            if(businessContacts == null || businessContacts.Count == 0)
            {
                return Result.CreateError($"Invalid number of business contacts specified for a business unit.");
            }

            if(businessContacts.Count > 3)
            {
                return Result.CreateError($"You cannot have more than 3 business contacts for a business unit.");
            }

            if(this.businessContacts.Count == businessContacts.Count &&
                this.businessContacts.All(x => businessContacts.Contains(x)))
            {
                // the list is exactly the same, prevent saving
                return Result.Create();  
            } 

            this.businessContacts = businessContacts;
            this.HasChanges();

            //TODO: raise domain event

            return Result.Create();
        }
    }
}
