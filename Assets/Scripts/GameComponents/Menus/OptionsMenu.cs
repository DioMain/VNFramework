using System.Collections;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : GameMenuBase
{
    [SerializeField]
    private List<Vector2Int> resolutions = new List<Vector2Int>();

    [SerializeField]
    private TMP_Dropdown resolutionDropdown;
    [SerializeField]
    private Toggle fullscreenToggle;

    [SerializeField]
    private Slider BGMSlider;
    [SerializeField]
    private Slider BGSSlider;
    [SerializeField]
    private Slider SESlider;

    private void Start()
    {
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        foreach (var item in Screen.resolutions.Reverse())
        {
            resolutions.Add(new Vector2Int(item.width, item.height));
            options.Add($"{item.width}x{item.height} : {Mathf.RoundToInt((float)item.refreshRateRatio.value)}x");
        }

        resolutionDropdown.AddOptions(options);

        for (var i = 0; i < resolutions.Count; i++)
        {
            if (resolutions[i] == GameManager.Instance.GameConfig.Resolution)
            {
                resolutionDropdown.value = i;
                break;
            }
        }

        fullscreenToggle.isOn = GameManager.Instance.GameConfig.IsFullScreen;

        BGMSlider.value = GameManager.Instance.GameConfig.BGMVolume;
        BGSSlider.value = GameManager.Instance.GameConfig.BGSVolume;
        SESlider.value = GameManager.Instance.GameConfig.SEVolume;

        Disactivate();
    }

    public void Apply()
    {
        GameManager.Instance.GameConfig.BGMVolume = BGMSlider.value;
        GameManager.Instance.GameConfig.BGSVolume = BGSSlider.value;
        GameManager.Instance.GameConfig.SEVolume = SESlider.value;

        GameManager.Instance.GameConfig.Resolution = resolutions[resolutionDropdown.value];
        GameManager.Instance.GameConfig.IsFullScreen = fullscreenToggle.isOn;

        GameManager.Instance.SaveLoad.SaveConfig(GameManager.Instance.GameConfig);

        GameManager.Instance.ApplyConfig();
    }
}
