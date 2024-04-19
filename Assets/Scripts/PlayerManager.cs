using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour, IDataPersistence
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static int numberOfCoins;
    public static int koinTambahan;
    public TMP_Text coinText;

    public static int score;
    public static float scoreTambahan;
    public float scoreDecimal;
    public static int scoreMulti = 1;
    public TMP_Text scoreText;

    public static int jarak;
    public static float jarakTambahan;
    public float jarakDecimal;
    public TMP_Text jarakText;

    public UIManager uiManager;

    private bool isPlay;
   
    public void LoadData(GameData data)
    {
        
    }

    public void SaveData(ref GameData data)
    {
        //data.jumlahKoin = numberOfCoins;
        //data.jumlahSkor = score;
        //data.jarakTempuh = jarak;
        data.totalCoins += koinTambahan;
        if (data.highScore < scoreTambahan)
        {
            data.highScore = scoreTambahan;
        }
        if (data.longestDistance < jarakTambahan)
        {
            data.longestDistance = jarakTambahan;
        }
    }
    private void Start()
    {
        Time.timeScale = 1.0f;
        gameOver = false;
        gameOverPanel.SetActive(false);
        numberOfCoins = 0;
        score = 0;
        jarak = 0;
        isPlay = false;
    }

    private void Update()
    {
        if (gameOver)
        {
            if(!isPlay)
            {
                AudioManager.instance.StopMusic("gameplay");
                AudioManager.instance.PlaySFX("gameover");
                isPlay = true;
            }
            
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
        
        
        scoreTambahan = Mathf.Floor(score * uiManager.statChoose.jSkor);
        jarakTambahan = Mathf.Floor(jarak * uiManager.statChoose.jJarak);
        koinTambahan = numberOfCoins + uiManager.statChoose.jKoin;

        scoreText.text = score.ToString();
        jarakText.text = jarak + "m";
        coinText.text = numberOfCoins.ToString();

        if (GameManager.isStartGame)
        {

            scoreDecimal += 4f * Time.deltaTime * scoreMulti;
            score = Mathf.RoundToInt(scoreDecimal);

            

            jarakDecimal += 4f * Time.deltaTime;
            jarak = Mathf.RoundToInt(jarakDecimal);

            
        }
        

        
    }

    
}
