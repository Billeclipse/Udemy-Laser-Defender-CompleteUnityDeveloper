using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPosition : MonoBehaviour
{
    [SerializeField] float gizmoAreaWidth = 10f;
    [SerializeField] float gizmoAreaheight = 5f;

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gizmoAreaWidth, gizmoAreaheight));
    }
}
