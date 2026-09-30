using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class MenuTraversalSystem : MonoBehaviour
{
    public PauseSystem pauseState;

    public GameObject PlayerInfoPanel;
    public GameObject SettingPanel;
    public GameObject MusicPanel;
    public GameObject MenuPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public void Start()
    {
        PlayerInfoPanel.SetActive(false);
        SettingPanel.SetActive(false);
        MusicPanel.SetActive(false);
    }
    public void OpenSettingsPanel()
    {
        SettingPanel.SetActive(true);
        MenuPanel.SetActive(false);
        pauseState.ChangePauseState();
    }
    public void OpenMusicPanel()
    {
        MusicPanel.SetActive(true);
        MenuPanel.SetActive(false);
        pauseState.ChangePauseState();
    }
    
    public void OpenPlayerInfoPanel()
    {
        PlayerInfoPanel.SetActive(true);
        MenuPanel.SetActive(false);
        pauseState.ChangePauseState();
    }

    public void CloseOpenPanel()
    {
        SettingPanel.SetActive(false);
        MusicPanel.SetActive(false);
        PlayerInfoPanel.SetActive(false);
        MenuPanel.SetActive(true);
        pauseState.ChangePauseState();
    }
}
