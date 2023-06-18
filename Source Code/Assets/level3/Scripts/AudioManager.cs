using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource efxSource;
    public AudioSource BGSource;
    public static AudioManager instance = null;
    public bool randomizePitch = false;
    public float lowPitchRnge = .95f;
    public float highPitchRange = 1.05f;
    //private static AudioManager instance = null;
    public static AudioManager Instance{
        get { return instance;}
    }
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null){
            instance = this;
        }
        else if(instance != this){
            Destroy(instance.gameObject);
            instance = this;
        }
        
    }
    
    public void setSfx(AudioClip soundClip, float volume = 1f){
        if (soundClip == null)
            soundClip = Resources.Load<AudioClip>("noSoundSfx"); //safety code to run a soundSfx of nothing if the script that uses setSfx() passed an AudioClip with null
        if (randomizePitch)
        {
            float randomPitch = Random.Range(lowPitchRnge, highPitchRange);
            efxSource.pitch = lowPitchRnge;    
        }
        efxSource.clip = soundClip;
        efxSource.volume = volume;
        efxSource.Play();
    }
    public void stopSfx(){
        efxSource.Stop();
    }
    public void setBG(AudioClip bg)
    {
        BGSource.Stop();
        BGSource.clip = bg;
        Debug.Log("Playing");
        BGSource.Play();
    }
    public void stopBG()
    {
        BGSource.Stop();
    }
}
