using System;
using System.Collections.Generic;
using System.Text;

namespace Hours.Core.Utilities
{
    /// <summary>
    /// Holds validation helper methods.
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>
        /// Validates that a string is not null or empty and not greater
        /// than the max length.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxLength"></param>
        /// <param name="errorMessage"></param>
        public static void ValidateStringLengthAndNullOrEmpty(
            string str, 
            string stringName, 
            int maxLength)
        {
            if(string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException($"{stringName} is null or empty.");
            }

            if(str.Length > maxLength)
            {
                throw new ArgumentException($"{stringName} is longer than {maxLength} characters.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        public static void ValidateEmail(string email)
        {
            ValidateStringLengthAndNullOrEmpty(email, nameof(email), 200);

            //TODO: implement proper email validation
        }
    }
}
