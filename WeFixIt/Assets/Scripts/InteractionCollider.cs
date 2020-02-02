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
        if (item_ref.CompareTag("Hose Box"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.gameObject.GetComponent<Player>().GetItem() != null)
                {
                    if (other.gameObject.GetComponent<Player>().GetItem().CompareTag("Hose"))
                    {

                        item_ref.setActionLock(true);
                    }
                }
            }
        }

    }

   
    private void OnTriggerExit(Collider other)
    {
       
            if (other.gameObject.CompareTag("Player"))
            {
                
                item_ref.removePlayer(other.gameObject.GetComponent<Player>());
            }

        if (item_ref.CompareTag("Hose Box"))
        {
            if(other.gameObject.CompareTag("Player"))
            {
                if (other.gameObject.GetComponent<Player>().GetItem() != null)
                {
                    if (other.gameObject.GetComponent<Player>().GetItem().CompareTag("Hose"))
                    {

                        item_ref.setActionLock(false);
                    }
                }
            }
            
        }
    }
}
