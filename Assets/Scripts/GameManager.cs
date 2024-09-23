using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private Text pausedHighScoreText;
    [SerializeField] private Text newHS;
    [SerializeField] private GameObject tube;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject doubleBonusBtn;
    [SerializeField] private GameObject doubleBonusTxt;
    [SerializeField] private GameObject newGold;
    [SerializeField] private GameObject newExp;
    [SerializeField] private GameObject muteBtn;
    [SerializeField] private GameObject unmuteBtn;
    [SerializeField] private GameObject pauseBtn;
    [SerializeField] private GameObject bulletTimeBtn;
    [SerializeField] private Text WinGoldTxt;
    [SerializeField] private Text WinExpTxt;
    private int goldAmount;
    private int expAmount;
    private int hsGoldAmount;
    private int hsExpAmount;
    [SerializeField] private Text hsGoldText;
    [SerializeField] private Text hsExpText;
    [SerializeField] private Text playerLevelTxt;
    [SerializeField] private Text totalgoldTxt;
    [SerializeField] private GameObject newHighScoreFx;
    [SerializeField] private GameObject newBonusFx;
    [SerializeField] private GameObject sliderTarget;
    [SerializeField] private Animator loadingAnim;
    [SerializeField] private Animator LvlUpAnim;
    [SerializeField] private Animator gameOverUiControllerAnim;
    [SerializeField] private PauseController pauseController;
    [SerializeField] private List<Transform> tubesList;
    private int tubeLvl;
    [HideInInspector] public int score;
    [HideInInspector] public bool gameOver;
    [HideInInspector] public bool playerSecondLife;
    [HideInInspector] public bool playerDoubleBonus;
    private bool newLvlOn;
    private float meters;
    private int highScore;
    private AudioManager audioManager;
    private LevelSystem levelSystem;
    private LevelSystemAnimated levelSystemAnimated;
    private GameQuestController gameQuestController;
    private AdsManager adsManager;
    private Transform randomTube;
    private Transform previousTube;

    private Vector3 firstEndPos;
    private Vector3 lastEndPos;
    private const float player_spawn_tube_distance = 450f;
    private Vector3 spawnStartTubePos = new Vector3(0, 6, 1509.7f);
    private Vector3 spawnTubePos = new Vector3(0, 0, 1009.7f);
    private Vector3 scaleChange = new Vector3(0.08f, 0.08f, 0f);
    private Vector3 respawnOffset = new Vector3(0, -5f, 20f);
    private float transitionTime = 1.25f;
    private IEnumerator waitForIt;
    private IEnumerator showGoldExp;

    void Start()
    {
        adsManager = FindObjectOfType<AdsManager>();
        gameQuestController = FindObjectOfType<GameQuestController>();
        audioManager = FindObjectOfType<AudioManager>();
        waitForIt = WaitForGoldExp();
        showGoldExp = ShowGoldExp();
        pausedHighScoreText.text = "HIGH SCORE " + PlayerPrefs.GetInt("HighScore").ToString();
        adsManager.SetGameQuestController();
    }

    void Awake()
    {
        lastEndPos = GameObject.Find("EndTube").transform.position;
        firstEndPos = GameObject.Find("StartTube").transform.position;
    }

    void Update()
    {
        // Update Score
        if (!gameOver)
        {
            meters = (player.position.z / 6);
            scoreText.text = (score + meters).ToString("0");
        }

        // Spawn Tube
        if (Vector3.Distance(player.transform.position, lastEndPos) < player_spawn_tube_distance)
        {
            SpawnNextTube(spawnStartTubePos + spawnTubePos * tubeLvl);
        }
    }

    private void SpawnNextTube(Vector3 spawnPos)
    {
        if (randomTube != null)
        {
            while (randomTube.name == previousTube.name)
            {
                randomTube = tubesList[Random.Range(0, tubesList.Count)];
            }
        }
        else
        {
            randomTube = tubesList[Random.Range(0, tubesList.Count)];
        }
        previousTube = randomTube;
        Transform newTube = Instantiate(randomTube, spawnPos, tube.transform.rotation);
        newTube.transform.localScale += scaleChange;
        scaleChange += new Vector3(0.08f, 0.08f, 0f);
        newTube.transform.SetParent(tube.transform);
        firstEndPos = lastEndPos;
        lastEndPos = newTube.Find("EndTube").position;
        tubeLvl++;
    }

    public void GameOver()
    {
        if (!PlayerPrefs.HasKey("AchievementLetsRoll"))
        {
            PlayGamesController.UnlockAchievement(GPGSIds.achievement_lets_roll);
            PlayerPrefs.SetInt("AchievementLetsRoll", 1);
        }

        Time.timeScale = 1f;
        gameOver = true;
        pauseBtn.SetActive(false);
        bulletTimeBtn.SetActive(false);
        if (pauseController.GameIsMute)
        {
            unmuteBtn.SetActive(true);
            muteBtn.SetActive(false);
        }
        else
        {
            unmuteBtn.SetActive(false);
            muteBtn.SetActive(true);
        }

        highScore = Mathf.RoundToInt(float.Parse(scoreText.text));
        highScoreText.text = "HIGH SCORE " + PlayerPrefs.GetInt("HighScore").ToString();
        gameQuestController.TakeHighScore(highScore);
        // NEW HIGH SCORE
        if (highScore > PlayerPrefs.GetInt("HighScore"))
        {
            PlayGamesController.PostToLeaderboard(highScore);
            if (PlayerPrefs.HasKey("HighScore"))
            {
                PlayGamesController.IncrementAchievement(GPGSIds.achievement_release_the_beast_i, 1);
                PlayGamesController.IncrementAchievement(GPGSIds.achievement_release_the_beast_ii, 1);
                PlayGamesController.IncrementAchievement(GPGSIds.achievement_release_the_beast_iii, 1);
            }
            PlayerPrefs.SetInt("HighScore", highScore);
            gameQuestController.TakeHighScore(highScore);
            StartCoroutine(ShowNewHS());
            hsGoldAmount = 500 + (22 * PlayerPrefs.GetInt("PlayerLevel"));
            hsExpAmount = 200 + (36 * PlayerPrefs.GetInt("PlayerLevel"));
            hsGoldText.text = "+" + hsGoldAmount.ToString();
            hsExpText.text = "+" + hsExpAmount.ToString();
            goldAmount += hsGoldAmount;
            expAmount += hsExpAmount;
        }

        goldAmount += Mathf.RoundToInt((highScore * 1.22f));
        expAmount += Mathf.RoundToInt((highScore / 3.22f));

        audioManager.SlowDownPitch("FunkTheme");
        gameOverUI.SetActive(true);
        if (playerDoubleBonus)
        {
            doubleBonusBtn.SetActive(false);
        }
        waitForIt = WaitForGoldExp();
        showGoldExp = ShowGoldExp();
        StartCoroutine(waitForIt);
    }

    public void DoubleBonus()
    {
        if (!playerDoubleBonus)
        {
            doubleBonusBtn.SetActive(false);
            doubleBonusTxt.SetActive(true);
            goldAmount *= 2;
            expAmount *= 2;
            StopCoroutine(waitForIt);
            showGoldExp = ShowGoldExp();
            StartCoroutine(showGoldExp);
            gameOverUiControllerAnim.Play("GameOver", 0, 4.4f);
            playerDoubleBonus = true;
        }
    }

    IEnumerator ShowNewHS()
    {
        audioManager.Play("NewHS");
        yield return new WaitForSeconds(1.6f);
        audioManager.Play("NewHSFX");
        highScoreText.text = "HIGH SCORE " + PlayerPrefs.GetInt("HighScore").ToString();
        newHS.gameObject.SetActive(true);
        Instantiate(newHighScoreFx, newHS.transform.position, Quaternion.identity, newHS.transform);
    }

    IEnumerator WaitForGoldExp()
    {
        yield return new WaitForSeconds(4.5f);
        StartCoroutine(showGoldExp);
    }

    IEnumerator ShowGoldExp()
    {
        WinGoldTxt.text = "+" + goldAmount.ToString();
        WinExpTxt.text = "+" + expAmount.ToString();
        LvlUpAnim.SetTrigger("NewGold");
        Instantiate(newBonusFx, newGold.transform.position, Quaternion.identity, newGold.transform);
        audioManager.Play("Gold");
        newGold.gameObject.SetActive(true);
        levelSystem.AddGold(goldAmount);
        gameQuestController.TakeGold(goldAmount);
        yield return new WaitForSeconds(0.5f);
        Instantiate(newBonusFx, newExp.transform.position, Quaternion.identity, newExp.transform);
        audioManager.Play("Exp");
        newExp.gameObject.SetActive(true);
        levelSystem.AddExperience(expAmount);
        gameQuestController.TakeExp(expAmount);
        gameQuestController.CheckLevel();
    }

    IEnumerator NewLevel()
    {
        newLvlOn = true;
        yield return new WaitForSecondsRealtime(3.95f);
        newLvlOn = false;
        audioManager.UpVolume("FunkTheme");
    }

    public void Menu()
    {
        audioManager.Stop("FunkTheme");
        audioManager.FastUpPitch("FunkTheme");
        StartCoroutine(LoadScene(0));
        audioManager.PlayLooping("FunkMenu");
    }
    public void Retry()
    {
        PlayerPrefs.SetInt("PlayerTotalPlay", PlayerPrefs.GetInt("PlayerTotalPlay") + 1);
        StartCoroutine(LoadScene(1));
    }

    public void Revive()
    {
        if (!playerSecondLife)
        {
            goldAmount = 0;
            expAmount = 0;
            PlayerPrefs.SetInt("PlayerTotalRevive", PlayerPrefs.GetInt("PlayerTotalRevive") + 1);
            audioManager.FastUpPitch("FunkTheme");
            newGold.gameObject.SetActive(false);
            newExp.gameObject.SetActive(false);
            newHS.gameObject.SetActive(false);
            doubleBonusTxt.SetActive(false);
            gameOverUI.SetActive(false);
            pauseBtn.SetActive(true);
            bulletTimeBtn.SetActive(true);
            playerSecondLife = true;
            gameOver = false;
            Rigidbody playerRb = player.GetComponent<Rigidbody>();
            Collider playerCl = player.GetComponent<Collider>();
            playerCl.enabled = false;
            player.transform.position = firstEndPos + respawnOffset;
            playerCl.enabled = true;
            playerRb.velocity = Vector3.zero;
            playerRb.AddForce(new Vector3(0, 0, 1300), ForceMode.Impulse);
            Time.timeScale = 0.5f;
            StartCoroutine(SlowRevive());
        }
        else
        {
            audioManager.Play("Denied");
        }
    }

    IEnumerator SlowRevive()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.25f);
        Time.timeScale = 0.6f;
        yield return new WaitForSeconds(0.25f);
        Time.timeScale = 0.7f;
        yield return new WaitForSeconds(0.25f);
        Time.timeScale = 0.8f;
        yield return new WaitForSeconds(0.25f);
        Time.timeScale = 0.9f;
        yield return new WaitForSeconds(0.25f);
        Time.timeScale = 1f;
    }

    public void BtnShowLeaderboardUI()
    {
        PlayGamesController.ShowLeaderboardUI();
    }

    public void BtnShowAchievementUI()
    {
        PlayGamesController.ShowAchievementUI();
    }

    IEnumerator LoadScene(int scene)
    {
        loadingAnim.SetTrigger("Load");

        yield return new WaitForSeconds(transitionTime);

        audioManager.Stop("FunkTheme");
        if (!gameOver)
        {
            audioManager.UpVolume("FunkTheme");
        }
        else
        {
            audioManager.FastUpPitch("FunkTheme");
        }
        if (newLvlOn)
        {
            audioManager.UpVolume("FunkTheme");
            audioManager.Stop("NewLevel");
        }
        if (scene == 1)
        {
            audioManager.Play("FunkTheme");
        }
        SceneManager.LoadScene(scene);
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;

        levelSystem.OnGoldChanged += LevelSystem_OnGoldChanged;
    }

    public void SetLevelSystemAnimated(LevelSystemAnimated levelSystemAnimated)
    {
        this.levelSystemAnimated = levelSystemAnimated;

        levelSystemAnimated.OnLevelChanged += LevelSystemAnimated_OnLevelChanged;
    }

    private void LevelSystem_OnGoldChanged(object sender, System.EventArgs e)
    {

    }

    private void LevelSystemAnimated_OnLevelChanged(object sender, System.EventArgs e)
    {
        if(gameOver)
        {
            LvlUpAnim.SetTrigger("LvlUp");
            audioManager.DownVolume("FunkTheme");
            audioManager.Play("NewLevel");
            Instantiate(newBonusFx, playerLevelTxt.transform.position, Quaternion.identity, playerLevelTxt.transform);
            Instantiate(newBonusFx, sliderTarget.transform.position, Quaternion.identity, sliderTarget.transform);
            StartCoroutine("NewLevel");
        }
    }
}