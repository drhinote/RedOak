using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Roi.Data;

namespace OData_Server.Controllers
{
    public class TestBinarysController : ApiController
    {
        public IHttpActionResult Post(TestBinary item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                UploadToBlob(item);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        private static HashAlgorithm SHA256Algo = CryptoProviderFactory.Default.CreateHashAlgorithm(HashAlgorithmName.SHA256);
        public static void UploadToBlob(TestBinary data)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(System.Configuration.ConfigurationManager.AppSettings["testblob"]);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("testbinary/");

            var bin = Convert.FromBase64String(data.Data);
            var hash = Convert.ToBase64String(SHA256Algo.ComputeHash(bin));
            if (bin.Length != data.Size || hash != data.Hash) throw new Exception("Hash Mismatch");

            var folder = container.GetDirectoryReference(data.TestId.ToString());
            folder.GetBlockBlobReference(data.TestSegment).UploadFromByteArray(bin, 0, data.Size);
        }


    }
}