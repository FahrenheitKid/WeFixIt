using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 

public class Game : MonoBehaviour
{

    [SerializeField]
    int score = 0;
    [SerializeField]
    float time;


    [SerializeField]
    int dishQuantity;

    [SerializeField]
    bool isPoolComplete;
    [SerializeField]
    bool dishComplete;
    [SerializeField]
    List<Dirt> dirts = new List<Dirt>();
    [SerializeField]
    List<Trash> trashes = new List<Trash>();


    [SerializeField]
    Transform spawnPos;

    [SerializeField]
    GameObject dish_prefab;

    private Animator dumpAnimator;

    [SerializeField]
    TextMeshProUGUI timeText;

    [SerializeField]
    TextMeshProUGUI trashText;

    [SerializeField]
    TextMeshProUGUI dishText;

    [SerializeField]
    TextMeshProUGUI poolText;

    [SerializeField]
    TextMeshProUGUI dirtText;

    [SerializeField]
    TextMeshProUGUI winText;

    [SerializeField]
    Pool pool_ref;

    bool stopTime = false;


    [SerializeField]
    List<Player> players = new List<Player>();


    // Start is called before the first frame update
    void Start()
    {
        dumpAnimator = GameObject.Find("Dump").GetComponent<Animator>();
        Application.targetFrameRate = 60;

        foreach( GameObject go in GameObject.FindGameObjectsWithTag("Dirt"))
        {
            dirts.Add(go.GetComponent<Dirt>());

        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Trash"))
        {
            trashes.Add(go.GetComponent<Trash>());

        }

        for(int i = 0; i < PlayerPrefs.GetInt("PlayerQtd"); i++)
        {
            if(i < players.Count)
            {
                players[i].gameObject.SetActive(true);
            }
        }

        
        RespawnDish();

        updateTextCounters();
       
    }

    // Update is called once per frame
    void Update()
    {
        if(time >= 0 && !stopTime)
        {
            time -= Time.deltaTime;
            timeText.text = FloatToTime(time, "#00:00");
        }
        else if(time < 0)
        {
            winText.text = "We didn't fix it :/";
            winText.transform.parent.gameObject.SetActive(true);
        }
        

       

    }

    public void Score(int score)
    {
        this.score += score;
    }

    public void SpawnDish()
    {
        GameObject go = Instantiate(dish_prefab, spawnPos.position, Quaternion.identity);
        go.GetComponent<Dish>().init();
    }

    public void RespawnDish() // safe version of spawn dish, decreasing from dish quantity
    {
        if (dishQuantity > 0)
        {
            GameObject go = Instantiate(dish_prefab, spawnPos.position, Quaternion.identity);
            go.GetComponent<Dish>().init();
        }
        else return;
        
    }

    public void PlayDump()
    {
        dumpAnimator.SetTrigger("dump");
    }

    public static string FloatToTime(float toConvert, string format)
    {
        switch (format)
        {
            case "00.0":
                return string.Format("{0:00}:{1:0}",
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 10) % 10));//miliseconds
                break;
            case "#0.0":
                return string.Format("{0:#0}:{1:0}",
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 10) % 10));//miliseconds
                break;
            case "00.00":
                return string.Format("{0:00}:{1:00}",
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 100) % 100));//miliseconds
                break;
            case "00.000":
                return string.Format("{0:00}:{1:000}",
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 1000) % 1000));//miliseconds
                break;
            case "#00.000":
                return string.Format("{0:#00}:{1:000}",
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 1000) % 1000));//miliseconds
                break;
            case "#0:00":
                return string.Format("{0:#0}:{1:00}",
                    Mathf.Floor(toConvert / 60),//minutes
                    Mathf.Floor(toConvert) % 60);//seconds
                break;
            case "#00:00":
                return string.Format("{0:#00}:{1:00}",
                    Mathf.Floor(toConvert / 60),//minutes
                    Mathf.Floor(toConvert) % 60);//seconds
                break;
            case "0:00.0":
                return string.Format("{0:0}:{1:00}.{2:0}",
                    Mathf.Floor(toConvert / 60),//minutes
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 10) % 10));//miliseconds
                break;
            case "#0:00.0":
                return string.Format("{0:#0}:{1:00}.{2:0}",
                    Mathf.Floor(toConvert / 60),//minutes
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 10) % 10));//miliseconds
                break;
            case "0:00.00":
                return string.Format("{0:0}:{1:00}.{2:00}",
                    Mathf.Floor(toConvert / 60),//minutes
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 100) % 100));//miliseconds
                break;
            case "#0:00.00":
                return string.Format("{0:#0}:{1:00}.{2:00}",
                    Mathf.Floor(toConvert / 60),//minutes
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 100) % 100));//miliseconds
                break;
            case "0:00.000":
                return string.Format("{0:0}:{1:00}.{2:000}",
                    Mathf.Floor(toConvert / 60),//minutes
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 1000) % 1000));//miliseconds
                break;
            case "#0:00.000":
                return string.Format("{0:#0}:{1:00}.{2:000}",
                    Mathf.Floor(toConvert / 60),//minutes
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 1000) % 1000));//miliseconds
                break;
        }
        return "error";
    }

    public void DeliverDish()
    {
        if(dishQuantity > 0)
        {
            dishQuantity--;
            updateTextCounters();
        }
    }

    public void DeliverTrash(Trash t)
    {
        if (trashes.Contains(t))
        {
            trashes.Remove(t);
            updateTextCounters();
        }
           
    }

    public void RemoveDirt(Dirt t)
    {
        if (dirts.Contains(t))
        {
            dirts.Remove(t);
            updateTextCounters();
        }

    }

    public bool checkWin()
    {
        if (isPoolComplete && dirts.Count == 0 && trashes.Count == 0 && dishQuantity == 0)
        {
            winText.text = "We fixed it! :D";
            winText.transform.parent.gameObject.SetActive(true);
            stopTime = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void addTrash(Trash t)
    {
        if(!trashes.Contains(t))
        {
            trashes.Add(t);
            updateTextCounters();
        }
       
    }

    public void setPoolComplete(bool b)
    {
        isPoolComplete = b;
    }

    public void updateTextCounters()
    {

        /*
         * 
         * inicial -0.7 --- 0%
         * final -0.3 ---- 100%
         * soma 0.7
         * 
         * 0 --- 0%
         * 0.4 --- 100%
         * 
         * 
         * x --- %
         * 0.4 -- 100%
         * 
         * */
        
        trashText.text = " Trash \n" + trashes.Count().ToString();
        dishText.text = " Dish \n" + dishQuantity.ToString();
        dirtText.text = " Dirt \n" + dirts.Count().ToString();
        int val = (int)((pool_ref.transform.position.y - pool_ref.initY) * 100 / (pool_ref.fullY - pool_ref.initY));
        poolText.text = " Pool \n" + val.ToString() + "%";

        checkWin();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}
