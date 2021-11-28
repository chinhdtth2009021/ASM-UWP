using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM_DUONGTHICHINH.Services
{
   public class FileService
    {
        private static string CloudName = "xuanhung2401";
        private static string CloudApiKey = "882796476526336";
        private static string CloudApiSecret = "gOOT_72AMyn9TQz1Hd4MxyGRjxY";
        static CloudinaryDotNet.Account account;
        static CloudinaryDotNet.Cloudinary cloud;

        public FileService()
        {
            if (account == null)
            {
                account = new CloudinaryDotNet.Account
                {
                    Cloud = CloudName,
                    ApiKey = CloudApiKey,
                    ApiSecret = CloudApiSecret
                };
            }
            if (cloud == null)
            {
                cloud = new CloudinaryDotNet.Cloudinary(account);
                cloud.Api.Secure = true;
            }
        }

        public async Task<string> UploadFile(Windows.Storage.StorageFile file)
        {
            if (file != null)
            {
                Debug.WriteLine(file.Path);
                CloudinaryDotNet.Actions.RawUploadParams imageUploadParams
                   = new CloudinaryDotNet.Actions.RawUploadParams
                   {
                       File = new CloudinaryDotNet.FileDescription(file.Name, await file.OpenStreamForReadAsync())
                   };
                CloudinaryDotNet.Actions.RawUploadResult result = await cloud.UploadAsync(imageUploadParams);
                return result.SecureUrl.OriginalString;
            }
            return null;
        }
    }
}
