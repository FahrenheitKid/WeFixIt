using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Item : MonoBehaviour
{

   public struct Task
    {
        public float time;
        public float timeCurrent;
        bool isComplete;

        public Task(float time)
        {
            this.time = time;
            timeCurrent = 0;
            isComplete = false;
        }


        public void progress()
        {
            if(!isComplete)
            {
                if (timeCurrent < time)
                {
                    timeCurrent += Time.deltaTime;
                }
                else
                {
                    timeCurrent = time;
                    isComplete = true;
                }
                   
            }
        }

        public void reset()
        {
            isComplete = false;
            timeCurrent = 0;
        }

        public void setTime(float time)
        {
            this.time = time;
        }

        public void setTimeAndReset(float time)
        {
            this.time = time;
            reset();
        }
        public bool IsComplete()
        {
            return isComplete;
        }
        public float getRemainingTime()
        {
            return Mathf.Abs(timeCurrent - time);
        }
        




    }

    [SerializeField]
    protected BoxCollider interactionCollider;

    [SerializeField]
    List<Player> players = new List<Player>(); // Players within item area
    [SerializeField]
    List<Player> currentPlayers = new List<Player>(); // players currently interacting
    [SerializeField]
    bool isBeingCarried;
    [SerializeField]
    bool isCarriable;
    [SerializeField]
    bool allowMultiplePlayers;


    [Header("timers")]
    public float pickupTime;
    public float dropOffTime;
    public float actionTime;

    Task pickUp;
    Task dropOff;
    Task action;

    // Start is called before the first frame update
    void Start()
    {
        pickUp.setTimeAndReset(pickupTime);
        dropOff.setTimeAndReset(dropOffTime);
        action.setTimeAndReset(actionTime);
    }

    // Update is called once per frame
    void Update()
    {
        //Save current Players
        if (!currentPlayers.Any() && !allowMultiplePlayers)
        {
            foreach (Player p in players) // foreach player within the item area
            {

                if (InputManager.GetKeyDown(p.getId(), KeyActions.Action)) // if the player is within action area of the object and pressed action button
                {

                    currentPlayers.Add(p); // set it as the only player that can interact with it
                    break;

                }


            }
        }
        else if (allowMultiplePlayers)
        {
            foreach (Player p in players) // foreach player within the item area
            {
                if (currentPlayers.Contains(p)) continue;

                if (InputManager.GetKeyDown(p.getId(), KeyActions.Action)) // if the player is within action area of the object and pressed action button
                {
                    currentPlayers.Add(p); // set it as the only player that can interact with it


                }
            }
            
        }

        // TODO

        if (!isCarriable && allowMultiplePlayers && !action.IsComplete())
        {//generator item in this case
            foreach (Player p in players) // foreach player within the item area
            {
                if (p.getKeyDown(KeyActions.Action))
                {

                    action.progress();
                }
                if (action.IsComplete())
                {

                    Action();
                    break;
                }
            }
        }
        else if (isCarriable && !allowMultiplePlayers && !pickUp.IsComplete() && currentPlayers.Any())
        {

            if (currentPlayers.First().getKey(KeyActions.Action))
            {
                pickUp.progress();
                print(pickUp.getRemainingTime());

            }
            else if (currentPlayers.First().getKeyUp(KeyActions.Action))
            {
                pickUp.reset();
            }

            if (pickUp.IsComplete())
            {
                PickUp(currentPlayers.First());
            }
        }
        else if (isCarriable && isBeingCarried && currentPlayers.Any() && !allowMultiplePlayers)
        {
            if (currentPlayers.First().getKeyDown(KeyActions.Action))
            {
                Drop();

            }
        }




    }

    public void Action()
    {
        //each item will have its own 

    }

    public void PickUp(Player p)
    {

        transform.parent = p.itemPosition;
        transform.localPosition = Vector3.zero;

        isBeingCarried = true;
    }


    public void Drop()
    {
        transform.parent = null;

        isBeingCarried = false;
        pickUp.reset();


    }


    public void addPlayer(Player p)
    {
        if (!players.Contains(p)) players.Add(p);
    }

    public void removePlayer(Player p)
    {
        if (currentPlayers.Contains(p)) currentPlayers.Remove(p);
        if (players.Contains(p)) players.Remove(p);

    }

    public List<Player> GetPlayerList()
    {
        return players;
    }

    public List<Player> GetCurrentPlayer()
    {
        return currentPlayers;
    }

    public Task GetPickUp()
    {
        return pickUp;
    }

    public bool GetBeingCarried()
    {
        return isBeingCarried;
    }
}
