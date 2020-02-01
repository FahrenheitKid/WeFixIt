using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCollider : MonoBehaviour
{

    [SerializeField]
    public Item item_ref;




    private void OnTriggerEnter(Collider other)
    {
       
           
        if (other.gameObject.CompareTag("Player"))
        {
           
            item_ref.addPlayer(other.gameObject.GetComponent<Player>());
        }
    }

   
    private void OnTriggerExit(Collider other)
    {
       
            if (other.gameObject.CompareTag("Player"))
            {
                
                item_ref.removePlayer(other.gameObject.GetComponent<Player>());
            }
    }
}
