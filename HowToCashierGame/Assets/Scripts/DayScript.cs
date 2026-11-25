using System;
using System.Collections;
using TMPro;
using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DayScript : MonoBehaviour
{
    public PlayerController atRegisterChecker;
    public InteractionController customerCountRetrieve;

    public TextMeshProUGUI dayTime;
    public TextMeshProUGUI overallLetterGrade;
    public TextMeshProUGUI customerRankCount;
    public TextMeshProUGUI customerRankLetter;
    public TextMeshProUGUI MrCentMessage;
    public GameObject GameOverPanel;
    public RawImage MrCentReactionImage;
    public int dayTimer = 300;

    public Texture coolImage;
    public Texture proudImage;

    public AudioClip[] voiceLines;
    public AudioSource audioSource;

    public AudioSource musicSource;

    private float Timer = 0;
    public bool isDayOver = false;
    public bool atRegister = false;
    int finalCustomerCount = 0;


    public static DayScript instance;


    // Update is called once per frame

    public void Start()
    {
        GameOverPanel.SetActive(false);
    }


    void Update()
    {
        if (atRegister)
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
        atRegister = atRegisterChecker.getIsAtRegister();

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
            GetReport();
        }
    }

    public void GetReport()
    {
        musicSource.Stop();
        finalCustomerCount = customerCountRetrieve.CustomersServed;
        StartCoroutine(UpdateReport());
    }

    private IEnumerator UpdateReport()
    {
        int finalScore = 0;
        for (int i = 1; i <= finalCustomerCount; i++)
        {
            customerRankCount.SetText("Customers Served: " + i);
            yield return new WaitForSeconds(0.3f);
        }
        if(finalCustomerCount == 0)
        {
            customerRankLetter.SetText("F");
        }
        else if(finalCustomerCount >= 1 && finalCustomerCount <= 2)
        {
            customerRankLetter.SetText("D");
            finalScore += 1;
        }
        else if(finalCustomerCount >= 3 && finalCustomerCount <= 4)
        {
            customerRankLetter.SetText("C");
            finalScore += 2;
        }
        else if(finalCustomerCount >= 5 && finalCustomerCount <= 7)
        {
            customerRankLetter.SetText("B");
            finalScore += 3;
        }
        else if(finalCustomerCount >= 8 && finalCustomerCount <= 10)
        {
            customerRankLetter.SetText("A");
            finalScore += 4;
        }
        else
        {
            customerRankLetter.SetText("S");
            finalScore += 5;
        }

        overallLetterGrade.SetText("Rank:");

        for (int i = 0; i <= finalScore; i++)
        {
            switch (i)
            {
                case 0: overallLetterGrade.SetText("Rank: F"); break;
                case 1: overallLetterGrade.SetText("Rank: D"); break;
                case 2: overallLetterGrade.SetText("Rank: C"); break;
                case 3: overallLetterGrade.SetText("Rank: B"); break;
                case 4: overallLetterGrade.SetText("Rank: A"); break;
                case 5: overallLetterGrade.SetText("Rank: S"); break;
            }

            yield return new WaitForSeconds(0.5f);
        }

        if(overallLetterGrade.text == "Rank: F")
        {
            MrCentMessage.SetText("Not even one customer served? Ouch.");
            audioSource.clip = voiceLines[0];
            audioSource.Play();
        }

        else if(overallLetterGrade.text == "Rank: D")
        {
            MrCentMessage.SetText("Not a great day.");
            audioSource.clip = voiceLines[1];
            audioSource.Play();
        }

        else if (overallLetterGrade.text == "Rank: C" || overallLetterGrade.text == "Rank: B")
        {
            MrCentReactionImage.texture = coolImage;
            if(overallLetterGrade.text == "Rank: C")
            {
                MrCentMessage.SetText("Not bad! Not bad at all!");
                audioSource.clip = voiceLines[2];
                audioSource.Play();
            }
            else
            {
                MrCentMessage.SetText("Good going employee!");
                audioSource.clip = voiceLines[3];
                audioSource.Play();
            }
        }

        else if (overallLetterGrade.text == "Rank: A" || overallLetterGrade.text == "Rank: S")
        {
            MrCentReactionImage.texture = proudImage;
            if(overallLetterGrade.text == "A")
            {
                MrCentMessage.SetText("Way to go superstar!");
                audioSource.clip = voiceLines[4];
                audioSource.Play();
            }
            else
            {
                audioSource.clip = voiceLines[5];
                audioSource.Play();
                MrCentMessage.SetText("You've made this currency so proud!");
            }
        } 

    }

    public bool GetIsDayOver()
    {
        return isDayOver;
    }
}
