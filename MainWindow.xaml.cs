using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Collections;
using System.Text.RegularExpressions;

namespace Wallet
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();  
            openSyrius();
        }

        Syrius syrius;
        public string walletName;
        public string selectedWalletsPath;

        public string pathToCache = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\znn";
        public Dictionary<string, string> paths = new Dictionary<string, string>();

        private void changeWalletBtn(object sender, RoutedEventArgs e) //Обработать исключение. Надо выводить сообщение юзеру, что не выбран номер кошельке в listbox
        {
            try {walletName = fileListBox.SelectedItem.ToString();
           paths = syrius.dataDic;
           syrius.MoveDirectory(@paths[walletName], pathToCache, true);
           Process.Start(@"D:\Syrius\Syrius.exe"); // Придумать универсальный путь к exe
            }
            catch {System.Windows.MessageBox.Show("123"); }
           
        }
        
        private void fileListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("Syrius");
            foreach (Process i in processes)
            {
                i.Kill();
                i.WaitForExit();
            }
            syrius.MoveDirectory(pathToCache, @paths[walletName], true);
        }

        private void addWalletsBtn_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            try
            {
                if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    selectedWalletsPath = Directory.GetDirectories(folderBrowser.SelectedPath)[0];
                    fileListBox.Items.Clear();
                    syrius = new Syrius(selectedWalletsPath);
                    syrius.addListBoxItems(fileListBox);
                }
            }

            catch (Exception)
            {
                System.Windows.MessageBox.Show("Выберите папку с кошельками!");
            }
        }

        public string[] getName(DriveInfo[] drives)
        {
            string[] names = new string[drives.Length];
            for (int i = 0; i < names.Length; i++)
                names[i] = drives[i].Name;
            return names;
        }

        public void openSyrius()
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Windows.Forms.MessageBox.Show(ofd.FileName);
            }
        }
    }
}