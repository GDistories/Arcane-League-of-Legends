using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMananger : MonoBehaviour
{
    public static AudioSource audioSrc;

    public static AudioClip pickCoin;
    
    // public static AudioClip 
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        pickCoin = Resources.Load<AudioClip>("PickCoin");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlayAudioOneShot(AudioClip audioClip)
    {
        audioSrc.PlayOneShot(audioClip);
    }
}
