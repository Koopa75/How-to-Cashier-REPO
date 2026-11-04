using TMPro;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.UIElements;

public class DayScript : MonoBehaviour
{

    public TextMeshProUGUI dayTime;
    public GameObject GameOverPanel;
    public int dayTimer = 300;

    private float Timer = 0;
    public bool isDayOver = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame

    public void Start()
    {
        GameOverPanel.SetActive(false);
    }


    void Update()
    {
        if (GameState.InDialogue)
            return;

        Timer += Time.deltaTime;

        if (Timer >= 1)
        {
            dayTimer--;
            CalcTime();
            Timer = 0;
        }

        CheckIfDayIsOver();

    }

    public void CalcTime()
    {
        if (!isDayOver)
        {
            int minutes = dayTimer / 60;
            int seconds = dayTimer % 60;

            if (seconds < 10)
            {
                dayTime.SetText("Time Before Day Ends: " + minutes + ":0" + seconds);

            }
            else
            {
                dayTime.SetText("Time Before Day Ends: " + minutes + ":" + seconds);
            }
        }
    }

    
    public void CheckIfDayIsOver()
    {
        if (dayTimer == 0)
        {
            GameOverPanel.SetActive(true);
            isDayOver = true;
        }
    }
}
