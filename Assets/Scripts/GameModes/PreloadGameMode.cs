using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreloadGameMode : GameModeBase
{
    public override GameModeId id => GameModeId.PRELOAD;

    public override void OnEnter()
    {
        AppController.Instance.input.PressedAnyKey += PressedAnyKey;
    }

    public override void OnExit()
    {
        AppController.Instance.input.PressedAnyKey -= PressedAnyKey;
    }

    private void PressedAnyKey()
    {
        AppController.Instance.modes.SetGameMode(GameModeId.MENU);
    }
}
