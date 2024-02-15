using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStorage
{
    void Save(Database database);
    Database Load();
}
