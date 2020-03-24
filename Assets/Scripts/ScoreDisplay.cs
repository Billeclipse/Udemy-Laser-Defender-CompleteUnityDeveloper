using UnityEngine;
using System.Collections;
using TMPro;

public class ScoreDisplay : MonoBehaviour {

	private TextMeshProUGUI myText;

	// Use this for initialization
	void Start () {
		myText = GetComponent<TextMeshProUGUI>();
		myText.text = ScoreKeeper.GetScore().ToString();
		ScoreKeeper.Reset();
	}
}
