using UnityEngine;
using UnityEngine.Video;

[System.Serializable]
public class TipData
{
    public string tipName;
    [TextArea(3, 10)]
    public string description;
    public VideoClip videoClip;
}