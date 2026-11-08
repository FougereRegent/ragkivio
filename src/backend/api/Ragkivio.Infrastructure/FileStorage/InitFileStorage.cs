using Amazon.S3;
using Microsoft.Extensions.Options;
using Ragkivio.Infrastructure.Common;
using Ragkivio.Infrastructure.Configuration.FileStorage;

namespace Ragkivio.Infrastructure.FileStorage;

internal sealed class InitFileStorage(IAmazonS3 amazonS3, IOptions<FileStorageOption> fileStorageOption) : IInit
{
    public async Task InitAsync(CancellationToken cancellationToken)
    {
        var bucketName = fileStorageOption.Value.BucketName;
        var responseBucket = await amazonS3.ListBucketsAsync(cancellationToken);
        var buckets = responseBucket.Buckets;

        var bucket = buckets.Any(pre => pre.BucketName == bucketName);
        if (bucket) return;

        try
        {
            await amazonS3.PutBucketAsync(bucketName, cancellationToken);
        }
        catch
        {
            throw;
        }
    }
}