using UnityEngine;
using UnityEngine.UI;

public class SoundSystem : MonoBehaviour
{

    public Slider MusicVolumeSlider;
    public Slider MasterVolumeSlider;
    public Slider SoundEffectVolumeSlider;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckMasterSliderValue();
        CheckMusicSliderVolume();
        CheckSoundEffectSliderValue();
    }

    public float CheckMusicSliderVolume()
    {
        return MusicVolumeSlider.value;
    }
    public float CheckMasterSliderValue()
    {
        return MasterVolumeSlider.value;
    }
    public float CheckSoundEffectSliderValue()
    {
        return SoundEffectVolumeSlider.value;
    }
}
