using System;
using System.Collections.Generic;

public class FileTasks
{

}

public class SavedData 
{
	public Dictionary<string, string> dataDic;
	public static string pathToSyrius;
	public string walletName;
	public string selectedWalletFolder;
	public string pathToCache = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\znn";
}