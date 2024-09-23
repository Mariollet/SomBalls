using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Quest03
{
    public int questType;
    public string title;
    public int expReward;
    public int goldReward;
    public int requiredAmount;
    public int currentAmount;
    public Image questImg;

    public int maxQuestypes = 9;
    private int playerLvl;

    public Quest03()
    {
        questType = PlayerPrefs.GetInt("TypeQuest03");
        title = PlayerPrefs.GetString("TitleQuest03");
        expReward = PlayerPrefs.GetInt("ExpQuest03");
        goldReward = PlayerPrefs.GetInt("GoldQuest03");
        requiredAmount = PlayerPrefs.GetInt("RequiredQuest03");
        currentAmount = PlayerPrefs.GetInt("CurrentQuest03");
    }

    public void SetQuest()
    {
        playerLvl = PlayerPrefs.GetInt("PlayerLevel");
        while (questType == PlayerPrefs.GetInt("TypeQuest01") || questType == PlayerPrefs.GetInt("TypeQuest02") || questType == PlayerPrefs.GetInt("TypeQuest03"))
            {
            if (playerLvl >= 10)
            {
                questType = Random.Range(0, maxQuestypes);
            }
            else
            {
                questType = 43;
            }

        }
        switch (questType)
        {
            case 0:
                expReward = Random.Range(500, 1000) + playerLvl * 24;
                goldReward = Random.Range(400, 800) + playerLvl * 36;
                requiredAmount = Random.Range(1, 2);
                currentAmount = 0;
                if (requiredAmount > 1)
                {
                    title = "Revive " + requiredAmount + " Times";
                }
                else
                {
                    title = "Revive " + requiredAmount + " Time";

                }
                break;
            case 1:
                expReward = Random.Range(150, 400) + playerLvl * 24;
                goldReward = Random.Range(200, 1000) + playerLvl * 36;
                requiredAmount = 6 + playerLvl * Random.Range(1, 3);
                currentAmount = 0;
                title = "Take " + requiredAmount + " Crystals";
                break;
            case 2:
                expReward = Random.Range(400, 500) + playerLvl * 28;
                goldReward = Random.Range(200, 300) + playerLvl * 13;
                requiredAmount = Random.Range(200, 600) + playerLvl * 31;
                currentAmount = 0;
                title = "Win " + requiredAmount + " Gold";
                break;
            case 3:
                expReward = Random.Range(200, 300) + playerLvl * 13;
                goldReward = Random.Range(400, 600) + playerLvl * 28;
                requiredAmount = Random.Range(200, 600) + playerLvl * 31;
                currentAmount = 0;
                title = "Earn " + requiredAmount + " Exp";
                break;
            case 4:
                expReward = Random.Range(500, 1000) + playerLvl * 26;
                goldReward = Random.Range(400, 800) + playerLvl * 32;
                requiredAmount = Random.Range(1, 3);
                currentAmount = 0;
                title = "Get " + requiredAmount + " Double Reward";
                break;
            case 5:
                expReward = Random.Range(500, 1000) + playerLvl * 21;
                goldReward = Random.Range(400, 800) + playerLvl * 36;
                requiredAmount = Random.Range(1, 3);
                currentAmount = 0;
                title = "Watch " + requiredAmount + " Gold Bonus Ad";
                break;
            case 6:
                expReward = Random.Range(800, 1000) + playerLvl * 29;
                goldReward = Random.Range(400, 800) + playerLvl * 42;
                requiredAmount = PlayerPrefs.GetInt("HighScore");
                currentAmount = 0;
                title = "Beats Your Highscore";
                break;
            case 7:
                expReward = Random.Range(600, 800) + playerLvl * 31;
                goldReward = Random.Range(400, 600) + playerLvl * 22;
                requiredAmount = playerLvl + Random.Range(2, 4);
                currentAmount = 0;
                title = "Launch " + requiredAmount + " Bullet-Time";
                break;
            case 8:
                expReward = Random.Range(150, 400) + playerLvl * 24;
                goldReward = Random.Range(200, 1000) + playerLvl * 36;
                requiredAmount = playerLvl + Random.Range(1, 3);
                currentAmount = playerLvl;
                title = "Reach Level " + requiredAmount;
                break;
            case 43:
                expReward = 640;
                goldReward = 1260;
                requiredAmount = 10;
                currentAmount = playerLvl;
                title = "Reach Level " + requiredAmount;
                break;
        }
        PlayerPrefs.SetInt("TypeQuest03", questType);
        PlayerPrefs.SetString("TitleQuest03", title);
        PlayerPrefs.SetInt("ExpQuest03", expReward);
        PlayerPrefs.SetInt("GoldQuest03", goldReward);
        PlayerPrefs.SetInt("RequiredQuest03", requiredAmount);
        PlayerPrefs.SetInt("CurrentQuest03", currentAmount);
    }
    public void TakeRevive()
    {
        if (GetTypeQuest() == 0)
        {
            currentAmount = GetCurrentQuest();
            requiredAmount = GetRequiredQuest();
            currentAmount++;
            PlayerPrefs.SetInt("CurrentQuest03", currentAmount);
            if (currentAmount > requiredAmount)
            {
                currentAmount = requiredAmount;
            }
        }
    }
    public void TakeBulletTime()
    {
        if (GetTypeQuest() == 7)
        {
            currentAmount = GetCurrentQuest();
            requiredAmount = GetRequiredQuest();
            currentAmount++;
            PlayerPrefs.SetInt("CurrentQuest03", currentAmount);
            if (currentAmount > requiredAmount)
            {
                currentAmount = requiredAmount;
            }
        }
    }
    public void TakeHighScore(int score)
    {
        if (GetTypeQuest() == 6)
        {
            if (score > currentAmount)
            {
                currentAmount = score;
                requiredAmount = GetRequiredQuest();
                PlayerPrefs.SetInt("CurrentQuest03", currentAmount);
                if (currentAmount > requiredAmount)
                {
                    currentAmount = requiredAmount;
                }
            }
        }
    }
    public void TakeGoldBonus()
    {
        if (GetTypeQuest() == 5)
        {
            currentAmount = GetCurrentQuest();
            requiredAmount = GetRequiredQuest();
            currentAmount++;
            PlayerPrefs.SetInt("CurrentQuest03", currentAmount);
            if (currentAmount > requiredAmount)
            {
                currentAmount = requiredAmount;
            }
        }
    }
    public void TakeDoubleReward()
    {
        if (GetTypeQuest() == 4)
        {
            currentAmount = GetCurrentQuest();
            requiredAmount = GetRequiredQuest();
            currentAmount++;
            PlayerPrefs.SetInt("CurrentQuest03", currentAmount);
            if (currentAmount > requiredAmount)
            {
                currentAmount = requiredAmount;
            }
        }
    }
    public void TakeCrystal()
    {
        if (GetTypeQuest() == 1)
        {
            currentAmount = GetCurrentQuest();
            requiredAmount = GetRequiredQuest();
            currentAmount++;
            PlayerPrefs.SetInt("CurrentQuest03", currentAmount);
            if (currentAmount > requiredAmount)
            {
                currentAmount = requiredAmount;
            }
        }
    }
    public void TakeGold(int gold)
    {
        if (GetTypeQuest() == 2)
        {
            currentAmount = GetCurrentQuest();
            requiredAmount = GetRequiredQuest();
            currentAmount += gold;
            PlayerPrefs.SetInt("CurrentQuest03", currentAmount);
            if (currentAmount > requiredAmount)
            {
                currentAmount = requiredAmount;
            }
        }
    }
    public void TakeExp(int exp)
    {
        if (GetTypeQuest() == 3)
        {
            currentAmount = GetCurrentQuest();
            requiredAmount = GetRequiredQuest();
            currentAmount += exp;
            PlayerPrefs.SetInt("CurrentQuest03", currentAmount);
            if (currentAmount > requiredAmount)
            {
                PlayerPrefs.SetInt("CurrentQuest03", requiredAmount);
            }
        }
    }
    public void CheckLevel()
    {
        if (GetTypeQuest() == maxQuestypes - 1 || GetTypeQuest() == 43)
        {
            currentAmount = PlayerPrefs.GetInt("PlayerLevel");
            requiredAmount = GetRequiredQuest();
            PlayerPrefs.SetInt("CurrentQuest03", PlayerPrefs.GetInt("PlayerLevel"));
            if (currentAmount > requiredAmount)
            {
                PlayerPrefs.SetInt("CurrentQuest03", requiredAmount);
            }
        }
    }

    public bool QuestIsReached()
    {
        return (currentAmount >= requiredAmount);
    }
    public int GetTypeQuest()
    {
        return PlayerPrefs.GetInt("TypeQuest03");
    }
    public int GetTitleQuest()
    {
        return PlayerPrefs.GetInt("TitleQuest03");
    }
    public int GetExpQuest()
    {
        return PlayerPrefs.GetInt("ExpQuest03");
    }
    public int GetGoldQuest()
    {
        return PlayerPrefs.GetInt("GoldQuest03");
    }
    public int GetCurrentQuest()
    {
        return PlayerPrefs.GetInt("CurrentQuest03");
    }
    public int GetRequiredQuest()
    {
        return PlayerPrefs.GetInt("RequiredQuest03");
    }
}