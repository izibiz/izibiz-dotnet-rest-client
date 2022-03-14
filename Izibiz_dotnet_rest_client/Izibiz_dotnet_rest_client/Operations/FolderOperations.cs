using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace Izibiz_dotnet_rest_client.Operations
{
    public class FolderOperations
    {
        public static string FilePath(string type)
        {
            string folderpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (type == nameof(EI.Type.EInvoice))
            {
                return string.Format("{0}{1}", folderpath, @"\EFatura\"); ;
            }
            else if (type == nameof(EI.Type.EArchive))
            {
                return string.Format("{0}{1}", folderpath, @"\ArşivFatura\");
            }
            else if (type == nameof(EI.Type.EDespatch))
            {
                return string.Format("{0}{1}", folderpath, @"\EIrsaliye\");
            }
            else if (type == nameof(EI.Type.EDespatchReceipt))
            {
                return string.Format("{0}{1}", folderpath, @"\EIrsaliyeYanıtı\");
            }
            else if (type == nameof(EI.Type.CreditNote))
            {
                return string.Format("{0}{1}", folderpath, @"\Müstahsil\");
            }
            else if (type == nameof(EI.Type.ESmm))
            {
                return string.Format("{0}{1}", folderpath, @"\Smm\");
            }
            else if (type == nameof(EI.Type.ECurrency))
            {
                return string.Format("{0}{1}", folderpath, @"\EDöviz\");
            }

            return null;
        }
        public static void FileYesNo(String path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void SaveToDisk(string type, string documentType, Dictionary<string, byte[]> text)
        {
            var filePath = FilePath(type);

            FileYesNo(filePath);

            foreach (var invoice in text)
            {
                File.WriteAllBytes(filePath + invoice.Key + "." + documentType.ToLower(), invoice.Value);
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
    }
}
