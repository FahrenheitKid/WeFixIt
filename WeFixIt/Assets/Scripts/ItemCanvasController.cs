using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCanvasController : MonoBehaviour
{
    private Canvas canvas;
    private Image fill;
    private TMP_Text text;
    private Item item;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        fill = transform.GetChild(0).GetComponent<Image>();
        text = transform.GetComponentInChildren<TMP_Text>();
        item = GetComponentInParent<Item>();
    }

    private void Update()
    {
        transform.forward = Camera.main.transform.forward;

        if ((!item.getActionLock() && item.GetPlayerList().Any()) || item.GetBeingCarried())
        {
            canvas.enabled = true;
        }
        else
        {
            canvas.enabled = false;
        }

        if (!item.GetBeingCarried() && item.IsCarriable())
        {
            text.text = "PICK UP";

            if (item.GetPickUp().timeCurrent > 0)
            {
                fill.fillAmount = 1 - (item.GetPickUp().getRemainingTime() / item.pickupTime);
            }
            else
            {
                fill.fillAmount = 0;
            }
        }
        else if(!item.IsCarriable() && item.getActionTask().time > 0 && !item.getActionTask().IsComplete() && !item.getActionLock())
        {
            bool on = false;
            //Hose Box
            if(item.CompareTag("Hose Box"))
            {
                HoseBox hb = (HoseBox)item;
                on = hb.getHoseRef().getWaterJet().isPlaying;
            }
            
            text.text = (on)?   "TURN OFF" : "TURN ON";

            if (item.getActionTask().timeCurrent > 0 )
            {
                fill.fillAmount = 1 - (item.getActionTask().getRemainingTime() / item.actionTime);
            }
            else
            {
                fill.fillAmount = 0;
            }
        }
        else if(item.GetBeingCarried())
        {
            text.text = "DROP";
            fill.fillAmount = 0;
        }
    }
}
