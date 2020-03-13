using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 15f;
	public float health = 250f;
	public float padding = 0.5f;
	public GameObject projectile;
	public float projectileSpeed;
	public float firingRate = 0.2f;
	public AudioClip fireSound;
	
	private Vector3 position;
	private float xMin;
	private float xMax;	

	// Use this for initialization
	void Start () {
		position = this.transform.position;
		float distance = position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		xMin = leftMost.x + padding;
		xMax = rightMost.x - padding;
	}
	
	void Fire(){	
		GameObject beam = Instantiate(projectile,new Vector3(transform.position.x,transform.position.y + 0.5f,transform.position.z), Quaternion.identity) as GameObject;		
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0,projectileSpeed,0);
		AudioSource.PlayClipAtPoint(fireSound,transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating("Fire", 0.000001f, firingRate);
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke("Fire");
		}
		if (Input.GetKey(KeyCode.RightArrow)){
			position.x = Mathf.Clamp(position.x + speed * Time.deltaTime,xMin,xMax);
		}
		else if (Input.GetKey(KeyCode.LeftArrow)){
			position.x = Mathf.Clamp(position.x - speed * Time.deltaTime,xMin,xMax);
		}
		this.transform.position = position;
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
		LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		man.LoadLevel("Win Screen");		
		Destroy(gameObject);
	}
}
