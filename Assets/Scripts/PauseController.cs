using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class PauseController : MonoBehaviour
{
    public bool GameIsPaused = false;
    public bool GameIsMute = false;
    public GameObject PauseMenuUI;
    public GameObject pauseBtn;
    public GameObject resumeBtn;
    public GameObject MuteBtn;
    public GameObject MuteBtn2;
    public GameObject UnmuteBtn;
    public GameObject UnmuteBtn2;
    [SerializeField] public GameObject bulletTimeBtn;
    private AudioManager audioManager;
    private GameManager gameManager;
    private IEnumerator pausedGame;
    private IEnumerator resumedGame;
    [SerializeField] public Animator uiControllerAnim;
    [SerializeField] public Animator pauseControllerAnim;


    void Start()
    {
        if (GameIsMute || PlayerPrefs.GetInt("GameMuted") == 1)
        {
            UnmuteBtn.SetActive(true);
            MuteBtn.SetActive(false);
        }
        else
        {
            UnmuteBtn.SetActive(false);
            MuteBtn.SetActive(true);
        }
    }

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Resume()
    {
        if (GameIsPaused)
        {
            pauseControllerAnim.SetBool("GamePaused", false);
            resumedGame = ResumedGame();
            StartCoroutine(resumedGame);
        }
    }

    public void Pause()
    {
        if(!GameIsPaused)
        {
            pauseBtn.SetActive(false);
            bulletTimeBtn.SetActive(false);
            PauseMenuUI.gameObject.SetActive(true);
            pauseControllerAnim.SetBool("GamePaused", true);
            audioManager.SlowDownPitch("FunkTheme");
            audioManager.DownVolume("FunkTheme");
            pausedGame = PausedGame();
            StartCoroutine(pausedGame);

        }
    }

    IEnumerator PausedGame()
    {
        GameIsPaused = true;
        yield return new WaitForSeconds(0.1f);
        resumeBtn.SetActive(true);
        Time.timeScale = 0f;
    }
    IEnumerator ResumedGame()
    {
        GameIsPaused = false;
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0.6f;
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0.7f;
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0.8f;
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0.9f;
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 1f;
        audioManager.FastUpPitch("FunkTheme");
        audioManager.UpVolume("FunkTheme");
        yield return new WaitForSeconds(0.2f);
        PauseMenuUI.gameObject.SetActive(false);
        if (gameManager.gameOver)
        {
            pauseBtn.SetActive(false);
            bulletTimeBtn.SetActive(false);
        }
        else
        {
            bulletTimeBtn.SetActive(true);
            pauseBtn.SetActive(true);
        }
    }

    public void MuteVolume()
    {
        audioManager.MuteVolume();
        GameIsMute = true;
        UnmuteBtn.SetActive(true);
        UnmuteBtn2.SetActive(true);
        MuteBtn.SetActive(false);
        MuteBtn2.SetActive(false);
        PlayerPrefs.SetInt("GameMuted", 1);
    }
    public void UnmuteVolume()
    {
        audioManager.UnmuteVolume();
        GameIsMute = false;
        UnmuteBtn.SetActive(false);
        UnmuteBtn2.SetActive(false);
        MuteBtn.SetActive(true);
        MuteBtn2.SetActive(true);
        PlayerPrefs.SetInt("GameMuted", 0);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseControllerAnim.SetBool("GamePaused", false);
        gameManager.Menu();
    }
}
