using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : Item
{
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

    public override void init()
    {
        base.init();

        StartCoroutine(resetTrashFalls());
       // print("init do trash" + players.Count);
       
    }


    public override void Drop()
    {
        base.Drop();
        if (isInsideDropOffArea)
        {
            game_ref.PlayDump();
            game_ref.Score(scoreValue);
            game_ref.DeliverTrash(this);
            Destroy(this.gameObject);
        }

       

    }

    public override void PickUp(Player p)
    {
        base.PickUp(p);

        p.setTrashFall(false);
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
