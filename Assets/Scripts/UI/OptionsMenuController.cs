using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenuController : MonoBehaviour
{
    [SerializeField] private Slider gameVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;

    private const string GameVolumeKey = "GameVolume";
    private const string MusicVolumeKey = "MusicVolume";

    private void Start()
    {
        float savedGameVolume = PlayerPrefs.GetFloat(GameVolumeKey, 1f);
        float savedMusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 1f);

        gameVolumeSlider.value = savedGameVolume;
        musicVolumeSlider.value = savedMusicVolume;
    }

    public void SetGameVolume(float value)
    {
        PlayerPrefs.SetFloat(GameVolumeKey, value);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat(MusicVolumeKey, value);
        PlayerPrefs.Save();
    }

    public void Back()
{
    string returnScene = PlayerPrefs.GetString("OptionsReturnScene", "HomePage");
    SceneManager.LoadScene(returnScene);
}
}