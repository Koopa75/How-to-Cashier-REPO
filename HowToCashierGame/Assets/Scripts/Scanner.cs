using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Experimental.GlobalIllumination;
using System.Collections;

public class Scanner : MonoBehaviour
{
    public float targetIntensity = 0.01f;
    private float lightFadeDuration = 0.5f;

    public AudioSource scanSound;
    public SoundSystem scannerSystem;
    public Light scannerLight;

    public void Start()
    {
        scannerLight.intensity = 0.01f;
    }
    public void Update()
    {
    }
    public void OnTriggerEnter(Collider other)
    {
        PhysicsItem item = other.gameObject.GetComponent<PhysicsItem>();
        if (item != null)
        {
            if (!item.item.scanned)
            {
                if (item.Scan())
                {
                    scannerLight.intensity = 0.2f;
                    StartCoroutine(FadeScannerLightBack());
                    scanSound.volume = scannerSystem.CheckSoundEffectSliderValue() * scannerSystem.CheckMasterSliderValue();
                    scanSound.time = 0.7f;
                    scanSound.Play();
                }
                else
                {
                    // Fail to scan sound effect
                }
            }
        }
    }
    IEnumerator FadeScannerLightBack() 
    { 
        float currentIntesntiy = scannerLight.intensity;
        float totalTime = 0f;

        while (totalTime < lightFadeDuration)
        {
            float current = Mathf.Lerp(currentIntesntiy, targetIntensity, totalTime / lightFadeDuration);
            scannerLight.intensity = current;
            yield return new WaitForSeconds(0.02f);
            totalTime += 0.02f;
        }


    }


}
