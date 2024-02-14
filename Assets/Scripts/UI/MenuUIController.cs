using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIController : MonoBehaviour
{
    public void OnStartClick()
    {
        AppController.Instance.modes.SetGameMode(GameModeId.GAMEPLAY);
    }
}
