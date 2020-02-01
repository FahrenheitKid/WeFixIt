using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

   public struct Task
    {
        float time;
        float timeCurrent;
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

    protected BoxCollider interactionCollider;

    List<Player> players = new List<Player>();
    Player currentPlayer = null;
    bool isBeingCarried;
    bool isCarriable;

    Task pickUp;
    Task dropOff;
    Task action;

    // Start is called before the first frame update
    void Start()
    {
        pickUp.setTimeAndReset(2);
        dropOff.setTimeAndReset(2);
        action.setTimeAndReset(2);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Player p in players) // foreach player within the item area
        {
            if(currentPlayer = null)
            {
                if (InputManager.GetKeyDown(p.getId(), KeyActions.Action)) // if the player is within action area of the object and pressed action button
                {
                    currentPlayer = p; // set it as the only player that can interact with it
                    
                }
            }
            
        }

        // TODO

        if (InputManager.GetKeyDown(currentPlayer.getId(), KeyActions.Action)) // if the current player is within action area of the object and pressed action button
        {
            if (isCarriable && !pickUp.IsComplete()) // if the item can be carried and has not completed the pickup action
            {
                pickUp.progress(); // progress the bar/timer
            }
        }
        else if(InputManager.GetKeyUp(currentPlayer.getId(), KeyActions.Action)) // if the current player lets go the button
            {
                if(isCarriable && !pickUp.IsComplete())
                {
                pickUp.reset(); // reset the progress
                currentPlayer = null; // and also leave the slot for current player for other players

                }

            }




    }

    public void Action()
    {
        

    }


    public void addPlayer(Player p)
    {
        if (!players.Contains(p)) players.Add(p);
    }

    public void removePlayer(Player p)
    {
        if (currentPlayer == p) currentPlayer = null;
        if (players.Contains(p)) players.Remove(p);

    }

}
