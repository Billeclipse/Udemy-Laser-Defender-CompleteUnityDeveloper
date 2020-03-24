using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPosition : MonoBehaviour
{
    [SerializeField] float gizmoAreaWidth = 6.5f;
    [SerializeField] float gizmoAreaheight = 11.5f;

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gizmoAreaWidth, gizmoAreaheight));
    }
}
