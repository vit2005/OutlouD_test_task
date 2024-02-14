using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameplayGameMode : GameModeBase
{
    private GameplayController controller;
    public override GameModeId id => GameModeId.GAMEPLAY;

    public override void Init()
    {
        controller = AppController.Instance.transform.GetComponentInChildren<GameplayController>(true); ;
    }

    public override void OnEnter()
    {
        controller.InitTable(2);
        AppController.Instance.input.Click += controller.OnClick;
        AppController.Instance.input.Escape += OnEscape;
    }

    private void OnEscape()
    {
        AppController.Instance.modes.SetGameMode(GameModeId.MENU);
    }

    public override void OnExit()
    {
        controller.ClearTable();
        AppController.Instance.input.Click -= controller.OnClick;
        AppController.Instance.input.Escape -= OnEscape;
    }
}
