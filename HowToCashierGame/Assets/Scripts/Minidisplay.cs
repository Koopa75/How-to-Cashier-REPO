using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Minidisplay : MonoBehaviour
{
    public void SetValue(int total)
    {
        string str = total.ToString();
        while (str.Length < 3) str = "0" + str;
        str = str.Insert(str.Length - 2, ".");
        str = "$" + str;
        text.text = str;
    }

    public TextMeshProUGUI text;
}
