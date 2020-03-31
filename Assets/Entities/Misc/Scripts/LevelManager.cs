using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name){
		SceneManager.LoadScene(name);
	}

	public void QuitRequest(){
		Application.Quit ();
	}

	public void LoadStartMenu()
	{
		SceneManager.LoadScene(0);
	}

	public void LoadNextLevel()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

		SceneManager.LoadScene(currentSceneIndex + 1);
	}
}
