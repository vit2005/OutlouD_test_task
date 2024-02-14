using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameModeBase
{

    public abstract GameModeId id { get; }

    public virtual void Init() { }

    public virtual void OnEnter() { }

    public virtual void OnUpdate() { }

    public virtual void OnExit() { }

}
