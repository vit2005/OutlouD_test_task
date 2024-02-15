using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AppController : MonoBehaviour
{
    public static AppController Instance { get; private set; }
    [SerializeField] private AppControllerModes _modes;
    public AppControllerModes modes => _modes;

    [SerializeField] private InputController _input;
    public InputController input => _input;

    [SerializeField] private Camera _camera;
    public Camera Camera => _camera;

    [Inject] AppControllerStorage _storage;
    public AppControllerStorage storage => _storage;

    public void Awake()
    {
        Instance = this;
        _modes.Init();
        _modes.SetGameMode(GameModeId.PRELOAD);
        _input.Init();
        _storage.Init();
    }
}
