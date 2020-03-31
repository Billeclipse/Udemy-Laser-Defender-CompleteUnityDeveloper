using TMPro;
using UnityEngine;

public class HighScoreMenu : MonoBehaviour {

	[SerializeField] public TextMeshProUGUI highScoreText;

	private void Update()
	{
		if(PlayerPrefsManager.GetHighScore() != -1)
		{
			highScoreText.SetText(PlayerPrefsManager.GetHighScore().ToString());
		}
		else
		{
			highScoreText.SetText("----");
		}	
	}

	public void ResetScore()
	{
		PlayerPrefsManager.ResetHighScore();
	}
}
