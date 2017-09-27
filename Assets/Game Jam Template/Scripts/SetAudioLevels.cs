using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetAudioLevels : MonoBehaviour {

	public AudioMixer mainMixer; //Used to hold a reference to the AudioMixer mainMixer
    public static float sfxVolume = 0.1f;


	//Call this function and pass in the float parameter musicLvl to set the volume of the AudioMixerGroup Music in mainMixer
	public void SetMusicLevel(float musicLvl)
	{
		mainMixer.SetFloat("musicVol", musicLvl);
        GameObject mCamera = GameObject.Find("Main Camera");

		if (mCamera.GetComponent<AudioSource>() == null)
		{
			mCamera.AddComponent<AudioSource>();
		}
		
        AudioSource audio = mCamera.GetComponent<AudioSource>();
        Debug.Log("SetMusicLevel = " + musicLvl);
        audio.volume = musicLvl;
    }

	//Call this function and pass in the float parameter sfxLevel to set the volume of the AudioMixerGroup SoundFx in mainMixer
	public void SetSfxLevel(float sfxLevel)
	{
		mainMixer.SetFloat("sfxVol", sfxLevel);
        sfxVolume = sfxLevel;

    }
}
