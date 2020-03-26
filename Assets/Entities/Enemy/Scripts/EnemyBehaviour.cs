using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	[Header("Enemy")]
	[SerializeField] float health = 150f;
	[SerializeField] int scoreValue = 150;

	[Header("Projectile")]
	[SerializeField] public GameObject projectilePrefab;
	[SerializeField] float projectileSpeed = 5f;
	[SerializeField] float minTimeBetweenShots = 0.5f;
	[SerializeField] float maxTimeBetweenShots = 1.5f;

	[Header("Fire Settings")]
	[SerializeField] public AudioClip fireSound;
	[Range(0f, 1f)] [SerializeField] public float fireSoundVolume = 0.2f;

	[Header("Death Settings")]
	[SerializeField] public AudioClip deathSound;
	[Range(0f, 1f)] [SerializeField] public float deathSoundVolume = 0.2f;
	[SerializeField] public GameObject explosionEffectPrefab;

	private float shotCounter;

	private void Start()
	{
		shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
	}

	private void Update()
	{
		CountAndFire();
	}

	private void CountAndFire()
	{
		shotCounter -= Time.deltaTime;
		if (shotCounter <= 0f)
		{
			Fire();
			shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
		}
	}

	private void Die(){
		AudioSource.PlayClipAtPoint(deathSound, transform.position, deathSoundVolume);
		GameObject explosion = Instantiate(explosionEffectPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
		Destroy(explosion, explosion.GetComponent<ParticleSystem>().main.duration);
		Destroy(gameObject);
		ScoreKeeper.Score(scoreValue);	
	}
	
	private void Fire(){		
		GameObject beam = Instantiate(projectilePrefab, new Vector3(transform.position.x,transform.position.y - 0.5f,transform.position.z), Quaternion.identity) as GameObject;		
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0,-projectileSpeed,0);
		AudioSource.PlayClipAtPoint(fireSound, transform.position, fireSoundVolume);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
		GetDamaged(damageDealer);
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		DamageDealer damageDealer = collider.gameObject.GetComponent<DamageDealer>();
		GetDamagedByProjectile(damageDealer);
	}

	private void GetDamaged(DamageDealer damageDealer)
	{
		health -= damageDealer.GetDamage();
		if (health <= 0)
		{
			Die();
		}
	}

	private void GetDamagedByProjectile(DamageDealer damageDealer)
	{
		health -= damageDealer.GetDamage();
		damageDealer.Hit();
		if (health <= 0)
		{
			Die();
		}
	}
}
