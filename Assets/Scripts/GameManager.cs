using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameDataManager Data;
    public AudioManager Audio;
    public SaveLoadManager SaveLoad;
    public LoadingManager Loading;
    public GameMenuManager GameMenu;

    public ConfigInfo GameConfig;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            GameConfig = SaveLoad.GetConfig();
            ApplyConfig();

            DontDestroyOnLoad(gameObject);
        }
        else 
            Destroy(gameObject);
    }

    public void ApplyConfig()
    {
        Audio.ApplyConfig();

        Screen.SetResolution(GameConfig.Resolution.x, GameConfig.Resolution.y, GameConfig.IsFullScreen);
    }
}