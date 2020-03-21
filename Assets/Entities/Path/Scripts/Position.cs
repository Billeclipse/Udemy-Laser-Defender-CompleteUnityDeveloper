using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {

	[SerializeField] float gizmoRadius = 0.5f;

	void OnDrawGizmos(){
		Gizmos.DrawWireSphere(transform.position, gizmoRadius);
	}
}
