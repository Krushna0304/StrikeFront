using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mainMixer;
    public float unit = 1;
    private float volume;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Ok");
            IncreaseEventSFX();
        }
    }
    public void IncreaseVolume()
    {
        mainMixer.GetFloat("MasterVolume",out volume);
        volume += unit;
        mainMixer.SetFloat("MasterVolume", volume);
    }
    public void DecreaseVolume()
    {

        mainMixer.GetFloat("MasterVolume", out volume);
        volume -= unit;
        mainMixer.SetFloat("MasterVolume", volume);
    }
    public void IncreaseMusic()
    {

        mainMixer.GetFloat("MasterVolume", out volume);
        volume += unit;
        mainMixer.SetFloat("MusicVolume", volume);
    }
    public void DeacreaseMusic()
    {

        mainMixer.GetFloat("MasterVolume", out volume);
        volume -= unit;
        mainMixer.SetFloat("MusicVolume", volume);
    }
    public void IncreaseUISFX()
    {

        mainMixer.GetFloat("MasterVolume", out volume);
        volume += unit;
        mainMixer.SetFloat("UISFXVolume", volume);
    }
    public void DeacreaseUISFX()
    {

        mainMixer.GetFloat("MasterVolume", out volume);
        volume -= unit;
        mainMixer.SetFloat("UISFXVolume", volume);
    }
    public void IncreaseEventSFX()
    {

        mainMixer.GetFloat("EventSFXVolume", out volume);;
        volume += unit;
        mainMixer.SetFloat("EventSFXVolume", volume);
    }
    public void DeacreaseEventSFX()
    {

        mainMixer.GetFloat("MasterVolume", out volume);
        volume -= unit;
        mainMixer.SetFloat("EventSFXVolume", volume);
    }
}
