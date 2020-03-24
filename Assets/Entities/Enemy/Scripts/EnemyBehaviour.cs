using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	
	public GameObject projectile;
	public float health = 150f;
	public float projectileSpeed = 5f;
	public float shotsPerSeconds = 0.5f;
	public int scoreValue = 150;
	public AudioClip fireSound;
	public AudioClip deathSound;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		DamageDealer damageDealer = collider.gameObject.GetComponent<DamageDealer>();
		GetDamaged(damageDealer);
	}

	private void GetDamaged(DamageDealer damageDealer)
	{
		health -= damageDealer.GetDamage();
		damageDealer.Hit();
		if (health <= 0)
		{
			Die();
		}
	}

	void Die(){
		AudioSource.PlayClipAtPoint(deathSound,transform.position);
		Destroy(gameObject);
		ScoreKeeper.Score(scoreValue);	
	}
	
	void Fire(){		
		GameObject beam = Instantiate(projectile, new Vector3(transform.position.x,transform.position.y - 0.5f,transform.position.z), Quaternion.identity) as GameObject;		
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0,-projectileSpeed,0);
		AudioSource.PlayClipAtPoint(fireSound,transform.position);
	}
	
	void Update(){
		float probability = Time.deltaTime * shotsPerSeconds;
		if(Random.value < probability){
			Fire();
		}
	}
}
