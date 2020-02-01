using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoseBox : MonoBehaviour
{
    [Header("Properties")]
    public Vector2 colliderSize;

    public void AddCollider(Vector3 pointA, Vector3 pointB)
    {
        GameObject newObject = new GameObject("Hose Collider");
        BoxCollider newCollider = newObject.AddComponent<BoxCollider>();

        newCollider.transform.parent = transform;
        newCollider.transform.position = pointA + ((pointB - pointA) / 2f);
        newCollider.transform.LookAt(pointB);
        newCollider.size = new Vector3(colliderSize.x, colliderSize.y, Vector3.Distance(pointA, pointB));
        newCollider.isTrigger = true;
    }

    public void DeleteColliders()
    {
        for(int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
