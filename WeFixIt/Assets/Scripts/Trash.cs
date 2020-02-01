using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : Item
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
    }

    public override void init()
    {
        base.init();

        StartCoroutine(resetTrashFalls());
       // print("init do trash" + players.Count);
       
    }




    private IEnumerator resetTrashFalls()
    {
        yield return null;
        foreach (Player p in players)
        {

            p.setTrashFall(false);
        }
    }
}
