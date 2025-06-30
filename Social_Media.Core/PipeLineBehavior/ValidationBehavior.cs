using FluentValidation;
using MediatR;

namespace Social_Media.Core.PipeLineBehavior
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        IEnumerable<IValidator<TRequest>> Validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> Validators)
        {
            this.Validators = Validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (Validators.Any())
            {
                var Context = new ValidationContext<TRequest>(request);
                var ValidationResult = Task.WhenAll(Validators.Select(V => V.ValidateAsync(Context, cancellationToken))).Result;
                var Failures = ValidationResult.SelectMany(VR => VR.Errors).Where(F => F is not null).ToList();
                if (Failures.Count != 0)
                {
                    var Message = Failures.Select(X => X.PropertyName + " : " + X.ErrorMessage).FirstOrDefault();
                    throw new ValidationException(Message);
                }
            }
            return await next();
        }
    }
}
