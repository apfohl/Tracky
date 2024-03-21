using OneOf;

namespace Tracky.Domain.Common;

[GenerateOneOf]
public partial class Result<T> : OneOfBase<T, DomainError>;

public static class ResultExtensions
{
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
}
