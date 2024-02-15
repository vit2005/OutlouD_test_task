using System;
using System.Collections.Generic;

[Serializable]
public class LevelFinished
{
    public int level;
    public float seconds;
}

[Serializable]
public class Database
{
    public List<LevelFinished> levels = new List<LevelFinished>();
}
