namespace Ragkivio.Graphql.Common;

using Ragkivio.Application.Common.Errors;
using FlIError = FluentResults.IError;
using ChError = HotChocolate.IError;

internal static class GraphqlErrorBuilder
{
    private const string AdminMessage = "An internal error occurred. Please contact support.";

    private const string DomainCode = "DOMAIN";
    private const string InfraCode = "INFRASTRUCTURE";
    private const string NotFoundCode = "NOT_FOUND";
    private const string AlreadyExistCode = "ALREADY_EXISTS";
    private const string UnknownCode = "UNKNOWN";
    private const string UnknownMessage = "Unknown error";

    internal static ChError Build(FlIError error) => error switch
    {
        InfrastructureError infraError => BuildInfrastructureError(infraError, InfraCode),
        DomainError domainError => BuildApplicationError(domainError, DomainCode),
        NotFoundError notFoundError => BuildApplicationError(notFoundError, NotFoundCode),
        AlreadyExistError alreadyExistError => BuildApplicationError(alreadyExistError, AlreadyExistCode),
        _ => UnknownError()
    };

    // TODO: add trace id later
    private static ChError BuildApplicationError(BaseError error, string code)
    {
        var builder = ErrorBuilder.New()
            .SetCode(code)
            .SetMessage(error.Message)
            .SetExtension("resource", error.Resource);

        if (!string.IsNullOrWhiteSpace(error.Identifier))
            builder.SetExtension("identifier", error.Identifier);

        if (error is DomainError domainError && !string.IsNullOrWhiteSpace(domainError.PropertyName))
            builder.SetExtension("field", domainError.PropertyName);

        return builder.Build();
    }

    private static ChError BuildInfrastructureError(BaseError error, string code) =>
        ErrorBuilder.New()
            .SetCode(code)
            .SetMessage(AdminMessage)
            .SetExtension("resource", error.Resource)
            .Build();

    internal static ChError UnknownError() =>
        ErrorBuilder.New()
            .SetCode(UnknownCode)
            .SetMessage(UnknownMessage)
            .Build();
}
