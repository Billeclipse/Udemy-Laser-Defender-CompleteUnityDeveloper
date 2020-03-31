using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsMenu : MonoBehaviour {

	[SerializeField] public Slider volumeSlider;

	private MusicPlayer musicPlayer;
	
	void Start()
	{
		musicPlayer = FindObjectOfType<MusicPlayer>();
		SetDefaultsToSlider();
	}
	
	void Update()
	{
		if (musicPlayer)
		{
			musicPlayer.SetVolume(volumeSlider.value);
		}			
	}

	public void SaveAndGoBack()
	{
		PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
		PlayerPrefsManager.SavePreferences();
	}

	public void SetDefaultsToSlider()
	{
		volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
	}
}
