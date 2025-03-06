using Ardalis.Result;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace Business.Behaviors;

/// <summary>
/// Represents the validation behavior middleware.
/// </summary>
/// <typeparam name="TRequest">The request type.</typeparam>
/// <typeparam name="TResponse">The response type.</typeparam>
public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var errors = validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .Select(x => new ValidationError(x.ErrorMessage))
            .ToList();

        if (!errors.Any())
        {
            return await next();
        }

        var resultType = typeof(TResponse);

        if (resultType == typeof(Result))
        {
            var result = Result.Invalid(errors);
            return (TResponse)(object)result;
        }
        if (resultType.IsGenericType && resultType.GetGenericTypeDefinition() == typeof(Result<>))
        {
            var resultGenericType = resultType.GetGenericArguments().First();

            var resultTypeInstance = typeof(Result<>).MakeGenericType(resultGenericType);

            var invalidMethod = resultTypeInstance.GetMethod(
                "Invalid",
                BindingFlags.Public | BindingFlags.Static,
                null,
                [typeof(IEnumerable<ValidationError>)],
                null);

            var result = invalidMethod?.Invoke(null, [errors]);

            return (TResponse)result!;
        }

        throw new InvalidOperationException($"Unsupported result type: {resultType.FullName}");

    }
}