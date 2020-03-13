using UnityEngine;
using System.Collections;
using TMPro;

public class ScoreKeeper : MonoBehaviour {

	public static int score = 0;
	private TextMeshProUGUI myText;
	
	void Start(){
		myText = GetComponent<TextMeshProUGUI>();
		Reset();
	}

	public void Score(int points){
		score += points;
		myText.text = "Score: " + score.ToString();
	}
	
	public static void Reset(){
		score = 0;
	}
	
}
