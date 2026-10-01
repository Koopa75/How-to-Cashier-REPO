using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    //THIS SCRIPT WILL BE FOR READING THE VALUES OF GAME SETTINGS AND USING GETTER FOR NECCESSARY SCRIPT TO GET THAT INFO

    public Slider mouseSenseSlider;
    //Shaders
    //Resolution (Maybe Have that as a main menu setting)


    void Start()
    {

    }

    public float GetMouseSensitivity()
    {
        return mouseSenseSlider.value;
    }
}
