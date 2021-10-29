using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;

namespace Wallet
{
    class Syrius
    {
        public Dictionary<string, string> dataDic = new Dictionary<string, string>();
        
        public Syrius(string walletsPath) 
        {
            addDictItems(getWalletPaths(@walletsPath));
        }

        public bool MoveDirectory(string sourceDirName, string destDirName, bool overwrite)
        {
            if (overwrite && Directory.Exists(destDirName))
            {
                var needRestore = false;
                var tmpDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                try
                {
                    Directory.Move(destDirName, tmpDir);
                    needRestore = true;
                    Directory.Move(sourceDirName, destDirName);
                    return true;
                }
                catch (Exception)
                {
                    if (needRestore)
                    {
                        Directory.Move(tmpDir, destDirName);
                    }
                }
            }
            else
            {
                Directory.Move(sourceDirName, destDirName);
                return true;
            }
            return false;
        }

        public string[] getWalletPaths(string path)
        {
            string[] folders = Directory.GetDirectories(path);
            int maxLen = folders.Max(x => x.Length);
            var result = folders.OrderBy(x => x.PadLeft(maxLen, '0'));
            return result.ToArray();
        }

        public void addDictItems(string[] filePaths) 
        {
            for (int i = 0; i < filePaths.Length; i++)
                dataDic.Add((i+1).ToString() , filePaths[i]);
        }

        public void addListBoxItems(ListBox sample)
        {
            for (int i = 0; i < dataDic.Keys.Count; i++)
                sample.Items.Add(dataDic.ElementAt(i).Key);
        }
    }
}
