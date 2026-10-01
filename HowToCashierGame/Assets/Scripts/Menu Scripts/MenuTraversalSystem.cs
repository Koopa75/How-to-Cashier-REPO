using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class MenuTraversalSystem : MonoBehaviour
{
    public PauseSystem pauseState;

    public GameObject PlayerInfoPanel;
    public GameObject SettingPanel;
    public GameObject MusicPanel;
    public GameObject MenuPanel;

    private bool isAnyPanelOpen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public void Start()
    {
        isAnyPanelOpen = false;
        PlayerInfoPanel.SetActive(false);
        SettingPanel.SetActive(false);
        MusicPanel.SetActive(false);
    }
    public void OpenSettingsPanel()
    {
        isAnyPanelOpen = true;
        SettingPanel.SetActive(true);
        MenuPanel.SetActive(false);
        pauseState.ChangePauseState();
    }
    public void OpenMusicPanel()
    {
        isAnyPanelOpen = true;
        MusicPanel.SetActive(true);
        MenuPanel.SetActive(false);
        pauseState.ChangePauseState();
    }
    
    public void OpenPlayerInfoPanel()
    {
        isAnyPanelOpen = true;
        PlayerInfoPanel.SetActive(true);
        MenuPanel.SetActive(false);
        pauseState.ChangePauseState();
    }

    public void CloseOpenPanel()
    {
        isAnyPanelOpen = false;
        SettingPanel.SetActive(false);
        MusicPanel.SetActive(false);
        PlayerInfoPanel.SetActive(false);
        MenuPanel.SetActive(true);
        pauseState.ChangePauseState();
    }

    public bool CheckAnyOpenSettingPanelState()
    {
        return isAnyPanelOpen;
    }
}
