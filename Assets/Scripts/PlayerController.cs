using UnityEngine;
using EZCameraShake;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    private BulletTimeController bulletTimeController;
    private AudioManager audioManager;
    public GameObject level;
    public ParticleSystem speedUpFx;
    public ParticleSystem speedDownFx;
    private GameObject player;
    private Rigidbody playerRb;
    private ScoreShake scoreShake;
    [SerializeField] private Animator uiControllerAnim;
    [SerializeField] public GameObject palmHitFx;
    [SerializeField] public GameObject scoreBonusFx;
    private GameQuestController gameQuestController;

    private Vector3 offset;
    private float screenWidth;
    public float speed;
    public float rotationSpeed;
    public float turnSpeed;
    private int inputLeft;
    private int inputRight;

    void Start()
    {
        player = this.gameObject;
        playerRb = player.GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
        bulletTimeController = FindObjectOfType<BulletTimeController>();
        gameQuestController = FindObjectOfType<GameQuestController>();
        offset = new Vector3(0, 1.25f, 0);
        scoreShake = GameObject.Find("Score").GetComponent<ScoreShake>();
        screenWidth = Screen.width;
        playerRb.AddForce(new Vector3(0, 0, 980), ForceMode.Impulse);
        //See tutorial
        if (PlayerPrefs.GetInt("PlayTuto") == 0)
        {
            uiControllerAnim.SetBool("PlayTuto", true);
        }
    }

    #region PC Control
#if UNITY_EDITOR
    private void Update()
    {
        // KEYBOARD CONTROLS
        if (!gameManager.gameOver)
        {
            MovePlayer();
            float horizontalInput = Input.GetAxis("Horizontal");
            if (Input.GetKeyDown("q"))
            {
                uiControllerAnim.SetBool("TutoLtoR", true);
                StartCoroutine(InputLeft());
            }
            if (Input.GetKeyDown("d"))
            {
                uiControllerAnim.SetBool("TutoRtoSM", true);
                StartCoroutine(InputRight());
            }
            if (inputLeft == 2 || inputRight == 2)
            {
                uiControllerAnim.SetBool("PlayTuto", false);
                uiControllerAnim.SetBool("TutoSMtoD", true);
                bulletTimeController.ActiveBulletTime();
                PlayerPrefs.SetInt("PlayTuto", 1);
            }
            if (Input.GetKeyDown("z"))
            {
                playerRb.AddForce(new Vector3(0, 0, 200), ForceMode.Impulse);
            }
            RotatePlayer(horizontalInput);
        }
    }
#endif
#endregion

    #region Android Control

    private void FixedUpdate()
    {
        // TOUCH CONTROLS
        if (!gameManager.gameOver)
        {
            MovePlayer();
            int i = 0;
            float direction = 0;

                //touch found
                if (i < Input.touchCount)
            {
                //check doubleTap for BulletTime
                if (Input.GetTouch(i).tapCount == 2)
                {
                    uiControllerAnim.SetBool("PlayTuto", false);
                    uiControllerAnim.SetBool("TutoSMtoD", true);
                    PlayerPrefs.SetInt("PlayTuto", 1);
                    bulletTimeController.ActiveBulletTime();
                }

                //loop over every touch found
                while (i < Input.touchCount)
                {
                    if (Input.GetTouch(i).position.x > screenWidth / 2)
                    {
                        //move right
                        uiControllerAnim.SetBool("TutoRtoSM", true);
                        direction = Mathf.Lerp(direction, 1, Time.fixedDeltaTime * rotationSpeed);
                        RotatePlayer(direction);
                    }
                    if (Input.GetTouch(i).position.x < screenWidth / 2)
                    {
                        //move left
                        uiControllerAnim.SetBool("TutoLtoR", true);
                        direction = Mathf.Lerp(direction, -1, Time.fixedDeltaTime * rotationSpeed);
                        RotatePlayer(direction);
                    }
                    ++i;
                }
            }
        }
    }

    #endregion

    private void RotatePlayer(float input)
    {
        Vector3 movement = new Vector3(input * turnSpeed, 0, 0);

        playerRb.AddForce(movement * Time.fixedDeltaTime * speed, ForceMode.Force);
        level.transform.Rotate(0, 0, -input * rotationSpeed * Time.fixedDeltaTime);
    }

    //MOVE PLAYER
    private void MovePlayer()
    {
        Vector3 roll = new Vector3(0, 0, 1f);
        playerRb.AddForce(roll * Time.fixedDeltaTime * speed, ForceMode.Acceleration);
    }

    public void PulsePlayer()
    {
        playerRb.AddForce(new Vector3(0, 0, 100), ForceMode.Acceleration);
    }

    //DETECT COLLISIONS
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Obstacles") && !gameManager.gameOver)
        {
            audioManager.Play("PalmHitFX");
            CameraShaker.Instance.ShakeOnce(6f, 6f, 0.5f, 1f);
            Instantiate(palmHitFx, player.transform.position, player.transform.rotation);
            uiControllerAnim.SetBool("TutoStop", true);
            uiControllerAnim.SetBool("PlayTuto", false);
            gameManager.GameOver();
        }
        if (other.collider.CompareTag("Obstacles"))
        {
            audioManager.Play("PalmHitFX");
            CameraShaker.Instance.ShakeOnce(6f, 6f, 0.5f, 1f);
            Instantiate(palmHitFx, player.transform.position, player.transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // TRIGGER FOR BONUS
        if (other.CompareTag("BonusScore") && !gameManager.gameOver)
        {
            Instantiate(scoreBonusFx, player.transform.position + offset, transform.rotation);
            Destroy(other.gameObject);
            gameManager.score += 25;
            scoreShake.ShakeIt();
            audioManager.Play("BonusScore");
            gameQuestController.TakeCrystal();
            PlayGamesController.IncrementAchievement(GPGSIds.achievement_crystals_farmer_i, 1);
            PlayGamesController.IncrementAchievement(GPGSIds.achievement_crystals_farmer_ii, 1);
            PlayGamesController.IncrementAchievement(GPGSIds.achievement_crystals_farmer_iii, 1);
        }

        //SPEED EFFECT
        if (other.CompareTag("SpeedUpZone") && !gameManager.gameOver)
        {
            Instantiate(scoreBonusFx, player.transform.position + offset, transform.rotation);
            speedUpFx.Play();
            playerRb.AddForce(new Vector3(0, 0, 250), ForceMode.Impulse);
            CameraShaker.Instance.ShakeOnce(5f, 5f, 1f, 0.5f);
            StartCoroutine(SpeedUpEffect());
        }
        if (other.CompareTag("SpeedDownZone") && !gameManager.gameOver)
        {
            Instantiate(scoreBonusFx, player.transform.position + offset, transform.rotation);
            speedDownFx.Play();
            playerRb.AddForce(new Vector3(0, 0, -310), ForceMode.Impulse);
            CameraShaker.Instance.ShakeOnce(4f, 4f, 0.2f, 1f);
            StartCoroutine(SpeedDownEffect());
        }
    }

    IEnumerator InputRight()
    {
        inputRight += 1;
        yield return new WaitForSeconds(0.2f);
        inputRight -= 1;
    }
    IEnumerator InputLeft()
    {
        inputLeft += 1;
        yield return new WaitForSeconds(0.2f);
        inputLeft -= 1;
    }
    IEnumerator SpeedUpEffect()
    {
        audioManager.Play("SpeedUp");
        yield return new WaitForSeconds(0.6f);
        speedUpFx.Stop();
    }
    IEnumerator SpeedDownEffect()
    {
        audioManager.Play("SpeedDown");
        yield return new WaitForSeconds(0.6f);
        speedDownFx.Stop();
    }
}
