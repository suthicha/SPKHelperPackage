using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SPKHelperPackage.IO
{
    public interface IAsDirectory
    {
        void Create(string path, string folderName);

        void Trash(string sourceFolderWithPath);

        List<FileInfo> GetFiles(string path, string ext = "*.*");
    }

    public class AsDirectory : IAsDirectory
    {
        public void Create(string path, string folderName)
        {
            try
            {
                string destFolder = Path.Combine(path, folderName);

                if (!Directory.Exists(destFolder))
                {
                    Directory.CreateDirectory(destFolder);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Trash(string sourceFolderWithPath)
        {
            try
            {
                Directory.Delete(sourceFolderWithPath, true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<FileInfo> GetFiles(string path, string ext = "*.*")
        {
            return new DirectoryInfo(path).GetFiles(ext).ToList();
        }
    }
}