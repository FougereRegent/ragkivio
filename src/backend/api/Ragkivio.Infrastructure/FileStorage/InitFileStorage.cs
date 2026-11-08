using Amazon.S3;
using Microsoft.Extensions.Options;
using Ragkivio.Infrastructure.Common;
using Ragkivio.Infrastructure.Configuration.FileStorage;

namespace Ragkivio.Infrastructure.FileStorage;

internal sealed class InitFileStorage(IAmazonS3 amazonS3, IOptions<FileStorageOption> fileStorageOption) : IInit
{
    private const int MaxAttempts = 5;
    private static readonly TimeSpan RetryDelay = TimeSpan.FromSeconds(2);

    public async Task InitAsync(CancellationToken cancellationToken)
    {
        var bucketName = fileStorageOption.Value.BucketName;

        for (var attempt = 1; ; attempt++)
        {
            try
            {
                await EnsureBucketExistsAsync(bucketName, cancellationToken);
                return;
            }
            catch (Exception exception) when (attempt < MaxAttempts
                                              && !cancellationToken.IsCancellationRequested
                                              && IsTransient(exception))
            {
                await Task.Delay(RetryDelay, cancellationToken);
            }
        }
    }

    private async Task EnsureBucketExistsAsync(string bucketName, CancellationToken cancellationToken)
    {
        var responseBucket = await amazonS3.ListBucketsAsync(cancellationToken);
        var buckets = responseBucket.Buckets;

        var bucket = buckets?.Any(pre => pre.BucketName == bucketName) ?? false;
        if (bucket) return;

        await amazonS3.PutBucketAsync(bucketName, cancellationToken);
    }

    private static bool IsTransient(Exception exception) => exception switch
    {
        TaskCanceledException => true,
        HttpRequestException => true,
        AmazonS3Exception s3Exception => (int)s3Exception.StatusCode >= 500,
        _ => false,
    };
}
