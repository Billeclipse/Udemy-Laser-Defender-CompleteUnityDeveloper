using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[Header("Player")]
	[SerializeField] float health = 250f;
	[SerializeField] float moveSpeed = 15f;
	[SerializeField] float padding = 0.5f;

	[Header("Projectile")]
	[SerializeField] public GameObject projectilePrefab;
	[SerializeField] public float projectileSpeed;
	[SerializeField] float firingRate = 0.2f;
	[SerializeField] public AudioClip fireSound;
	[Range(0f, 1f)] [SerializeField] public float fireSoundVolume = 0.2f;

	[Header("Death Settings")]
	[SerializeField] public AudioClip deathSound;
	[Range(0f, 1f)] [SerializeField] public float deathSoundVolume = 0.2f;
	[SerializeField] public GameObject explosionEffectPrefab;

	//private Vector3 position;
	private float xMin;
	private float xMax;
	private float yMin;
	private float yMax;

	private Coroutine firingCoroutine;

	// Use this for initialization
	private void Start ()
	{
		//position = this.transform.position;
		SetUpMoveBoundaries();
	}

	// Update is called once per frame
	private void Update ()
	{
		Fire();
		Move();
	}

	private void SetUpMoveBoundaries()
	{
		float distance = transform.position.z - Camera.main.transform.position.z;

		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		Vector3 downMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 upMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance));

		xMin = leftMost.x + padding;
		xMax = rightMost.x - padding;
		yMin = downMost.y + padding;
		yMax = upMost.y - padding;
	}

	private IEnumerator FireContinuously()
	{
		while (true)
		{
			GameObject beam = Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity) as GameObject;
			beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
			AudioSource.PlayClipAtPoint(fireSound, transform.position, fireSoundVolume);
			yield return new WaitForSeconds(firingRate);
		}		
	}

	private void Fire()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			firingCoroutine = StartCoroutine(FireContinuously());
		}
		if (Input.GetButtonUp("Fire1"))
		{
			StopCoroutine(firingCoroutine);
		}
	}

	private void Move()
	{
		var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
		var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

		var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
		var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

		transform.position = new Vector2(newXPos, newYPos);
	}
		
	private IEnumerator Die(){
		LevelManager levelManager = FindObjectOfType<LevelManager>();
		GameObject explosion = Instantiate(explosionEffectPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
				
		gameObject.GetComponent<SpriteRenderer>().forceRenderingOff = true;
		AudioSource.PlayClipAtPoint(deathSound, transform.position, deathSoundVolume);
		Destroy(explosion, explosion.GetComponent<ParticleSystem>().main.duration);
		Destroy(gameObject, 1.1f);
		Time.timeScale = 0.5f;
		yield return new WaitForSeconds(1f);
		Time.timeScale = 1f;
		levelManager.LoadNextLevel();
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
			StartCoroutine(Die());
		}
	}

	private void GetDamagedByProjectile(DamageDealer damageDealer)
	{
		health -= damageDealer.GetDamage();
		damageDealer.Hit();
		if (health <= 0)
		{
			StartCoroutine(Die());
		}
	}
}
