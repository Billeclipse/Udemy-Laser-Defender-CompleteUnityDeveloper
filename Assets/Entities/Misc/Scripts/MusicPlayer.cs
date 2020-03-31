using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	private AudioSource audioSource;

	private void Awake ()
	{
		audioSource = GetComponent<AudioSource>();

		SetUpSingleton();
		SetDefaults();
	}

	private void SetUpSingleton()
	{
		if(FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}

	public void SetVolume(float volume)
	{
		audioSource.volume = volume;
	}

	public void SetDefaults()
	{
		SetVolume(PlayerPrefsManager.GetMasterVolume());
	}
}
