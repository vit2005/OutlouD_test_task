using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AppControllerModes : MonoBehaviour 
{

    [SerializeField] private CinemachineVirtualCamera vcam;

    [SerializeField] private Transform preloadLookAtPoint;
    [SerializeField] private Transform menuLookAtPoint;
    [SerializeField] private Transform gameplayLookAtPoint;

    private Dictionary<GameModeId, GameModeBase> _gameModes;
    public GameModeBase CurrentMode;


    public void Init()
    {
        _gameModes = new Dictionary<GameModeId, GameModeBase>() { { GameModeId.PRELOAD, new PreloadGameMode() }, 
            { GameModeId.MENU, new MenuGameMode() }, { GameModeId.GAMEPLAY, new GameplayGameMode() } };

        foreach (var gameMode in _gameModes.Values)
        {
            gameMode.Init();
        }
    }

    public void SetGameMode(GameModeId modeId)
    {
        if (CurrentMode != null) CurrentMode.OnExit();
        CurrentMode = _gameModes[modeId];
        SetCamera(modeId);
        CurrentMode.OnEnter();
        AppController.Instance.sound.Whoosh();
    }

    public void SetCamera(GameModeId modeId)
    {
        switch (modeId)
        {
            case GameModeId.PRELOAD:
                vcam.LookAt = preloadLookAtPoint;
                break;
            case GameModeId.MENU:
                vcam.LookAt = menuLookAtPoint;
                break;
            case GameModeId.GAMEPLAY:
                vcam.LookAt = gameplayLookAtPoint;
                break;
            case GameModeId.SETTINGS:
                break;
        }
    }

    public void Update()
    {
        CurrentMode?.OnUpdate();
    }
}
