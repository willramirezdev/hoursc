using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Hours.Core
{
    /// <summary>
    /// Used to capture the the result of whether or not an operation has succedded
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result 
    {
        public Result(bool isSuccess, List<string> errorMessages = null)
        {
            this.errorMessages = errorMessages ?? new List<string>();
            this.IsSuccess = isSuccess;
        }

        /// <summary>
        /// Gets if the result is a failure;
        /// </summary>
        public bool IsFailure => !this.IsSuccess;

        /// <summary>
        /// Gets if the result is a success;
        /// </summary>
        public bool IsSuccess { get; private set; }

        private readonly List<string> errorMessages;
        /// <summary>
        /// Gets a list of error messages
        /// </summary>
        public IReadOnlyCollection<string> ErrorMessages => this.errorMessages;

        /// <summary>
        /// Combines multiple results together. If any results have failed,
        /// then the new Result will be a failure.
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public static Result Combine(params Result[] results)
        {
            bool isSuccess = results.All(x => x.IsSuccess);
            List<string> errorMessages = new List<string>();
            errorMessages.AddRange(results.SelectMany(x => x.ErrorMessages));

            return new Result(isSuccess, errorMessages);
        }
     }


    public class Result<T> : Result
    {       
        public Result(bool isSuccess, T returnResult, List<string> errorMessages = null) 
            : base(isSuccess, errorMessages)
        {
            this.ReturnResult = returnResult;
        }

        public T ReturnResult { get; private set; }

        public static Result<T> CreateError(List<string> errorMessages)
        {
            return new Result<T>(true, default(T), errorMessages);
        }

        public static Result<T> Create(T returnResult)
        {
            return new Result<T>(false, returnResult);
        }        
    }
}
