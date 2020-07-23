using System;
using System.Collections.Generic;
using System.Text;

namespace Railway.NetCore.Core
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public string Error { get; private set; }

        public bool IsFailure
        {
            get { return !IsSuccess; }
        }

        protected Result(bool success, string error)
        {
            IsSuccess = success;
            Error = error;
        }

        public static Result Fail(string message)
        {
            return new Result(false, message);
        }

        public static Result<T> Fail<T>(string message)
        {
            return new Result<T>(default(T), false, message);
        }

        public static Result Success()
        {
            return new Result(true, String.Empty);
        }

        public static Result<T> Success<T>(T value)
        {
            return new Result<T>(value, true, String.Empty);
        }

        public static Result Combine(params Result[] results)
        {
            foreach (Result result in results)
            {
                if (result.IsFailure)
                    return result;
            }

            return Success();
        }
    }


    public class Result<T> : Result
    {
        private T _value;

        public T Value
        {
            get
            {
                return _value;
            }
            private set { _value = value; }
        }

        protected internal Result( T value, bool success, string error)
            : base(success, error)
        {
            Value = value;
        }
    }
}
