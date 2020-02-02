using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    [SerializeField]
    int score = 0;
    [SerializeField]
    float time;

    [SerializeField]
    int dishQuantity;
    [SerializeField]
    Transform spawnPos;

    [SerializeField]
    GameObject dish_prefab;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        RespawnDish();
       
    }

    // Update is called once per frame
    void Update()
    {
        
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
            dishQuantity--;
        }
        else return;
        
    }
}
