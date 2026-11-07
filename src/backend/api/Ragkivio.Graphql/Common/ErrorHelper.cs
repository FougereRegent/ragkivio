namespace Ragkivio.Graphql.Common;

using FluentResults;

public static class ErrorHelper
{
    extension<T>(Result<T> result)
    {
        public T ToGraphQlException()
        {
            if(!IsInError(result)) return result.Value;
            var error = ExtractError(result);

            var graphqlError = GraphqlErrorBuilder.BuildException(error);
            throw new GraphQLException(graphqlError);
        }
    }

    extension(Result result)
    {
        public void ToGraphQlException()
        {
            if(!IsInError(result)) return;
            var error = ExtractError(result);

            var graphqlError = GraphqlErrorBuilder.BuildException(error);
            throw new GraphQLException(graphqlError);
        }
    }

    private static bool IsInError(Result result) 
        => result.IsFailed;
    private static bool IsInError<T>(Result<T> result) 
        => result.IsFailed;

    private static IError ExtractError(Result result) =>
        result.Errors[0];
    private static IError ExtractError<T>(Result<T> result) =>
        result.Errors[0];
}