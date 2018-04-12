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
	
	private ScoreKeeper scoreKeeper;
	
	void Start(){
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();		
	}	
	
	void OnTriggerEnter2D(Collider2D collider){
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile){
			health -= missile.GetDamage();
			missile.Hit ();
			if(health <= 0){
				Die();
			}
		}
	}
	
	void Die(){
		AudioSource.PlayClipAtPoint(deathSound,transform.position);
		Destroy(gameObject);
		scoreKeeper.Score(scoreValue);	
	}
	
	void Fire(){		
		GameObject beam = Instantiate(projectile, new Vector3(transform.position.x,transform.position.y - 0.5f,transform.position.z), Quaternion.identity) as GameObject;		
		beam.rigidbody2D.velocity = new Vector3(0,-projectileSpeed,0);
		AudioSource.PlayClipAtPoint(fireSound,transform.position);
	}
	
	void Update(){
		float probability = Time.deltaTime * shotsPerSeconds;
		if(Random.value < probability){
			Fire();
		}
	}
}
