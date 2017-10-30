using SendDokumenter.iis.DocumentDataReference;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendDokumenter.iis.FileHandling
{
    /// <summary>
    /// Creates an attachment from a file path
    /// </summary>
    public class FileBytes
    {
        /// <summary>
        /// Creates an attachment from a file name and a file path
        /// </summary>
        /// <param name="filename">File name. For example "this is a file.pdf"</param>
        /// <param name="filePath">a physical path</param>
        /// <returns>An object that fits the [DataMember] AttachedFile[] AttachedFiles in the [DataContract] AttachedDocuments</returns>
        public AttachedFile CreateAttachment(string filePath)
        {
            AttachedFile afile = new AttachedFile();

            afile.FileName = Path.GetFileName(filePath);

            afile.Document = Create(filePath);

            return afile;
        }

        /// <summary>
        /// Creates a byte[] from af file path
        /// </summary>
        /// <param name="path">path to physical file</param>
        /// <returns>byte[] representation of the content</returns>
        private byte[] Create(string path)
        {
            BinaryReader binReader = new BinaryReader(File.Open(path, FileMode.Open, FileAccess.Read));

            binReader.BaseStream.Position = 0;

            byte[] binFile = binReader.ReadBytes(Convert.ToInt32(binReader.BaseStream.Length));

            binReader.Close();

            return binFile;
        }

    }
}
