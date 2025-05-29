// using Microsoft.Extensions.Configuration;
// using Minio;
//
// namespace Infrastructure.Services;
//
// public class MinioService
// {
//     private readonly string _bucketName;
//     private readonly IMinioClient _minioClient;
//
//     public MinioService(IConfiguration configuration)
//     {
//         var minioEndpoint = configuration["Minio:Endpoint"] ?? "";
//         var minioAccessKey = configuration["Minio:AccessKey"] ?? "";
//         var minioSecretKey = configuration["Minio:SecretKey"] ?? "";
//         _bucketName = configuration["Minio:BucketName"] ?? "qms";
//         
//         _minioClient = new MinioClient()
//             .WithEndpoint(minioEndpoint)
//             .WithCredentials(minioAccessKey, minioSecretKey)
//             .Build();
//     }
//     
//     
//     
//     
//     public MemoryStream ConvertToMemoryStream(IFormFile formFile)
//     {
//         MemoryStream memoryStream = new();
//         formFile.CopyTo(memoryStream);
//         memoryStream.Seek(0, SeekOrigin.Begin); // 重置流的位置到开头，这样可以从头开始读取数据  
//         return memoryStream;
//     }
//     
//     public async Task<string> AsyncUploadFilesMinIO(FileUploadPara parm, string date)
//     {
//         // 检查是否存在桶
//         var bucketExists = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket("mes2"))
//             .ConfigureAwait(false);
//         if (!bucketExists)
//             await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket("mes2")).ConfigureAwait(false);
//
//         // 验证数据是否为空
//         if (parm == null || parm.FileData == null) return "";
//
//         var folderName = parm.BusinessType?.ToLower() ?? "";
//         var uuid = _hashHelper.GetUUID();
//         var fileName = uuid + parm.FileSuffix;
//
//         // 重置流位置
//         parm.FileData.Position = 0;
//
//         // 获取文件类型
//         var provider = new FileExtensionContentTypeProvider();
//         if (!provider.TryGetContentType(fileName, out var mimeType)) mimeType = "application/octet-stream";
//
//         // 上传文件到 Minio
//         await _minioClient.PutObjectAsync(new PutObjectArgs()
//             .WithBucket(_bucketName)
//             .WithObject($"{folderName}/{fileName}") // 设置文件夹和文件名
//             .WithStreamData(parm.FileData)
//             .WithObjectSize(parm.FileData.Length)
//             .WithContentType(mimeType)).ConfigureAwait(false);
//
//         // 保存文件到files表
//         await AsyncSaveFileToTable(parm, uuid, date);
//
//         // 返回数据
//         return uuid;
//     }
//     
//     public async Task<FileStreamResult?> AsyncGetFilePreviewMinIO(string id)
//     {
//         // 获取文件 url 列表
//         var path = await AsyncGetFilePathMinIO(id);
//         if (string.IsNullOrEmpty(path.Path) || string.IsNullOrEmpty(path.FileName)) return null;
//         var filePath = path.Path;
//         var fileName = path.FileName;
//         var provider = new FileExtensionContentTypeProvider();
//         if (!provider.TryGetContentType(fileName, out var mimeType)) mimeType = "application/octet-stream";
//         // 创建内存流
//         MemoryStream memoryStream = new();
//         // 下载文件到 MemoryStream
//         var getObjectArgs = new GetObjectArgs()
//             .WithBucket(_bucketName)
//             .WithObject(filePath)
//             .WithCallbackStream(async stream => { await stream.CopyToAsync(memoryStream); });
//         var getObjectResult = await _minioClient.GetObjectAsync(getObjectArgs);
//
//         // 定位到 0
//         memoryStream.Seek(0, SeekOrigin.Begin);
//
//         // 转成文件流
//         FileStreamResult fileStreamResult = new(memoryStream, mimeType)
//         {
//             FileDownloadName = fileName
//         };
//
//         return fileStreamResult;
//     }
//     
//     public async Task<string> AsyncDeleteFileMinIO(string id)
//     {
//         var path = await AsyncGetFilePathMinIO(id);
//         if (string.IsNullOrEmpty(path.Path) || string.IsNullOrEmpty(path.FileName)) return "";
//         var filePath = path.Path;
//         var removeObjectArgs = new RemoveObjectArgs()
//             .WithBucket(_bucketName)
//             .WithObject(filePath);
//         await _minioClient.RemoveObjectAsync(removeObjectArgs).ConfigureAwait(false);
//         ;
//         await AsyncDeleteFileToTableMinIO(id);
//         return "success";
//     }
// }

