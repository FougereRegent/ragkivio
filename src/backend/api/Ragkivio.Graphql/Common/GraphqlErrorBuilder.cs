namespace Ragkivio.Graphql.Common;

using Ragkivio.Application.Common.Errors;
using FlIError = FluentResults.IError;
using ChError = HotChocolate.IError;

internal static class GraphqlErrorBuilder
{
    private const string AdminMessage = "contact admin please";

    private const string DomainCode = "DOMAIN";
    private const string InfraCode = "INFRASTRUCTURE";
    private const string NotFoundCode = "NOT_FOUND";
    private const string AlreadyExistCode = "ALREADY_EXIST";

    internal static ChError BuildException(FlIError error)
    {
        var baseError = error as BaseError;

        if (baseError == null)
            return ErrorBuilder.New()
                .SetCode("unknow")
                .SetMessage("unknow error")
                .Build();

        return baseError switch
        {
            InfrastructureError infraErr => InfrastructureError(infraErr, InfraCode),
            DomainError domainError => ApplicationErrorBuilder(domainError, DomainCode),
            NotFoundError notFoundError => ApplicationErrorBuilder(notFoundError, NotFoundCode),
            AlreadyExistError alreadyExistError => ApplicationErrorBuilder(alreadyExistError, AlreadyExistCode),
            _ => UnknownError()
        };
    }

    //TODO : add trace id later
    private static ChError ApplicationErrorBuilder(BaseError error, string code) =>
        ErrorBuilder.New()
            .SetCode(code)
            .SetMessage(error.Message)
            .SetExtension("ressource", error.Ressource)
            .Build();

    private static ChError InfrastructureError(BaseError error, string code) =>
        ErrorBuilder.New()
            .SetCode(code)
            .SetMessage(AdminMessage)
            .SetExtension("ressource", error.Ressource)
            .Build();

    private static ChError UnknownError() =>
        ErrorBuilder.New()
            .SetCode("unknow")
            .SetMessage("unknow error")
            .Build();
}