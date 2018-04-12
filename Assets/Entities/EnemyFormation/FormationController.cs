using UnityEngine;
using System.Collections;

public class FormationController : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 2f;
	public float padding = 0.5f;
	public float spawnDelay = 0.5f;
	
	public bool movingRight = true;
	private float xMax;
	private float xMin;
	
	// Use this for initialization
	void Start () {	
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distanceToCamera));
		xMax = rightBoundary.x + padding;
		xMin = leftBoundary.x - padding;
		SpawnUntilFull();
	}
	
	void SpawnEnemies(){
		foreach (Transform child in transform){
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}
	
	void SpawnUntilFull(){
		Transform freePosition = NextFreePostition();
		if(freePosition){
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if(NextFreePostition()){
			 Invoke ("SpawnUntilFull", spawnDelay);
		}
	}
	
	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width,height));
	
	}
	
	// Update is called once per frame
	void Update () {
		if(movingRight){
			transform.position += Vector3.right * speed * Time.deltaTime;
		}else{
			transform.position += Vector3.left * speed * Time.deltaTime;			
		}
		
		float rightEdgeOfFormation = transform.position.x + (0.5f*width);
		float leftEdgeOfFormation = transform.position.x - (0.5f*width);
		
		if(leftEdgeOfFormation < xMin){
			movingRight = true;	
		}else if(rightEdgeOfFormation > xMax){
			movingRight = false;
		}
		
		if(AllMembersDead()){
			SpawnUntilFull();
		}
				
	}
	
	Transform NextFreePostition(){
		foreach(Transform childPostitionGameObject in transform){
			if(childPostitionGameObject.childCount == 0){
				return childPostitionGameObject;
			}
		}
		return null;
	}
	
	bool AllMembersDead(){
		foreach(Transform childPostitionGameObject in transform){
			if(childPostitionGameObject.childCount > 0){
				return false;
			}
		}
		return true;
	}
}
