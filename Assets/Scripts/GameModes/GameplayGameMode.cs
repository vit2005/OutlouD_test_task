using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameplayGameMode : GameModeBase
{
    private GameplayController _controller;
    private AppControllerStorage _storage;
    public override GameModeId id => GameModeId.GAMEPLAY;

    public override void Init()
    {
        _controller = AppController.Instance.transform.GetComponentInChildren<GameplayController>(true);
        _storage = AppController.Instance.storage;
    }

    public override void OnEnter()
    {
        _controller.InitTable((_storage.GetLevel()+1)*2);
        AppController.Instance.input.Click += _controller.OnClick;
        AppController.Instance.input.Escape += OnEscape;
    }

    private void OnEscape()
    {
        AppController.Instance.modes.SetGameMode(GameModeId.MENU);
    }

    public override void OnExit()
    {
        _controller.OnExit();
        AppController.Instance.input.Click -= _controller.OnClick;
        AppController.Instance.input.Escape -= OnEscape;
    }
}
