using UnityEngine;
using UnityEngine.UI;

public class MainMusicScript : MonoBehaviour
{

    public AudioSource MainMusic;
    public SoundSystem MainMusicSliderVolume;



    // Update is called once per frame
    void Update()
    {
        CheckMusicVolume();
    }

    public void CheckMusicVolume()
    {
        MainMusic.volume = MainMusicSliderVolume.CheckMusicSliderVolume() * MainMusicSliderVolume.CheckMasterSliderValue();
    }
}
