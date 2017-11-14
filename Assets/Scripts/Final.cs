using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Final : MonoBehaviour {

    public static string Result;
    public static int Team;
    public static int Index;

    public GameObject[] Panels;

    [Header("Winner")]
    public Image FillArea;
    public Sprite[] Sprite;
    public Text Header;

    private void Start()
    {
        SetActive(Index);
    }

    private void SetActive(int index)
    {
        if(index == 0)
        {
            Header.text = Result;
            FillArea.sprite = Sprite[Team];
        }
        Panels[index].SetActive(true);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
