using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONStorage : IStorage
{
    string fileLocation = Application.persistentDataPath + "/database.json";

    public Database Load()
    {
        Database database = new Database();
        if (File.Exists(fileLocation))
        {
            //File.Delete(fileLocation);
            string loadedFileString = File.ReadAllText(fileLocation);
            database = JsonUtility.FromJson<Database>(loadedFileString);
        }
        else
        {
            Save(database);
        }

        return database;
    }

    public void Save(Database database)
    {
        File.WriteAllText(fileLocation, JsonUtility.ToJson(database));
    }
}
