using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class PlayGamesController : MonoBehaviour
{
    private static PlayGamesController instance;

    // Start is called before the first frame update
    void Start()
    {
        AuthenticateUser();

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void AuthenticateUser()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log("Logged in to Google Play Games Services");
            }
            else
            {
                Debug.Log("Unable to sign in to Google Play Games Services");
            }
        });
    }

    public static void ShowLeaderboardUI()
    {
        Social.ShowLeaderboardUI();
    }

    public static void ShowAchievementUI()
    {
        Social.ShowAchievementsUI();
    }

    public static void PostToLeaderboard(long newHighscore)
    {
        Social.ReportScore(newHighscore, GPGSIds.leaderboard_highscore, (bool success) =>
        {
            if (success)
            {
                Debug.Log("Posted new Highscore");
            }
            else
            {
                Debug.Log("Unable to post new Highscore to leaderboard !");
            }
        });
    }

    public static void UnlockAchievement(string id, int pourcent = 100)
    {
        Social.ReportProgress(id, pourcent, (bool success) => { });
    }

    public static void IncrementAchievement(string id, int stepsToIncrement)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(id,stepsToIncrement, (bool success) => { });
    }
}
