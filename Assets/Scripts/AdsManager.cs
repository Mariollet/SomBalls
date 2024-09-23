using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    private GameManager gameManager;
    private QuestController questController;
    private GameQuestController gameQuestController;
    private AudioManager audioManager;
    private MenuController menuController;
    private float launchAd;
    private string PlayStoreID = "3715841";
    private string appStoreID = "3715840";

    private string videoAd = "video";
    private string rewardedVideoAd = "rewardedVideo";

    public bool isTargetPlayStore;
    public bool isTestAd;
    private bool IsRewardVideo;
    private bool IsReviveVideo;
    private bool IsDoubleBonusVideo;

    [SerializeField] int AmountOfGoldBonusAd;

    private void Start()
    {
        InitializeAd();
        Advertisement.AddListener(this);
        menuController = FindObjectOfType<MenuController>();
        questController = FindObjectOfType<QuestController>();
        audioManager = FindObjectOfType<AudioManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void SetGameQuestController()
    {
        gameQuestController = FindObjectOfType<GameQuestController>();
    }

    private void InitializeAd()
    {
        if (isTargetPlayStore)
        {
            Advertisement.Initialize(PlayStoreID, isTestAd);
        }
        else
        {
            Advertisement.Initialize(appStoreID, isTestAd);
        }
    }

    public void PlayVideoAd()
    {
        launchAd = Random.Range(0f, 4f);
        if (launchAd <= 1f)
        {
            if (Advertisement.IsReady(videoAd))
            {
                Advertisement.Show(videoAd);
            }
            else
            {
                audioManager.Play("Denied");
                Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
            }
        }
    }
    public void PlayRewardedVideoAd()
    {
        if (Advertisement.IsReady(rewardedVideoAd))
        {
            IsRewardVideo = true;
            Advertisement.Show(rewardedVideoAd);
        }
        else
        {
            audioManager.Play("Denied");
            Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
        }
    }

    public void PlayReviveVideoAd()
    {
        if (Advertisement.IsReady(rewardedVideoAd) && !gameManager.playerSecondLife)
        {
            IsReviveVideo = true;
            Advertisement.Show(rewardedVideoAd);
        }
        else
        {
            audioManager.Play("Denied");
            Debug.Log("Revive video is not ready at the moment! Please try again later!");
        }
    }

    public void PlayDoubleBonusVideoAd()
    {
        if (Advertisement.IsReady(rewardedVideoAd) && !gameManager.playerDoubleBonus)
        {
            IsDoubleBonusVideo = true;
            Advertisement.Show(rewardedVideoAd);
        }
        else
        {
            audioManager.Play("Denied");
            Debug.Log("Double Bonus video is not ready at the moment! Please try again later!");
        }
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (IsRewardVideo)
        {
            // Define conditional logic for each ad completion status:
            if (showResult == ShowResult.Finished)
            {
                // Reward the user for watching the ad to completion.
                Debug.LogWarning("You get reward !");
                menuController.TakeBonusReward();
                questController.TakeGoldBonus();
                IsRewardVideo = false;
                Time.timeScale = 1f;
            }
            else if (showResult == ShowResult.Skipped)
            {
                // Do not reward the user for skipping the ad.
                Debug.LogWarning("You DONT get reward !");
                audioManager.Play("NewHSFX");
                IsRewardVideo = false;
                Time.timeScale = 1f;
            }
            else if (showResult == ShowResult.Failed)
            {
                Debug.LogWarning("The ad did not finish due to an error.");
                audioManager.Play("NewHSFX");
                IsRewardVideo = false;
                Time.timeScale = 1f;
            }
        }

        else if (IsReviveVideo)
        {
            // Define conditional logic for each ad completion status:
            if (showResult == ShowResult.Finished)
            {
                // Reward the user for watching the ad to completion.
                Debug.LogWarning("You get reward !");
                Time.timeScale = 1f;
                IsReviveVideo = false;
                gameManager.Revive();
                gameQuestController.TakeRevive();
            }
            else if (showResult == ShowResult.Skipped)
            {
                // Do not reward the user for skipping the ad.
                Debug.LogWarning("You DONT get reward !");
                Time.timeScale = 1f;
                IsReviveVideo = false;
                audioManager.Play("NewHSFX");
            }
            else if (showResult == ShowResult.Failed)
            {
                Debug.LogWarning("The ad did not finish due to an error.");
                Time.timeScale = 1f;
                IsReviveVideo = false;
                audioManager.Play("NewHSFX");
            }
        }

        else if (IsDoubleBonusVideo)
        {
            // Define conditional logic for each ad completion status:
            if (showResult == ShowResult.Finished)
            {
                // Reward the user for watching the ad to completion.
                Debug.LogWarning("You get reward !");
                Time.timeScale = 1f;
                IsDoubleBonusVideo = false;
                gameManager.DoubleBonus();
                gameQuestController.TakeDoubleReward();
            }
            else if (showResult == ShowResult.Skipped)
            {
                // Do not reward the user for skipping the ad.
                Debug.LogWarning("You DONT get reward !");
                Time.timeScale = 1f;
                IsDoubleBonusVideo = false;
                audioManager.Play("NewHSFX");
            }
            else if (showResult == ShowResult.Failed)
            {
                Debug.LogWarning("The ad did not finish due to an error.");
                Time.timeScale = 1f;
                IsDoubleBonusVideo = false;
                audioManager.Play("NewHSFX");
            }
        }
        else
        {
            // Define conditional logic for each ad completion status:
            if (showResult == ShowResult.Finished)
            {
                // Reward the user for watching the ad to completion.
                Debug.LogWarning("You get reward !");
                Time.timeScale = 1f;
            }
            else if (showResult == ShowResult.Skipped)
            {
                // Do not reward the user for skipping the ad.
                Debug.LogWarning("You DONT get reward !");
                Time.timeScale = 1f;
            }
            else if (showResult == ShowResult.Failed)
            {
                Debug.LogWarning("The ad did not finish due to an error.");
                Time.timeScale = 1f;
            }
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == rewardedVideoAd)
        {
            // Optional actions to take when the placement becomes ready(For example, enable the rewarded ads button)
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad
        Time.timeScale = 0f;
    }

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }
}
