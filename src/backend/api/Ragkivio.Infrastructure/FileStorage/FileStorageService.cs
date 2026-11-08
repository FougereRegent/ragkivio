namespace Ragkivio.Infrastructure.FileStorage;

using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using Ragkivio.Application.Common;
using Ragkivio.Application.Document.Services;
using Ragkivio.Infrastructure.Configuration.FileStorage;

internal sealed class FileStorageService(IAmazonS3 amazonS3, IOptions<FileStorageOption> configuration)
{
    internal async Task SaveFileAsync(FileInputReferenceKey referenceKey, CancellationToken token)
    {
        var bucketName = configuration.Value.BucketName;

        var request = new PutObjectRequest
        {
            BucketName = bucketName,
            Key = referenceKey.Key,
            InputStream = referenceKey.Content,
            ContentType = referenceKey.MimeType,
            AutoCloseStream = true,
        };

        PutObjectResponse? response;

        try
        {
            await amazonS3.PutObjectAsync(request, token);
        }
        catch (AmazonS3Exception ex)
        {
            throw new FileStorageException(ex.Message);
        }
    }

    internal async Task<Stream> GetFileAsync(FileReferenceKey referenceKey, CancellationToken token)
    {
        var bucketName = configuration.Value.BucketName;

        var resultStream = new MemoryStream();
        try
        {
            var result = await amazonS3.GetObjectAsync(bucketName, referenceKey.Key, token);
            await result.ResponseStream.CopyToAsync(resultStream, token);

            resultStream.Seek(0, SeekOrigin.Begin);
            resultStream.Position = 0;

            return resultStream;
        }
        catch (AmazonS3Exception ex)
        {
            throw new FileStorageException(ex.Message);
        }
    }
}