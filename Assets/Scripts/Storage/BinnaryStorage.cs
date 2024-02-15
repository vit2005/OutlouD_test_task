using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Playables;

public class BinnaryStorage : IStorage
{
    string fileLocation = Application.persistentDataPath + "/database.data";
    BinaryFormatter converter = new BinaryFormatter();

    public Database Load()
    {
        Database database = new Database();
        if (File.Exists(fileLocation))
        {
            var dataStream = new FileStream(fileLocation, FileMode.Open);
            var databaseObject = converter.Deserialize(dataStream);
            if (databaseObject != null) database = (Database)databaseObject;
            dataStream.Close();
        }
        else
        {
            Save(database);
        }

        return database;
    }

    public void Save(Database database)
    {
        var dataStream = new FileStream(fileLocation, FileMode.Create);
        converter.Serialize(dataStream, database);
        dataStream.Close();
    }
}
