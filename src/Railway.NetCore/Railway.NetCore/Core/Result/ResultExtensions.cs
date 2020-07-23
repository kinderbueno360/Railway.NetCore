namespace Railway.NetCore.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public static class ResultExtensions
    {
        public static Result OnSuccess(this Result result, Func<Result> func)
        {
            if (result.IsFailure)
                return result;

            return func();
        }

        public static Result OnSuccess(this Result result, Action action)
        {
            if (result.IsFailure)
                return result;

            action();

            return Result.Success();
        }

        public static Result OnSuccess<T>(this Result<T> result, Action<T> action)
        {
            if (result.IsFailure)
                return result;

            action(result.Value);

            return Result.Success();
        }

        public static Result<T> OnSuccess<T>(this Result result, Func<T> func)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            return Result.Success(func());
        }

        public static Result<T> OnSuccess<T>(this Result result, Func<Result<T>> func)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            return func();
        }

        public static Result OnSuccess<T>(this Result<T> result, Func<T, Result> func)
        {
            if (result.IsFailure)
                return result;

            return func(result.Value);
        }

        public static Result OnFailure(this Result result, Action action)
        {
            if (result.IsFailure)
            {
                action();
            }

            return result;
        }

        public static Result OnBoth(this Result result, Action<Result> action)
        {
            action(result);

            return result;
        }

        public static T OnBoth<T>(this Result result, Func<Result, T> func)
        {
            return func(result);
        }
    }
}
