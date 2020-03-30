using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class HealthKeeper : MonoBehaviour
{
	[SerializeField] float offsetY = 0.5f;

	private float maxHealth = 200;

	private PlayerController playerController;
	private TextMeshProUGUI healthText;
	private Slider healthSlider;	
	private FillColor fillColor;	

	private void Start()
	{
		playerController = FindObjectOfType<PlayerController>();
		healthSlider = GetComponent<Slider>();
		healthText = GetComponentInChildren<TextMeshProUGUI>();
		fillColor = GetComponentInChildren<FillColor>();
		Reset();
	}

	private void FixedUpdate()
	{
		float playerPositionX = playerController.transform.position.x;
		float playerPositionY = playerController.transform.position.y;
		transform.position = new Vector3 (playerPositionX, playerPositionY - offsetY, transform.position.z);
	}

	public void DisplayHealth()
	{
		int healthInPercentage = (int)(playerController.GetHealth() * 100 / maxHealth);

		if(healthInPercentage <= 0)
		{
			Destroy(gameObject);
		}

		switch (healthInPercentage)
		{
			case int n when (n >= 75): 
				fillColor.SetColor(new Color32(89, 245, 95, 255)); 
				break;
			case int n when (n >= 50):
				fillColor.SetColor(new Color32(248, 219, 106, 255));
				break;
			case int n when (n >= 1):
				fillColor.SetColor(new Color32(251, 65, 53, 255));
				break;
			default:
				fillColor.SetColor(new Color32(0, 0, 0, 255));
				break;
		}
		healthText.text = healthInPercentage.ToString() + "%";
		healthSlider.value = healthInPercentage;
	}

	public void Reset()
	{
		maxHealth = playerController.GetHealth();
		fillColor.SetColor(new Color32(89, 245, 95, 255));
		DisplayHealth();
	}
}
