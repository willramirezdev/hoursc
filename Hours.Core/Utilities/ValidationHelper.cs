using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;

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
        public static Result ValidateStringLengthAndNullOrEmpty(
            string str, 
            string stringName, 
            int maxLength)
        {
            if(string.IsNullOrEmpty(str))
            {
                return Result.CreateError($"{stringName} is null or empty.");                
            }

            if(str.Length > maxLength)
            {
                return Result.CreateError($"{stringName} is longer than {maxLength} characters.");
            }

            return Result.Create();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        public static Result ValidateEmail(string email)
        {            
            var lengthResult = 
                ValidateStringLengthAndNullOrEmpty(email, nameof(email), 200);

            Result formatResult = null;
            try
            {
                new MailAddress(email);
                formatResult = Result.Create();
            }
            catch(FormatException)
            {
                //TODO: find a way to handle this without slow exceptions
                formatResult = Result.CreateError("Invalid email format.");
            }

            return Result.Combine(lengthResult, formatResult);
        }
    }
}
