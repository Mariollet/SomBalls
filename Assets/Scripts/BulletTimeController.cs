using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using GooglePlayGames.BasicApi.Multiplayer;

public class BulletTimeController : MonoBehaviour
{
    private bool isBulletTimeReady = true;
    private bool isBulletDownFill;
    private bool isBulletUpFill;
    private float fillAmount;
    private float maximum = 100f;
    private float minimum = 0f;
    private float current = 100f;
    public float btPower = 0.5f;
    public float btTimer = 0.5f;
    private float btTempValue = -40f;
    private float tempValue;
    private float chromaticValue;
    private float defaultTempValue;
    public Image mask;
    public Image bulletTimeLogo;
    public PostProcessVolume volume;
    private ColorGrading _Color;
    private ChromaticAberration _Chromatic;
    private AudioManager audioManager;
    private PlayerController playerController;
    private GameQuestController gameQuestController;
    private PauseController pauseController;
    [SerializeField] public Animator uiControllerAnim;
    [SerializeField] public GameObject BulletTimeReadyFx;
    [SerializeField] public GameObject BulletTimeFx;



    private float downFill = 4.2f;
    private float upFill = 5f;

    void Start()
    {
        volume.profile.TryGetSettings(out _Color);
        volume.profile.TryGetSettings(out _Chromatic);
        playerController = FindObjectOfType<PlayerController>();
        gameQuestController = FindObjectOfType<GameQuestController>();
        pauseController = FindObjectOfType<PauseController>();
        audioManager = FindObjectOfType<AudioManager>();
        defaultTempValue = _Color.temperature.value;
    }

    private void Update()
    {

        if (isBulletDownFill)
        {
            uiControllerAnim.SetBool("BulletTimeReady", false);
            GetCurrentFill();
            current = Mathf.Lerp(current, minimum, Time.deltaTime * downFill);
            tempValue = Mathf.Lerp(tempValue, btTempValue, Time.deltaTime * downFill);
            chromaticValue = Mathf.Lerp(0f, 1f, Time.deltaTime * 200f);
            GetCurrentTempValue();

            if (current < 0.5f)
            {
                current = 0f;
                GetCurrentFill();
                DeactivateBulletTime();
                playerController.PulsePlayer();
                isBulletUpFill = true;
                isBulletDownFill = false;
            }
        }
        if (isBulletUpFill && !pauseController.GameIsPaused)
        {
            Time.timeScale += 0.2f * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

            GetCurrentFill();
            current = Mathf.Lerp(current, maximum, Time.deltaTime / upFill);
            tempValue = Mathf.Lerp(tempValue, defaultTempValue, Time.deltaTime);
            chromaticValue = Mathf.Lerp(1f, 0f, Time.deltaTime * 200f);
            GetCurrentTempValue();

            if (current > 98f)
            {
                current = 100f;
                chromaticValue = 0f;
                tempValue = defaultTempValue;
                GetCurrentFill();
                isBulletTimeReady = true;
                uiControllerAnim.SetBool("BulletTimeReady", true);
                Instantiate(BulletTimeReadyFx, bulletTimeLogo.transform.position, Quaternion.identity, bulletTimeLogo.transform);
                isBulletUpFill = false;
            }
        }
    }

    public void ActiveBulletTime()
    {
        if (isBulletTimeReady)
        {
            isBulletTimeReady = false;
            Instantiate(BulletTimeFx, uiControllerAnim.transform.position, Quaternion.identity, uiControllerAnim.transform);
            isBulletDownFill = true;
            gameQuestController.TakeBulletTime();
            audioManager.SlowlyDownPitch("FunkTheme");
            playerController.turnSpeed += 10f;

            Time.timeScale = btPower;
        }
    }

    public void DeactivateBulletTime()
    {
        audioManager.FastUpPitch("FunkTheme");
        playerController.turnSpeed -= 10f;
    }

    void GetCurrentFill()
    {
        fillAmount = current / maximum;
        mask.fillAmount = fillAmount;
    }

    void GetCurrentTempValue()
    {
        _Color.temperature.value = tempValue;
        _Chromatic.intensity.value = chromaticValue;
        
    }
}
