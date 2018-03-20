using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Hours.Core
{
    /// <summary>
    /// Used to capture the the result of whether or not an operation has succeeded.
    /// </summary>    
    public class Result 
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="errorMessages"></param>
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
        /// Creates a error result with error messages.
        /// </summary>
        /// <returns></returns>
        public static Result CreateError(string errorMessage)
        {
            return new Result(false, new List<string> { errorMessage });
        }

        /// <summary>
        /// Creates a error result with error messages.
        /// </summary>
        /// <returns></returns>
        public static Result CreateError(List<string> errorMessages)
        {
            return new Result(false, errorMessages);
        }

        /// <summary>
        /// Creates a successful result with a return value.
        /// </summary>
        /// <returns></returns>
        public static Result Create()
        {
            return new Result(true);
        }

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

    /// <summary>
    /// Used to capture the the result and return value of whether 
    /// or not an operation has succeeded.  
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T> : Result
    {    
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="returnValue"></param>
        /// <param name="errorMessages"></param>
        public Result(bool isSuccess, T returnValue, List<string> errorMessages = null) 
            : base(isSuccess, errorMessages)
        {
            this.ReturnValue = returnValue;
        }

        /// <summary>
        /// Gets the return value of the result.
        /// </summary>
        public T ReturnValue { get; private set; }

        /// <summary>
        /// Creates a error result with error messages.
        /// </summary>
        /// <param name="errorMessages"></param>
        /// <returns></returns>
        public new static Result<T> CreateError(List<string> errorMessages)
        {
            //TODO: fix method hiding
            return new Result<T>(false, default(T), errorMessages);
        }

        /// <summary>
        /// Creates a error result with error messages.
        /// </summary>
        /// <param name="errorMessages"></param>
        /// <returns></returns>
        public static Result<T> CreateError(IReadOnlyCollection<string> errorMessages)
        {
            return CreateError(errorMessages.ToList());
        }

        /// <summary>
        /// Creates a successful result with a return value.
        /// </summary>
        /// <param name="returnValue"></param>
        /// <returns></returns>
        public static Result<T> Create(T returnValue)
        {
            return new Result<T>(true, returnValue);
        }

        /// <summary>
        /// Combines multiple results together. If any results have failed,
        /// then the new Result will be a failure.
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public static Result<List<T>> Combine(params Result<T>[] results)
        {
            bool isSuccess = results.All(x => x.IsSuccess);
            List<string> errorMessages = results.SelectMany(x => x.ErrorMessages).ToList();
            var returnResults = results.Select(x => x.ReturnValue).ToList();

            return new Result<List<T>>(isSuccess, returnResults, errorMessages);
        }
    }
}
