using UnityEngine;
using System.Collections;
using TMPro;

public class ScoreKeeper : MonoBehaviour {

	private static int score = 0;
	private static TextMeshProUGUI myText;
	
	void Start(){
		myText = GetComponent<TextMeshProUGUI>();
		Reset();
	}

	public static void Score(int points){
		score += points;
		myText.text = "Score: " + score.ToString();
	}
	
	public static void Reset(){
		score = 0;
	}

	public static int GetScore()
	{
		return score;
	}
	
}
