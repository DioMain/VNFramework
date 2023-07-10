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
    public AchievementManager Achievements;

    public ConfigInfo GameConfig;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            GameConfig = SaveLoad.GetConfig();
            

            DontDestroyOnLoad(gameObject);
        }
        else 
            Destroy(gameObject);
    }

    private void Start()
    {
        if (Instance == this)
        {
            ApplyConfig();
        }
    }

    public void ApplyConfig()
    {
        Audio.ApplyConfig();

        RefreshRate refresh = Screen.resolutions.Where(i => i.refreshRateRatio.value == GameConfig.RefreshRate).ToArray()[0].refreshRateRatio;

        Screen.SetResolution(GameConfig.Resolution.x, GameConfig.Resolution.y, 
                                GameConfig.IsFullScreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed, 
                                refresh);
    }
}