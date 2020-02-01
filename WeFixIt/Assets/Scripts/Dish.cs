using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dish : Item
{
    [SerializeField]
    GameObject trash;
    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();

    }

    public override void Drop()
    {
        base.Drop();
        if(isInsideDropOffArea)
        {
            game_ref.Score(scoreValue);
        }
        else
        {
           GameObject go = Instantiate(trash, transform.position, Quaternion.identity);
            
            go.GetComponent<Trash>().init();
            
            
            

        }
        
        Destroy(this.gameObject);

    }

    

   
}
