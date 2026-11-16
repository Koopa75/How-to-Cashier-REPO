using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

public class TipManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text tipNameText;
    public TMP_Text descriptionText;

    [Header("Video")]
    public VideoPlayer videoPlayer;

    [Header("Tips")]
    public TipData[] tips;

    private int currentTipIndex = 0;

    private void Start()
    {
        DisplayTip(currentTipIndex);
    }

    public void NextTip()
    {
        currentTipIndex++;

        if (currentTipIndex >= tips.Length)
            currentTipIndex = 0;

        DisplayTip(currentTipIndex);
    }

    public void PreviousTip()
    {
        currentTipIndex--;

        if (currentTipIndex < 0)
            currentTipIndex = tips.Length - 1;

        DisplayTip(currentTipIndex);
    }

    private void DisplayTip(int index)
    {
        TipData tip = tips[index];

        tipNameText.text = tip.tipName;
        descriptionText.text = tip.description;

        if (tip.videoClip != null)
        {
            videoPlayer.clip = tip.videoClip;
            videoPlayer.Play();
        }
    }
}