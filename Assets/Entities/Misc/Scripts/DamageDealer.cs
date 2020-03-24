using UnityEngine;
using System.Collections;

public class DamageDealer : MonoBehaviour {
	[SerializeField] int damage = 50;
	
	public int GetDamage(){
		return damage;	
	}
	
	public void Hit(){
		Destroy(gameObject);	
	}	
}
