using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
	public static string fileExtension = ".sav";

	public static void Save(string name, object data)
	{
		// save the save data
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/" + name + fileExtension;
		FileStream stream = new FileStream(path, FileMode.Create);

		formatter.Serialize(stream, data);
		stream.Close();
    }

	public static void Delete(string name)
	{
		if (File.Exists(Application.persistentDataPath + "/" + name + fileExtension))
		{
			File.Delete(Application.persistentDataPath + "/" + name + fileExtension);
		}
	}

	public static object Load(string filename)
	{
		if (File.Exists(Application.persistentDataPath + "/" + filename + fileExtension))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(Application.persistentDataPath + "/" + filename + fileExtension, FileMode.Open);
			object data = null;

			try
			{
				data = formatter.Deserialize(stream) as object;
				stream.Dispose();
				stream.Close();
			}
			catch (Exception e)
			{
				Debug.Log(e);
			}

			return data;
		}

		return null;
	}
}