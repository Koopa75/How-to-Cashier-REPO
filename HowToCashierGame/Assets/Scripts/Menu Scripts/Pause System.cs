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
        }
        else
        {
            pausedText.alpha = 255f;
            isPaused = true;
            Time.timeScale = 0.0f;
        }
    }

    public bool GetPauseState()
    {
        return isPaused;
    }

}
