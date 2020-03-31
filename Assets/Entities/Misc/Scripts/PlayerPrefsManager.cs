using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager {
	const string MASTER_VOLUME_KEY = "master_volume";
	const string HIGH_SCORE = "high_score";

	const float DEFAULT_MASTER_VOLUME = 0.35f;

	public static void SavePreferences()
	{
		PlayerPrefs.Save();
	}

	public static void SetMasterVolume(float volume)
	{
		if (volume > 0f && volume < 1f)
		{
			PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
		}
		else
		{
			Debug.LogError("Master volume out of range!");
		}
	}

	public static float GetMasterVolume()
	{
		if (!PlayerPrefs.HasKey(MASTER_VOLUME_KEY))
		{ 
			PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, DEFAULT_MASTER_VOLUME);
		}
		return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
	}

	public static void SetHighScore(int high_score)
	{
		if(high_score >= GetHighScore())
		{
			PlayerPrefs.SetInt(HIGH_SCORE, high_score);
		}		
	}

	public static int GetHighScore()
	{
		if (!PlayerPrefs.HasKey(HIGH_SCORE))
		{
			return -1;
		}
		else
		{
			return PlayerPrefs.GetInt(HIGH_SCORE);
		}					
	}

	public static void ResetHighScore()
	{
		PlayerPrefs.DeleteKey(HIGH_SCORE);
	}
}
