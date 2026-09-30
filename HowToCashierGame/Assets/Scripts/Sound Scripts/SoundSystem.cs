using UnityEngine;
using UnityEngine.UI;

public class SoundSystem : MonoBehaviour
{

    public Slider MusicVolumeSlider;
    public Slider MasterVolumeSlider;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckMasterSliderValue();
        CheckMusicSliderVolume();
    }

    public float CheckMusicSliderVolume()
    {
        return MusicVolumeSlider.value;
    }
    public float CheckMasterSliderValue()
    {
        return MasterVolumeSlider.value;
    }
}
