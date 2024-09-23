using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public List<Text> highScoreText;
    public List<Text> levelText;
    public List<Text> goldText;
    public List<Text> goldBonusAd;
    public Text totalGoldText;
    public Text totalReviveText;
    public Text totalPlayText;
    public List<Text> playerExpText;
    public List<Slider> playerExpSlider;
    public Slider playerLvlSlider;
    public Slider settingGraphicSlider;
    public GameObject resetPanel;
    public GameObject bonusPanel;
    public GameObject MuteBtn;
    public GameObject UnmuteBtn;
    public Toggle toggleTuto;
    public Animator loadingAnim;
    public Animator menuAnim;
    public Animator resetPanelAnim;
    private AudioManager audioManager;
    private PlayerCustom playerCustom;
    private ShopController shopController;
    private QuestController questController;
    private float graphicLevel;
    private float transitionTime = 1.25f;
    private bool gameLaunch;
    public bool isGoldChange;
    public bool isExpChange;
    public bool isSliderChange;
    private int AmountOfGoldBonusAd;


    private void Start()
    {
        Application.targetFrameRate = 60;

        audioManager = FindObjectOfType<AudioManager>();
        playerCustom = FindObjectOfType<PlayerCustom>();
        shopController = FindObjectOfType<ShopController>();
        questController = FindObjectOfType<QuestController>();
        AmountOfGoldBonusAd = 2654 + (42 * PlayerPrefs.GetInt("PlayerLevel"));

        foreach (Text levelText in levelText)
        {
            levelText.text = PlayerPrefs.GetInt("PlayerLevel").ToString();
        }
        foreach (Text highScoreText in highScoreText)
        {
            highScoreText.text = "HIGH SCORE " + PlayerPrefs.GetInt("HighScore").ToString();
        }
        foreach (Text goldText in goldText)
        {
            goldText.text = PlayerPrefs.GetInt("PlayerGold").ToString();
        }
        foreach (Text goldBonusAd in goldBonusAd)
        {
            goldBonusAd.text = "+ " + AmountOfGoldBonusAd.ToString();
        }
        foreach (Slider playerExpSlider in playerExpSlider)
        {
            if (PlayerPrefs.HasKey("PlayerExpToNextLvl"))
            {
                playerExpSlider.value = ((float)PlayerPrefs.GetInt("PlayerExp") / PlayerPrefs.GetInt("PlayerExpToNextLvl")) * 100;
            }
            else
            {
                PlayerPrefs.SetInt("PlayerExpToNextLvl", 100);
                playerExpSlider.value = 0;
            }
        }
        foreach (Text playerExpText in playerExpText)
        {
            playerExpText.text = PlayerPrefs.GetInt("PlayerExp").ToString() + " Exp / " + PlayerPrefs.GetInt("PlayerExpToNextLvl").ToString() + " Exp";
        }

        if (PlayerPrefs.HasKey("GameMuted"))
        {
            if (PlayerPrefs.GetInt("GameMuted") == 0)
            {
                MuteBtn.SetActive(true);
            }
            else
            {
                MuteBtn.SetActive(false);
                UnmuteBtn.SetActive(true);
            }
            MuteBtn.SetActive(true);
            PlayerPrefs.SetInt("GameMuted", 0);
        }
        else
        {
            MuteBtn.SetActive(true);
            PlayerPrefs.SetInt("GameMuted", 0);
        }
        playerLvlSlider.value = (float)PlayerPrefs.GetInt("PlayerLevel");
        totalPlayText.text = PlayerPrefs.GetInt("PlayerTotalPlay").ToString();
        totalGoldText.text = PlayerPrefs.GetInt("PlayerTotalGold").ToString();
        totalReviveText.text = PlayerPrefs.GetInt("PlayerTotalRevive").ToString();
        if (PlayerPrefs.HasKey("SetGraphic"))
        {
            graphicLevel = PlayerPrefs.GetFloat("SetGraphic");
        }
        else
        {
            graphicLevel = 1.0f;
        }
        if (PlayerPrefs.GetInt("PlayTuto") == 0)
        {
            toggleTuto.isOn = true;
        }
        else
        {
            toggleTuto.isOn = false;
        }
        settingGraphicSlider.value = graphicLevel;
        SetQuality(Mathf.RoundToInt(graphicLevel));
    }

    private void Update()
    {
        if (isGoldChange || isExpChange || isSliderChange)
        {
            GoldExpChange();
        }
    }
    public void StartGame()
    {
        if (!gameLaunch)
        {
            menuAnim.SetTrigger("EasterEgg");
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        gameLaunch = true;

        PlayerPrefs.SetInt("PlayerTotalPlay", PlayerPrefs.GetInt("PlayerTotalPlay") + 1);

        loadingAnim.SetTrigger("Load");

        audioManager.Stop("FunkMenu");
        audioManager.Stop("NewLevel");
        audioManager.Play("FunkTheme");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene("Level01");
    }
    IEnumerator ReloadMenu()
    {
        loadingAnim.SetTrigger("Load");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene("Menu");
    }

    public void BtnFacebook()
    {
        Application.OpenURL("https://www.facebook.com/SomAppsDev");
        questController.CheckSomApps();
    }
    public void BtnFacebookHelp()
    {
        Application.OpenURL("https://www.facebook.com/messages/t/107440100936424");
        questController.CheckSomApps();
    }
    public void BtnInsta()
    {
        Application.OpenURL("https://www.instagram.com/somappsdev");
        questController.CheckSomApps();
    }
    public void BtnSoundclound()
    {
        Application.OpenURL("https://soundcloud.com/somatib/funk");
        questController.CheckSomApps();
    }
    public void BtnEasterEgg()
    {
        menuAnim.SetTrigger("EasterEgg");
        audioManager.Play("NewLevel");
    }
    public void BtnToggleTuto()
    {
        if (toggleTuto.isOn == false)
        {
            PlayerPrefs.SetInt("PlayTuto", 1);
        }
        else
        {
            PlayerPrefs.SetInt("PlayTuto", 0);
        }
    }
    public void BtnShowLeaderboardUI()
    {
        PlayGamesController.ShowLeaderboardUI();
    }
    public void BtnShowAchievementUI()
    {
        PlayGamesController.ShowAchievementUI();
    }

    public void MuteVolume()
    {
        audioManager.MuteVolume();
        UnmuteBtn.SetActive(true);
        MuteBtn.SetActive(false);
        PlayerPrefs.SetInt("GameMuted", 1);
    }
    public void UnmuteVolume()
    {
        audioManager.UnmuteVolume();
        UnmuteBtn.SetActive(false);
        MuteBtn.SetActive(true);
        PlayerPrefs.SetInt("GameMuted", 0);
    }

    public void SetQuality(float qualityIndex)
    {
        QualitySettings.SetQualityLevel(Mathf.RoundToInt(qualityIndex));
        PlayerPrefs.SetFloat("SetGraphic", qualityIndex);
    }

    public void OpenResetPanel()
    {
        resetPanelAnim.SetBool("OpenResetPanel", true);
        resetPanel.SetActive(true);
    }

    public void CloseResetPanel()
    {
        resetPanelAnim.SetBool("OpenResetPanel", false);
        audioManager.Play("Denied");
    }

    public void TakeBonusReward()
    {
        PlayerPrefs.SetInt("PlayerGold", PlayerPrefs.GetInt("PlayerGold") + AmountOfGoldBonusAd);
        PlayerPrefs.SetInt("PlayerTotalGold", PlayerPrefs.GetInt("PlayerTotalGold") + AmountOfGoldBonusAd);
        questController.TakeGold(AmountOfGoldBonusAd);
        OnGoldChange();
        audioManager.Play("Gold");
    }


    public void ResetPlayer()
    {
        audioManager.Play("NewHSFX");
        resetPanelAnim.SetBool("OpenResetPanel", false);
        PlayerPrefs.DeleteAll();
        PlayGamesController.UnlockAchievement(GPGSIds.achievement_reset);
        StartCoroutine(ReloadMenu());
    }

    public void OnGoldChange()
    {
        isGoldChange = true;
        totalGoldText.text = PlayerPrefs.GetInt("PlayerTotalGold").ToString();
        shopController.playerGold = PlayerPrefs.GetInt("PlayerGold");
        shopController.Initialize();
    }

    public void OnExpChange()
    {
        isExpChange = true;
        isSliderChange = true;
        foreach (Text levelText in levelText)
        {
            levelText.text = PlayerPrefs.GetInt("PlayerLevel").ToString();
        }
        playerLvlSlider.value = (float)PlayerPrefs.GetInt("PlayerLevel");
    }

    private void GoldExpChange()
    {
        float lerp = 0.1f;
        foreach (Text goldText in goldText)
        {
            int gold = int.Parse(goldText.text);
            int goldTo = PlayerPrefs.GetInt("PlayerGold");
            gold = (int)Mathf.Lerp(gold, goldTo, lerp);
            goldText.text = gold.ToString();
            if (gold > (goldTo - (goldTo / 50)))
            {
                goldText.text = goldTo.ToString();
                isGoldChange = false;

            }

        }
        foreach (Slider playerExpSlider in playerExpSlider)
        {
            if (PlayerPrefs.HasKey("PlayerExpToNextLvl"))
            {
                float exp = playerExpSlider.value;
                float expTo = ((float)PlayerPrefs.GetInt("PlayerExp") / PlayerPrefs.GetInt("PlayerExpToNextLvl")) * 100;
                exp = Mathf.Lerp(exp, expTo, lerp);
                playerExpSlider.value = exp;
                if (playerExpSlider.value > (expTo - (expTo / 80)) && playerExpSlider.value < (expTo + (expTo / 80)))
                {
                    playerExpSlider.value = exp;
                    isSliderChange = false;
                }
            }
            else
            {
                PlayerPrefs.SetInt("PlayerExpToNextLvl", 100);
                playerExpSlider.value = 0;
            }
        }
        foreach (Text playerExpText in playerExpText)
        {
            string[] array = playerExpText.text.Split(' ');
            int exp = int.Parse(array[0]);
            int expTo = PlayerPrefs.GetInt("PlayerExp");
            int expNext = int.Parse(array[3]);
            int expNextTo = PlayerPrefs.GetInt("PlayerExpToNextLvl");
            exp = (int)Mathf.Lerp(exp, expTo, lerp);
            expNext = (int)Mathf.Lerp(expNext, expNextTo, lerp);
            playerExpText.text = exp.ToString() + " Exp / " + expNext.ToString() + " Exp";
            if (exp > (expTo - (expNextTo / 50)) && exp < (expTo + (expNextTo / 50)))
            {
                playerExpText.text = expTo.ToString() + " Exp / " + expNextTo.ToString() + " Exp";
                isExpChange = false;
            }
        }
    }

}
