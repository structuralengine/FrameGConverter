using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using System.IO.Compression;

namespace FramePrintPDF
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string testStr = req.Query["test"];
            if(testStr != null)
            {
                string responseMessage = string.IsNullOrEmpty(testStr)
                    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                    : $"Hello, {testStr}. This HTTP triggered function executed successfully.";

                return new OkObjectResult(responseMessage);
            }

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            // base64���f�R�[�h
            byte[] a = Convert.FromBase64String(requestBody);
            String stCsvData = Encoding.UTF8.GetString(a);

            // �J���}��؂�ŕ������Ĕz��Ɋi�[����
            string[] stArrayData = stCsvData.Split(',');

            // byte �z��ɕϊ�����
            byte[] b = new byte[stArrayData.Length];
            for (int i = 0; i < stArrayData.Length; i++)
                b[i] = Convert.ToByte(stArrayData[i]);

            // gzip��
            String jsonString = Unzip(b);

            var myPrintInput = new PrintInput(jsonString);
            string base64str = myPrintInput.getPdfSource();

            return new OkObjectResult(base64str);
        }

        /// <summary>
        /// ���k�f�[�^�𕶎���Ƃ��ĕ������܂��B
        /// </summary>
        public static string Unzip(byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    gs.CopyTo(mso);
                }

                // System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance); // memo: Shift-JIS���������߂̂��܂��Ȃ�
                // Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
                // return sjisEnc.GetString(mso.ToArray());
                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }

    }
}
