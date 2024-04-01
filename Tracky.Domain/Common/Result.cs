using OneOf;

namespace Tracky.Domain.Common;

[GenerateOneOf]
public partial class Result<T> : OneOfBase<T, Error>;

public static class ResultExtensions
{
    public static Result<T> ToResult<T>(this T value) => value;

    public static Result<TResult> Map<T, TResult>(this Result<T> result, Func<T, TResult> mapping) =>
        result.Match<Result<TResult>>(
            value => mapping(value),
            error => error);

    public static Result<TResult> Bind<T, TResult>(this Result<T> result, Func<T, Result<TResult>> mapping) =>
        result.Match(
            mapping,
            error => error);

    public static Task<Result<TResult>> MapAsync<T, TResult>(this Result<T> result, Func<T, Task<TResult>> mapping) =>
        result.Match<Task<Result<TResult>>>(
            async value => await mapping(value),
            error => Task.FromResult<Result<TResult>>(error));

    public static Task<Result<TResult>> MapAsync<T, TResult>(this Task<Result<T>> result, Func<T, TResult> mapping) =>
        result.ContinueWith(task => task.Result.Map(mapping));

    public static async Task<Result<TResult>> MapAsync<T, TResult>(this Task<Result<T>> result,
        Func<T, Task<TResult>> mapping) =>
        await (await result).MapAsync(mapping);

    public static Task<Result<TResult>> BindAsync<T, TResult>(this Result<T> result,
        Func<T, Task<Result<TResult>>> mapping) =>
        result.Match(
            async value => await mapping(value),
            error => Task.FromResult<Result<TResult>>(error));

    public static Task<Result<TResult>> BindAsync<T, TResult>(this Task<Result<T>> result,
        Func<T, Result<TResult>> mapping) =>
        result.ContinueWith(task => task.Result.Bind(mapping));

    public static async Task<Result<TResult>> BindAsync<T, TResult>(this Task<Result<T>> result,
        Func<T, Task<Result<TResult>>> mapping) => await (await result).BindAsync(mapping);

    public static Result<T> Tap<T>(this Result<T> result, Action<T> action)
    {
        result.Switch(action, _ => { });

        return result;
    }

    public static async Task<Result<T>> TapAsync<T>(this Task<Result<T>> result, Action<T> action) =>
        (await result).Tap(action);

    public static Result<T> FirstOrError<T>(this IEnumerable<T> source, Func<T, bool> predicate, Error error) =>
        source.FirstOrDefault(predicate) ?? (Result<T>)error;
}
