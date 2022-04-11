using Ionic.Zip;
using Izibiz.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace Izibiz.Operations
{
    public class FolderOperations
    {
        public static string FolderPath(Enum type)
        {
            string folderpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"\\Izibiz_dotnet_soap_client_files";
            CreateIfFileNotExists(folderpath);
            if (type.Equals(EI.Type.EInvoice))
            {
                return string.Format("{0}{1}", folderpath, @"\EFatura\"); ;
            }
            else if (type.Equals(EI.Type.EArchive))
            {
                return string.Format("{0}{1}", folderpath, @"\ArsivFatura\");
            }
            else if (type.Equals(EI.Type.EDespatch))
            {
                return string.Format("{0}{1}", folderpath, @"\EIrsaliye\");
            }
            else if (type.Equals(EI.Type.EDespatchReceipt))
            {
                return string.Format("{0}{1}", folderpath, @"\EIrsaliyeYaniti\");
            }
            else if (type.Equals(EI.Type.CreditNote))
            {
                return string.Format("{0}{1}", folderpath, @"\Mustahsil\");
            }
            else if (type.Equals(EI.Type.ESmm))
            {
                return string.Format("{0}{1}", folderpath, @"\Smm\");
            }
            else if (type.Equals(EI.Type.EExchange))
            {
                return string.Format("{0}{1}", folderpath, @"\EDoviz\");
            }
            else if (type.Equals(EI.Type.ECheck))
            {
                return string.Format("{0}{1}", folderpath, @"\EAdisyon\");
            }

            return null;
        }
        public static void CreateIfFileNotExists(String path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void SaveToDisk(Enum type, Enum documentType, Dictionary<string, byte[]> text)
        {
            var folderPath = FolderPath(type);

            CreateIfFileNotExists(folderPath);

            foreach (var invoice in text)
            {
                File.WriteAllBytes(folderPath + invoice.Key + "." + documentType.ToString().ToLower(), invoice.Value);
            }
        }


        public static byte[] DecompressZip(byte[] data)
        {
            // string myString;
            using (var ms = new MemoryStream(data))
            {
                using (ZipFile zip = ZipFile.Read(ms))
                {

                    foreach (ZipEntry entry in zip.Entries)
                    {
                        if ((entry.FileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase)) ||
                            (entry.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase)))
                        {

                           
                            MemoryStream memoryStreamZip = new MemoryStream();
                            entry.Extract(memoryStreamZip);
                            return memoryStreamZip.ToArray();
                        }
                    }
                }
            }
            return null;
        }

        public static T BaseDeserialize<T>(string responseData)
        { 
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<T>>(responseData);
           T eInvoiceResponse = deserializerData.data;
            return eInvoiceResponse;


        }
    }
}
