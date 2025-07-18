using Tracky.Domain.Common;

namespace Tracky.DomainTests;

internal sealed record TestError : Error;

public static class ResultTests
{
    [Test]
    public static void ToResult_with_value_returns_successful_result()
    {
        var result = "Test".ToResult();

        result.Switch(
            value => value.Should().Be("Test"),
            _ => Assert.Fail()
        );
    }

    [Test]
    public static void Map_with_successful_result_returns_result_with_new_value()
    {
        const int value = 42;
        Result<int> result = value;

        result
            .Map(number => number.ToString())
            .Switch(
                s => s.Should().Be(value.ToString()),
                error => Assert.Fail(error.ToString()));
    }

    [Test]
    public static void Map_with_failed_result_returns_result_with_error()
    {
        Result<int> result = new TestError();

        result
            .Map(number => number.ToString())
            .Switch(
                _ => Assert.Fail(),
                error => error.Should().BeOfType<TestError>());
    }

    [Test]
    public static void Bind_with_failed_result_returns_result_with_error()
    {
        Result<int> result = new TestError();

        result
            .Bind(number => (Result<string>)number.ToString())
            .Switch(
                _ => Assert.Fail(),
                error => error.Should().BeOfType<TestError>());
    }

    [Test]
    public static void Bind_with_successful_result_returns_result_with_new_value()
    {
        const int value = 42;
        Result<int> result = value;

        result
            .Bind(number => (Result<string>)number.ToString())
            .Switch(
                s => s.Should().Be(value.ToString()),
                error => Assert.Fail(error.ToString()));
    }

    [Test]
    public static async Task MapAsync_success_result_with_async_mapping_returns_success_result_task()
    {
        Result<int> result = 42;

        await result.MapAsync(value => Task.FromResult(value.ToString()))
            .ContinueWith(task => task.Result.Switch(
                value => value.Should().Be("42"),
                _ => Assert.Fail()));
    }

    [Test]
    public static async Task MapAsync_success_result_Task_with_mapping_returns_success_result_task()
    {
        var result = Task.FromResult<Result<int>>(42);

        await result.MapAsync(value => value.ToString())
            .ContinueWith(task => task.Result.Switch(
                value => value.Should().Be("42"),
                _ => Assert.Fail()));
    }

    [Test]
    public static async Task MapAsync_success_result_task_with_async_mapping_returns_success_result_task()
    {
        var result = Task.FromResult<Result<int>>(42);

        await result.MapAsync(value => Task.FromResult(value.ToString()))
            .ContinueWith(task => task.Result.Switch(
                value => value.Should().Be("42"),
                _ => Assert.Fail()));
    }

    [Test]
    public static void MapAsync_with_failed_result_returns_result_with_error()
    {
        Result<int> result = new TestError();

        result
            .MapAsync(value => Task.FromResult(value.ToString()))
            .ContinueWith(task => task.Result.Switch(
                _ => Assert.Fail(),
                error => error.Should().BeOfType<TestError>()));
    }

    [Test]
    public static async Task BindAsync_success_result_with_async_mapping_returns_success_result_task()
    {
        Result<int> result = 42;

        await result.BindAsync(value => Task.FromResult<Result<string>>(value.ToString()))
            .ContinueWith(task => task.Result.Switch(
                value => value.Should().Be("42"),
                _ => Assert.Fail()
            ));
    }

    [Test]
    public static async Task BindAsync_success_result_task_with_mapping_returns_success_result_task()
    {
        var result = Task.FromResult<Result<int>>(42);

        await result.BindAsync(value => (Result<string>)value.ToString())
            .ContinueWith(task => task.Result.Switch(
                value => value.Should().Be("42"),
                _ => Assert.Fail()
            ));
    }

    [Test]
    public static async Task BindAsync_success_result_task_with_async_mapping_returns_success_result_task()
    {
        var result = Task.FromResult<Result<int>>(42);

        await result.BindAsync(value => Task.FromResult<Result<string>>(value.ToString()))
            .ContinueWith(task => task.Result.Switch(
                value => value.Should().Be("42"),
                _ => Assert.Fail()
            ));
    }

    [Test]
    public static void BindAsync_with_failed_result_returns_result_with_error()
    {
        Result<int> result = new TestError();

        result
            .BindAsync(value => Task.FromResult<Result<string>>(value.ToString()))
            .ContinueWith(task => task.Result.Switch(
                _ => Assert.Fail(),
                error => error.Should().BeOfType<TestError>()));
    }
}
