using FluentValidation;
using MediatR;

namespace Ordering.Application.Behaviours;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        this.validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(validators.Select(e => e.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(e => e.Errors).Where(f => f != null).ToList();

            if (failures.Count != 0)
            {
                throw new Ordering.Application.Exceptions.ValidationException(failures);
            }
        }
        return await next();
    }
}
