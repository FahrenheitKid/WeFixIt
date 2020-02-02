using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public float fullY;
    public float frameIncrease;
    public bool isFull = false;

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("WaterJet"))
        {
            transform.Translate(new Vector3(0, frameIncrease, 0));
            if (transform.position.y >= fullY)
            {
                transform.position = new Vector3(transform.position.x, fullY, transform.position.y);
                isFull = true;
            }
        }
    }
}
