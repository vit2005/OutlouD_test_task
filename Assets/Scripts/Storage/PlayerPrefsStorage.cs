using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsStorage : IStorage
{
    string databaseKey = "database";

    public Database Load()
    {
        Database database = new Database();
        if (PlayerPrefs.HasKey(databaseKey))
        {
            database = JsonUtility.FromJson<Database>(PlayerPrefs.GetString(databaseKey));
        }
        else
        {
            Save(database);
        }

        return database;
    }

    public void Save(Database database)
    {
        PlayerPrefs.SetString(databaseKey, JsonUtility.ToJson(database));
    }
}
