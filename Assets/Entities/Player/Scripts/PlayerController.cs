using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[SerializeField] float moveSpeed = 15f;
	[SerializeField] float health = 250f;
	[SerializeField] float padding = 0.5f;
	[SerializeField] public GameObject projectile;
	[SerializeField] public float projectileSpeed;
	[SerializeField] float firingRate = 0.2f;
	[SerializeField] public AudioClip fireSound;
	
	//private Vector3 position;
	private float xMin;
	private float xMax;
	private float yMin;
	private float yMax;

	private Coroutine firingCoroutine;

	// Use this for initialization
	void Start ()
	{
		//position = this.transform.position;
		SetUpMoveBoundaries();
	}	

	// Update is called once per frame
	void Update ()
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
			GameObject beam = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity) as GameObject;
			beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
			AudioSource.PlayClipAtPoint(fireSound, transform.position);
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
		
	void Die(){
		LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		man.LoadLevel("Win Screen");		
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D collider)
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
}
