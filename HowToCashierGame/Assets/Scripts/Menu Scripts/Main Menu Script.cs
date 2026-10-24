using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public GameObject MainMenuPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        MainMenuPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSaveGame()
    {

    }



    public void LoadNewGame()
    {
        SceneManager.LoadScene("Scene 1");
    }


}
