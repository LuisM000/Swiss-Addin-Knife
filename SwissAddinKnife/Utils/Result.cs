using System;
namespace SwissAddinKnife.Utils
{
        /// <summary>
    /// Result of operation (without Error field)
    /// </summary>
    /// <typeparam name="TResult">Type of Value field</typeparam>
    public struct Result<TResult>
    {
        private readonly bool _isSuccess;

        public readonly TResult Value;

        public bool IsSuccess => _isSuccess;
        public bool IsError => !_isSuccess;

        public Result(bool isSuccess)
        {
            _isSuccess = isSuccess;
            Value = default(TResult);
        }

        public Result(TResult result)
        {
            _isSuccess = true;
            Value = result;
        }

        public Result(bool isSuccess, TResult result)
        {
            _isSuccess = isSuccess;
            Value = result;
        }

        public static implicit operator bool(Result<TResult> result)
        {
            return result._isSuccess;
        }

        public static implicit operator Result<TResult>(TResult result)
        {
            return new Result<TResult>(result);
        }

    }
}
