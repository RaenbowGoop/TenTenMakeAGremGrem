using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class JsonDataService : IDataService
{
    public bool SaveData<T>(string relativePath, T data)
    {
        // Get path you want to write to
        string path = Application.persistentDataPath + relativePath;

        try {
            // If file already exists, delete it and write a new file
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            // Open and close stream to create file
            using FileStream stream = File.Create(path);
            stream.Close();

            // Write to file (BIG MAGIC)
            File.WriteAllText(path, JsonConvert.SerializeObject(data, Formatting.Indented));

            // return completion status
            return true;

        } catch (Exception e) { 
            Debug.LogError($"Unable to save data due to: {e.Message} {e.StackTrace}");
            return false;
        }

    }

    public T LoadData<T>(string relativePath)
    {
        // Get path you want to read/load from
        string path = Application.persistentDataPath + relativePath;

        // If file already exists, delete it and write a new file
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"{path} does not exist!");
        }

        try
        {
            // Read and deserialize data + return it
            T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            return data;
        } catch (Exception e)
        {
            Debug.LogError($"Failed to load data due to: {e.Message} {e.StackTrace}");
            return default(T);
        }
    }
}
