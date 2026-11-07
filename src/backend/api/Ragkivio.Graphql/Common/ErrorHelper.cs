namespace Ragkivio.Graphql.Common;

using FluentResults;

internal static class ErrorHelper
{
    extension<T>(Result<T> result)
    {
        public T UnwrapOrThrow()
        {
            if (result.IsSuccess)
                return result.Value;

            throw ToGraphQlException(result.Errors);
        }
    }

    extension(Result result)
    {
        public void UnwrapOrThrow()
        {
            if (result.IsSuccess)
                return;

            throw ToGraphQlException(result.Errors);
        }
    }

    private static GraphQLException ToGraphQlException(IReadOnlyList<IError> errors) =>
        new(errors.Count > 0
            ? errors.Select(GraphqlErrorBuilder.Build).ToArray()
            : [GraphqlErrorBuilder.UnknownError()]);
}
