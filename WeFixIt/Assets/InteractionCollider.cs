using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCollider : MonoBehaviour
{

    [SerializeField]
    private Item item_ref;
 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            item_ref.addPlayer(collision.gameObject.GetComponent<Player>());
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            item_ref.removePlayer(collision.gameObject.GetComponent<Player>()); ;
        }
    }
}
