using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalLoder : MonoBehaviour
{
    public void LoadMainMenuScene() => GameManager.Instance.Loading.LoadMainMenuScene();

    public void LoadGameScene(int slotid = -1) => GameManager.Instance.Loading.LoadGameScene(slotid);
}
