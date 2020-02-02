using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoseBox : Item
{
    [Header("Properties")]
    public Vector2 colliderSize;
    private Transform colliders;

    [SerializeField]
    Hose hose_ref;

    private void Awake()
    {
        colliders = transform.GetChild(0);
        actionLock = true;
    }

    private void Start()
    {
        init();
    }

    private void Update()
    {
        OnUpdate();
    }

    public void AddCollider(Vector3 pointA, Vector3 pointB)
    {
        GameObject newObject = new GameObject("Hose Collider");
        newObject.layer = LayerMask.NameToLayer("Hose");
        BoxCollider newCollider = newObject.AddComponent<BoxCollider>();

        newCollider.transform.parent = colliders;
        newCollider.transform.position = pointA + ((pointB - pointA) / 2f);
        newCollider.transform.LookAt(pointB);
        newCollider.size = new Vector3(colliderSize.x, colliderSize.y, Vector3.Distance(pointA, pointB));
        newCollider.isTrigger = true;
    }

    public void DeleteColliders()
    {
        for(int i = colliders.childCount - 1; i >= 0; i--)
        {
            Destroy(colliders.GetChild(i).gameObject);
        }
    }

    public override void Action()
    {
        base.Action();

        print("SHUA SHUA");


    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
