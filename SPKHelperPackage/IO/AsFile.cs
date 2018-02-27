using System;
using System.IO;

namespace SPKHelperPackage.IO
{
    public interface IAsFile
    {
        byte[] ReadToByteArray(string fileName);

        bool Delete(string fileName);

        void WriteFile(string fileName, string content);
    }

    public class AsFile : IAsFile
    {
        public bool Delete(string fileName)
        {
            try
            {
                File.Delete(fileName);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public byte[] ReadToByteArray(string fileName)
        {
            return toByteArray(fileName);
        }

        public void WriteFile(string fileName, string content)
        {
            using (StreamWriter sw = new StreamWriter(
             new FileStream(fileName, FileMode.Append)))
            {
                try
                {
                    sw.WriteLine(content);
                }
                catch { }
            }
        }

        private byte[] toByteArray(string fileName)
        {
            byte[] buffer = null;

            try
            {
                using (FileStream fs = File.OpenRead(fileName))
                {
                    using (BinaryReader binaryReader = new BinaryReader(fs))
                    {
                        buffer = binaryReader.ReadBytes((int)fs.Length);
                    }
                }
            }
            catch { }

            return buffer;
        }
    }
}