using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMusicScript : MonoBehaviour
{

    public AudioSource MainMusic;

    public AudioClip Song1;
    public AudioClip Song2;


    public SoundSystem MainMusicSliderVolume;

    public Toggle Song1Toggle;
    public Toggle Song2Toggle;
    public Toggle Song3Toggle;



    private void Start()
    {

        Song1Toggle.isOn = true;
    }



    void Update()
    {
        CheckMusicVolume();
        CheckActiveSong();
    }

    public void CheckMusicVolume()
    {
        MainMusic.volume = MainMusicSliderVolume.CheckMusicSliderVolume() * MainMusicSliderVolume.CheckMasterSliderValue();
    }

    
    public void CheckActiveSong()
    {
        if (Song1Toggle.isOn)
        {
            PlaySong(Song1);
        }
        else if (Song2Toggle.isOn)
        {
            PlaySong(Song2);
        }
        //Currently Playes Song 2 Until Another is Added

        else if (Song3Toggle.isOn)
        {
            PlaySong(Song2);
        }
    }
    

    private void PlaySong(AudioClip clip)
    {
        if (MainMusic.clip == clip) return;

        MainMusic.clip = clip;
        MainMusic.Play();
    }
}
