using TMPro;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    public TextMeshProUGUI pausedText;
    public bool isPaused;

    private void Start()
    {
        isPaused = false;
        pausedText.alpha = 0f;
        pausedText.SetText("**Paused**");
    }

    public void ChangePauseState()
    {
        if (isPaused)
        {
            pausedText.alpha = 0f;
            isPaused = false;
            Time.timeScale = 1.0f;
            if (PlayerController.instance.currentCam == -1)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        else
        {
            pausedText.alpha = 255f;
            isPaused = true;
            Time.timeScale = 0.0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public bool GetPauseState()
    {
        return isPaused;
    }

}
