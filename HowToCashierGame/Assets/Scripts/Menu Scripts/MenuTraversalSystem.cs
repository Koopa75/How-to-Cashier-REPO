using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTraversalSystem : MonoBehaviour
{
    public PauseSystem pauseState;

    public GameObject PlayerInfoPanel;
    public GameObject SettingPanel;
    public GameObject MusicPanel;
    public GameObject MenuPanel;
    public GameObject MrCentPanel;

    public MenuPulloutScript MenuPullout;
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

    }
    public void OpenMusicPanel()
    {
        isAnyPanelOpen = true;
        MusicPanel.SetActive(true);
        MenuPanel.SetActive(false);

    }
    
    public void OpenPlayerInfoPanel()
    {
        isAnyPanelOpen = true;
        PlayerInfoPanel.SetActive(true);
        MenuPanel.SetActive(false);

    }

    public void OpenMrCentPanel()
    {
        isAnyPanelOpen = true;
        MrCentPanel.SetActive(true);
        MenuPanel.SetActive(false);

    }

    public void CloseOpenPanel()
    {
        isAnyPanelOpen = false;
        SettingPanel.SetActive(false);
        MusicPanel.SetActive(false);
        PlayerInfoPanel.SetActive(false);
        MrCentPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");

    }

    public bool CheckAnyOpenSettingPanelState()
    {
        return isAnyPanelOpen;
    }
}
