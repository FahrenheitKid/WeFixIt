using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private Image toggledImage;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("PlayerQtd"))
        {
            PlayerPrefs.SetInt("PlayerQtd", 4);
        }

        toggledImage = GameObject.Find(PlayerPrefs.GetInt("PlayerQtd").ToString() + "P Toggle").GetComponent<Image>();
        toggledImage.color = new Color(1f, 0.5f, 0.5f);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SelectPlayerQtd(int qtd)
    {
        toggledImage.color = Color.white;

        toggledImage = GameObject.Find(qtd + "P Toggle").GetComponent<Image>(); ;
        toggledImage.color = new Color(1f, 0.5f, 0.5f);
        PlayerPrefs.SetInt("PlayerQtd", qtd);
    }
}
