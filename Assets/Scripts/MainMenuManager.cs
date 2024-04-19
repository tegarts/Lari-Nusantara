using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject panelMain;
    public GameObject panelShoes;

    private void Start()
    {
        panelMain.SetActive(true);
        panelShoes.SetActive(false);
    }

    public void ShoesButton()
    {
        panelShoes.SetActive(true);
    }

    public void BackButtonFromShoes()
    {
        panelShoes.SetActive(false);
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
