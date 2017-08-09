using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource fxSound;
    public AudioClip backMusic; 
                                
    void Start()
    {
        fxSound = GetComponent<AudioSource>();
        fxSound.Play();
    }
}