using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour, IDataPersistence
{
    public GameObject pausePanel;
    public GameObject countdownPanel;
    public GameObject mainMenuPanel;
    public GameObject settingPanel;
    public GameObject aboutPanel;
    public GameObject startButton;
    public GameObject logoImage;
    public GameObject settingButton;
    public GameObject aboutButton;
    public GameObject skorButton;
    public GameObject skorPanel;

    [Header("Shoes selection")]
    public GameObject backButton;
    public GameObject shoesStat;
    public GameObject recButton;
    public GameObject shoesList;
    public GameObject mulaiButton;

    [Header("Anim")]
    public Animator settingPanelAnim;
    public Animator aboutPanelAnim;
    public Animator logoAnim;
    public Animator startButtonAnim;
    public Animator settingButtonAnim;
    public Animator aboutButtonAnim;
    public Animator backButtonAnim;
    public Animator shoesStatAnim;
    public Animator shoesListAnim;
    public Animator recButtonAnim;
    public Animator mulaiButtonAnim;
    public Animator skorButtonAnim;
    public Animator skorPanelAnim;

    [Header("Shoes Selection")]
    public TMP_Text _shoesName;
    public TMP_Text _koin;
    public TMP_Text _powerup;
    public TMP_Text _lompat;
    public TMP_Text _menunduk;
    public TMP_Text _pindah;
    public TMP_Text _jarak;
    public TMP_Text _skor;
    public TMP_Text _kecepatan;
    public ShoesStat statChoose;
    public GameObject buttonRekomendasi;

    public TMP_Text _sepatuRek;
    public List<ShoesStat> _shoes = new();

    [Header("High Score")]
    public TMP_Text skorTertinggi;
    public TMP_Text totalKoin;
    public TMP_Text jarakTerjauh;
    private float highScore;
    private int totalCoin;
    private float longestDistance;

    [Header("other")]
    public TMP_Text countdownText;
    private bool isOver = false;
    private bool isRetry;

    public GameObject pauseBotton;
    public GameObject hudGame;

    public TMP_Text skorGO;
    public TMP_Text koinGO;
    public TMP_Text jarakGO;
    private bool isPlay;
    // Start is called before the first frame update
    public void LoadData(GameData data)
    {
        highScore = data.highScore;
        totalCoin = data.totalCoins;
        longestDistance = data.longestDistance;
    }

    public void SaveData(ref GameData data)
    {
        data.isRetry = isRetry;
    }

    void Start()
    {
        aboutButton.SetActive(true);
        settingButton.SetActive(true);
        startButton.SetActive(true);
        logoImage.SetActive(true);
        skorButton.SetActive(true);
        pausePanel.SetActive(false);
        countdownPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        pauseBotton.SetActive(false);
        hudGame.SetActive(false);
        settingPanel.SetActive(false);
        aboutPanel.SetActive(false);

        // shoes selection
        backButton.SetActive(false);
        shoesStat.SetActive(false);
        shoesList.SetActive(false);
        recButton.SetActive(false);
        mulaiButton.SetActive(false);

        isPlay = false;

        AudioManager.instance.PlayMusic("theme");
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.isPlay && !isOver)
        {
            StartCoroutine(CountDown());
            AudioManager.instance.PlaySFX("countdown");
        }


        if(GameManager.isPlay)
        {
            //mainMenuPanel.SetActive(false);
            startButton.SetActive(false);
            logoImage.SetActive(false);
            settingButton.SetActive(false);
            aboutButton.SetActive(false);
            skorButton.SetActive(false);

        }

        if(GameManager.isStartGame)
        {
            pauseBotton.SetActive(true);
            hudGame.SetActive(true);
            if(!isPlay)
            {
                AudioManager.instance.PlayMusic("gameplay");
                isPlay = true;
            }
            
        }

        TextShoes();

        skorGO.text = PlayerManager.scoreTambahan.ToString();
        koinGO.text = PlayerManager.koinTambahan.ToString();
        jarakGO.text = PlayerManager.jarakTambahan + "m";

        if (PlayerManager.gameOver)
        {
            hudGame.SetActive(false);
            pauseBotton.SetActive(false);
        }
    }

    IEnumerator CountDown()
    {
        isOver = true;
        countdownPanel.SetActive(true);
        countdownText.text = "3";
        yield return new WaitForSeconds(1f);
        countdownText.text = "2";
        yield return new WaitForSeconds(1f);
        countdownText.text = "1";
        yield return new WaitForSeconds(1f);
        countdownPanel.SetActive(false);
        GameManager.isStartGame = true;
    }

    private void TextShoes()
    {
        statChoose = _shoes[ShoesManager.selectedShoes];
        _shoesName.text = statChoose.shoesName;
        _koin.text = "+ " + statChoose.jKoin;
        _powerup.text = "+ " + statChoose.jPowerup;
        _lompat.text = statChoose.jLompat.ToString();
        _menunduk.text = statChoose.jMenunduk.ToString();
        _pindah.text = statChoose.jBerpindah.ToString();
        _jarak.text = "x " + statChoose.jJarak;
        _skor.text = "x " + statChoose.jSkor;
        _kecepatan.text = "x " + statChoose.jKecepatan;
        _sepatuRek.text = statChoose.shoesName;
    }
    public void StartGame()
    {
        StartCoroutine(StartGameDelay());
        
    }

    public void ShoesSelection()
    {
        StartCoroutine(CloseMainMenu());
    }

    public void TryAgain()
    {
        isRetry = true;
        SceneManager.LoadScene("Gameplay 2");
    }
    public void PauseMenu()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        AudioManager.instance.PauseMusic("gameplay");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        AudioManager.instance.PlayMusic("gameplay");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Gameplay 2");
    }

    public void OpenSetting()
    {
        settingPanel.SetActive(true);
        StartCoroutine(OpenPanel());
    }

    public void CloseSetting()
    {
        StartCoroutine(SettingClose());
    }

    public void OpenAbout()
    {
        StartCoroutine(OpenPanel());
        aboutPanel.SetActive(true);
    }

    public void CloseAbout()
    {
        StartCoroutine(AboutClose());
    }

    public void OpenSkor()
    {
        StartCoroutine(OpenPanel());
        skorTertinggi.text = highScore.ToString();
        totalKoin.text = totalCoin.ToString();
        jarakTerjauh.text = longestDistance + " m";
        skorPanel.SetActive(true);
    }

    public void CloseSkor()
    {
        StartCoroutine(SkorClose());
    }

    public void BackShoes()
    {
        StartCoroutine(CloseShoes());
    }

    public void ButtonClicked()
    {
        AudioManager.instance.PlaySFX("button");
    }


    // ANIMATION ----------------------------------------

    IEnumerator OpenPanel()
    {
        startButtonAnim.SetTrigger("close2");
        yield return new WaitForSeconds(0.5f);
        startButton.SetActive(false);
    }
    IEnumerator SettingClose()
    {
        settingPanelAnim.SetTrigger("close");
        startButton.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        settingPanel.SetActive(false);
    }

    IEnumerator AboutClose()
    {
        aboutPanelAnim.SetTrigger("close");
        startButton.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        aboutPanel.SetActive(false);
    }

    IEnumerator SkorClose()
    {
        skorPanelAnim.SetTrigger("close");
        startButton.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        skorPanel.SetActive(false);
    }

    IEnumerator CloseMainMenu()
    {
        startButtonAnim.SetTrigger("close");
        logoAnim.SetTrigger("close");
        settingButtonAnim.SetTrigger("close");
        aboutButtonAnim.SetTrigger("close");
        skorButtonAnim.SetTrigger("close");
        yield return new WaitForSeconds(0.5f);
        startButton.SetActive(false);
        logoImage.SetActive(false);
        settingButton.SetActive(false);
        aboutButton.SetActive(false);
        skorButton.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        backButton.SetActive(true);
        shoesStat.SetActive(true);
        shoesList.SetActive(true);
        recButton.SetActive(true);
        mulaiButton.SetActive(true);

        if(!DataPersistenceManager.instance.HasGameData())
        {
            buttonRekomendasi.SetActive(false);
        }
        else
        {
            buttonRekomendasi.SetActive(true);
        }
    }

    IEnumerator CloseShoes()
    {
        backButtonAnim.SetTrigger("close");
        shoesStatAnim.SetTrigger("close");
        shoesListAnim.SetTrigger("close");
        recButtonAnim.SetTrigger("close");
        mulaiButtonAnim.SetTrigger("close");
        yield return new WaitForSeconds(0.5f);
        backButton.SetActive(false);
        shoesStat.SetActive(false);
        shoesList.SetActive(false);
        recButton.SetActive(false);
        mulaiButton.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        logoImage.SetActive(true);
        startButton.SetActive(true);
        settingButton.SetActive(true);
        aboutButton.SetActive(true);
        skorButton.SetActive(true);
    }

    IEnumerator StartGameDelay()
    {
        backButtonAnim.SetTrigger("close");
        shoesStatAnim.SetTrigger("close");
        shoesListAnim.SetTrigger("close");
        recButtonAnim.SetTrigger("close");
        mulaiButtonAnim.SetTrigger("close");
        yield return new WaitForSeconds(0.5f);
        backButton.SetActive(false);
        shoesStat.SetActive(false);
        shoesList.SetActive(false);
        recButton.SetActive(false);
        mulaiButton.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        if (!DataPersistenceManager.instance.HasGameData())
        {
            DataPersistenceManager.instance.NewGame();
            //SceneManager.LoadSceneAsync("Main Menu");
            GameManager.isPlay = true;
            Debug.Log("New Game");
        }
        else
        {
            //SceneManager.LoadSceneAsync("Main Menu");
            GameManager.isPlay = true;
            Debug.Log("Load Game");
        }
    }
}
