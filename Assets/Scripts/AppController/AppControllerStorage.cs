using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class AppControllerStorage
{
    [Inject] IStorage storage;
    public Database data;

    public void Init()
    {
        data = storage.Load();
    }

    public int GetLevel()
    {
        if (data.levels.Count == 0) return 0;
        return data.levels.Max(x => x.level);
    }

    public void SaveLevel(float seconds)
    {
        data.levels.Add(new LevelFinished() { level = GetLevel()+1, seconds = seconds });
        storage.Save(data);
    }
}
