using UnityEngine;
using System.Collections;
using System.IO;
public class WriteRead : MonoBehaviour {
	FileInfo f; 
	string path;
	void Start()
	{
		path = Application.persistentDataPath + "\\" + "jsonscene.txt";
		f = new FileInfo(path);
		Screen.SetResolution(800, 600, true);
	}

	public void Save(string text)
	{
		StreamWriter w;
		if(!f.Exists)
		{
			w = f.CreateText();    
		}
		else
		{
			f.Delete();
			w = f.CreateText();
		}
		w.WriteLine(text);
		w.Close();
	}

	public string Load()
	{
		StreamReader r = File.OpenText(path);
		string info = r.ReadToEnd();
		r.Close();
		return info;
	}
}