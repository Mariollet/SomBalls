using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestController : MonoBehaviour
{
    public Quest01 quest01;
    public Quest02 quest02;
    public Quest03 quest03;
    public MenuController menuController;
    private AudioManager audioManager;
    [SerializeField] private Animator LvlUpAnim;
    public List<Animator> questAnim;
    public List<Text> titleText;
    public List<Text> goldText;
    public List<Text> expText;
    public List<Image> questImg;
    public List<Text> questProgress;
    public List<Slider> questSlider;
    public List<GameObject> questRewardBtn;
    public List<GameObject> questRewardImg;
    public GameObject PlayerLevel;
    public GameObject rewardQuestFx;
    public Slider playerExpSlider;
    public Sprite[] imgQuest;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        Quest01 quest01 = new Quest01();
        Quest02 quest02 = new Quest02();
        Quest03 quest03 = new Quest03();
        this.quest01 = quest01;
        this.quest02 = quest02;
        this.quest03 = quest03;

        if (!PlayerPrefs.HasKey("TypeQuest01"))
        {
            quest01.SetQuest();
            quest02.SetQuest();
            quest03.SetQuest();
        }
        CheckQuest();
    }

    public void TakeQuest01Reward()
    {
        PlayerPrefs.SetInt("PlayerGold", PlayerPrefs.GetInt("PlayerGold") + quest01.goldReward);
        PlayerPrefs.SetInt("PlayerTotalGold", PlayerPrefs.GetInt("PlayerTotalGold") + quest01.goldReward);
        CheckGoldAchievment();
        IncreaseQuestAchievment();
        PlayerPrefs.SetInt("PlayerExp", PlayerPrefs.GetInt("PlayerExp") + quest01.expReward);
        LvlUpAnim.SetTrigger("NewGold");
        while (PlayerPrefs.GetInt("PlayerExp") >= PlayerPrefs.GetInt("PlayerExpToNextLvl"))
        {
            NewLevel();
            PlayerPrefs.SetInt("PlayerLevel", PlayerPrefs.GetInt("PlayerLevel") + 1);
            PlayerPrefs.SetInt("PlayerExp", PlayerPrefs.GetInt("PlayerExp") - PlayerPrefs.GetInt("PlayerExpToNextLvl"));
            PlayerPrefs.SetInt("PlayerExpToNextLvl", PlayerPrefs.GetInt("PlayerExpToNextLvl") + 188 + PlayerPrefs.GetInt("PlayerLevel") * 6);
            CheckLevel();
        }
        TakeGold(quest01.goldReward);
        TakeExp(quest01.expReward);
        menuController.OnGoldChange();
        menuController.OnExpChange();
        questRewardBtn[0].SetActive(false);
        questAnim[0].SetTrigger("FlipQuest");
        Instantiate(rewardQuestFx, questRewardBtn[0].transform.position, Quaternion.identity, questAnim[0].transform);
        audioManager.Play("Gold");
        audioManager.Play("Exp");
        StartCoroutine(TakeNewQuest01());
    }
    public void TakeQuest02Reward()
    {
        PlayerPrefs.SetInt("PlayerGold", PlayerPrefs.GetInt("PlayerGold") + quest02.goldReward);
        PlayerPrefs.SetInt("PlayerTotalGold", PlayerPrefs.GetInt("PlayerTotalGold") + quest02.goldReward);
        CheckGoldAchievment();
        IncreaseQuestAchievment();
        PlayerPrefs.SetInt("PlayerExp", PlayerPrefs.GetInt("PlayerExp") + quest02.expReward);
        LvlUpAnim.SetTrigger("NewGold");
        while (PlayerPrefs.GetInt("PlayerExp") >= PlayerPrefs.GetInt("PlayerExpToNextLvl"))
        {
            NewLevel();
            PlayerPrefs.SetInt("PlayerLevel", PlayerPrefs.GetInt("PlayerLevel") + 1);
            PlayerPrefs.SetInt("PlayerExp", PlayerPrefs.GetInt("PlayerExp") - PlayerPrefs.GetInt("PlayerExpToNextLvl"));
            PlayerPrefs.SetInt("PlayerExpToNextLvl", PlayerPrefs.GetInt("PlayerExpToNextLvl") + 188 + PlayerPrefs.GetInt("PlayerLevel") * 6);
            CheckLevel();
        }
        TakeGold(quest02.goldReward);
        TakeExp(quest03.expReward);
        menuController.OnGoldChange();
        menuController.OnExpChange();
        questRewardBtn[1].SetActive(false);
        questAnim[1].SetTrigger("FlipQuest");
        Instantiate(rewardQuestFx, questRewardBtn[1].transform.position, Quaternion.identity, questAnim[1].transform);
        audioManager.Play("Gold");
        audioManager.Play("Exp");
        StartCoroutine(TakeNewQuest02());
    }
    public void TakeQuest03Reward()
    {
        PlayerPrefs.SetInt("PlayerGold", PlayerPrefs.GetInt("PlayerGold") + quest03.goldReward);
        PlayerPrefs.SetInt("PlayerTotalGold", PlayerPrefs.GetInt("PlayerTotalGold") + quest03.goldReward);
        CheckGoldAchievment();
        IncreaseQuestAchievment();
        PlayerPrefs.SetInt("PlayerExp", PlayerPrefs.GetInt("PlayerExp") + quest03.expReward);
        LvlUpAnim.SetTrigger("NewGold");
        while (PlayerPrefs.GetInt("PlayerExp") >= PlayerPrefs.GetInt("PlayerExpToNextLvl"))
        {
            NewLevel();
            PlayerPrefs.SetInt("PlayerLevel", PlayerPrefs.GetInt("PlayerLevel") + 1);
            PlayerPrefs.SetInt("PlayerExp", PlayerPrefs.GetInt("PlayerExp") - PlayerPrefs.GetInt("PlayerExpToNextLvl"));
            PlayerPrefs.SetInt("PlayerExpToNextLvl", PlayerPrefs.GetInt("PlayerExpToNextLvl") + 188 + PlayerPrefs.GetInt("PlayerLevel") * 6);
            CheckLevel();
        }
        TakeGold(quest03.goldReward);
        TakeExp(quest03.expReward);
        menuController.OnGoldChange();
        menuController.OnExpChange();
        questRewardBtn[2].SetActive(false);
        questAnim[2].SetTrigger("FlipQuest");
        Instantiate(rewardQuestFx, questRewardBtn[2].transform.position, Quaternion.identity, questAnim[2].transform);
        audioManager.Play("Gold");
        audioManager.Play("Exp");
        StartCoroutine(TakeNewQuest03());
    }

    void NewLevel()
    {
        Instantiate(rewardQuestFx, PlayerLevel.transform.position, Quaternion.identity, PlayerLevel.transform);
        playerExpSlider.value = 0;
        int level = PlayerPrefs.GetInt("PlayerLevel");
        if (level > 5)
        {
            PlayGamesController.UnlockAchievement(GPGSIds.achievement_bronze_level, 100);
            PlayGamesController.UnlockAchievement(GPGSIds.achievement_silver_level, 50);
            PlayGamesController.UnlockAchievement(GPGSIds.achievement_gold_level, 33);
            PlayGamesController.UnlockAchievement(GPGSIds.achievement_platinum_level, 25);
            PlayGamesController.UnlockAchievement(GPGSIds.achievement_diamond_level, 20);
            PlayGamesController.UnlockAchievement(GPGSIds.achievement_master_level, 10);
        }
        if (level > 10)
        {
            PlayGamesController.UnlockAchievement(GPGSIds.achievement_silver_level, 100);
            PlayGamesController.UnlockAchievement(GPGSIds.achievement_gold_level, 66);
            PlayGamesController.UnlockAchievement(GPGSIds.achievement_platinum_level, 50);
            PlayGamesController.UnlockAchievement(GPGSIds.achievement_diamond_level, 40);
            PlayGamesController.UnlockAchievement(GPGSIds.achievement_master_level, 20);
        }
        if (level > 15)
        {
            PlayGamesController.UnlockAchievement(GPGSIds.achievement_gold_level, 100);
            PlayGamesController.UnlockAchievement(GPGSIds.achievement_platinum_level, 75);
            PlayGamesController.UnlockAchievement(GPGSIds.achievement_diamond_level, 60);
            PlayGamesController.UnlockAchievement(GPGSIds.achievement_master_level, 30);
        }
        if (level > 20)
        {
            PlayGamesController.UnlockAchievement(GPGSIds.achievement_platinum_level, 100);
            PlayGamesController.UnlockAchievement(GPGSIds.achievement_diamond_level, 80);
            PlayGamesController.UnlockAchievement(GPGSIds.achievement_master_level, 40);
        }
        if (level > 25)
        {
            PlayGamesController.UnlockAchievement(GPGSIds.achievement_diamond_level, 100);
            PlayGamesController.UnlockAchievement(GPGSIds.achievement_master_level, 50);
        }
        if (level > 50)
        {
            PlayGamesController.UnlockAchievement(GPGSIds.achievement_master_level, 100);
        }
        LvlUpAnim.SetTrigger("NewLevel");
        audioManager.Play("NewLevel");
    }

    IEnumerator TakeNewQuest01()
    {
        quest01.SetQuest();
        quest01 = new Quest01();
        yield return new WaitForSeconds(0.4f);
        audioManager.Play("SpeedUp");
        questRewardImg[0].SetActive(false);
        questSlider[0].gameObject.SetActive(true);
        CheckQuest();
    }
    IEnumerator TakeNewQuest02()
    {
        quest02.SetQuest();
        quest02 = new Quest02();
        yield return new WaitForSeconds(0.4f);
        audioManager.Play("SpeedUp");
        questRewardImg[1].SetActive(false);
        questSlider[1].gameObject.SetActive(true);
        CheckQuest();
    }
    IEnumerator TakeNewQuest03()
    {
        quest03.SetQuest();
        quest03 = new Quest03();
        yield return new WaitForSeconds(0.4f);
        audioManager.Play("SpeedUp");
        questRewardImg[2].SetActive(false);
        questSlider[2].gameObject.SetActive(true);
        CheckQuest();
    }
    public void TakeGoldBonus()
    {
        quest01.TakeGoldBonus();
        quest02.TakeGoldBonus();
        quest03.TakeGoldBonus();
        CheckQuest();
    }
    public void TakeGold(int gold)
    {
        quest01.TakeGold(gold);
        quest02.TakeGold(gold);
        quest03.TakeGold(gold);
    }
    public void TakeExp(int exp)
    {
        quest01.TakeExp(exp);
        quest02.TakeExp(exp);
        quest03.TakeExp(exp);
    }
    public void CheckSomApps()
    {
        quest02.CheckSomApps();
        CheckQuest();
    }
    public void CheckLevel()
    {
        quest01.CheckLevel();
        quest02.CheckLevel();
        quest03.CheckLevel();
    }

    public void CheckQuest()
    {
        if (!PlayerPrefs.HasKey("TypeQuest01"))
        {
            quest01.SetQuest();
            quest02.SetQuest();
            quest03.SetQuest();
        }
        CheckLevel();

        titleText[0].text = quest01.title;
        questImg[0].sprite = imgQuest[quest01.questType];
        goldText[0].text = quest01.goldReward.ToString();
        expText[0].text = quest01.expReward.ToString();
        questProgress[0].text = quest01.currentAmount + " / " + quest01.requiredAmount;
        questSlider[0].maxValue = quest01.requiredAmount;
        questSlider[0].value = quest01.currentAmount;

        titleText[1].text = quest02.title;
        if (quest02.questType == 41)
        {
            questImg[1].sprite = imgQuest[quest02.maxQuestypes];
        }
        else if (quest02.questType == 42)
        {
            questImg[1].sprite = imgQuest[quest02.maxQuestypes - 1];
        }
        else
        {
            questImg[1].sprite = imgQuest[quest02.questType];
        }
        goldText[1].text = quest02.goldReward.ToString();
        expText[1].text = quest02.expReward.ToString();
        questProgress[1].text = quest02.currentAmount + " / " + quest02.requiredAmount;
        questSlider[1].maxValue = quest02.requiredAmount;
        questSlider[1].value = quest02.currentAmount;

        titleText[2].text = quest03.title;
        if (quest03.questType == 43)
        {
            questImg[2].sprite = imgQuest[quest03.maxQuestypes - 1];
        }
        else
        {
            questImg[2].sprite = imgQuest[quest03.questType];
        }
        goldText[2].text = quest03.goldReward.ToString();
        expText[2].text = quest03.expReward.ToString();
        questProgress[2].text = quest03.currentAmount + " / " + quest03.requiredAmount;
        questSlider[2].maxValue = quest03.requiredAmount;
        questSlider[2].value = quest03.currentAmount;

        if (quest01.QuestIsReached())
        {
            questSlider[0].gameObject.SetActive(false);
            questRewardBtn[0].SetActive(true);
            questRewardImg[0].SetActive(true);
        }

        if (quest02.QuestIsReached())
        {
            questSlider[1].gameObject.SetActive(false);
            questRewardBtn[1].SetActive(true);
            questRewardImg[1].SetActive(true);
        }

        if (quest03.QuestIsReached())
        {
            questSlider[2].gameObject.SetActive(false);
            questRewardBtn[2].SetActive(true);
            questRewardImg[2].SetActive(true);
        }
        Debug.Log("Quest01 Type : " + quest01.questType);
        Debug.Log("Quest01 Type : " + quest01.GetTypeQuest());
        Debug.Log("Quest01 : " + quest01.currentAmount + " / " + quest01.requiredAmount);
        Debug.Log("Quest02 Type : " + quest02.questType);
        Debug.Log("quest02 : " + quest02.currentAmount + " / " + quest02.requiredAmount);
        Debug.Log("quest03 Type : " + quest03.questType);
        Debug.Log("quest03 : " + quest03.currentAmount + " / " + quest03.requiredAmount);
    }
    void CheckGoldAchievment()
    {
        int gold = PlayerPrefs.GetInt("PlayerGold");

        if (gold > 10000)
        {
            PlayGamesController.UnlockAchievement(GPGSIds.achievement_gold_miner_i);
        }
        if (gold > 25000)
        {
            PlayGamesController.UnlockAchievement(GPGSIds.achievement_gold_miner_ii);
        }
        if (gold > 50000)
        {
            PlayGamesController.UnlockAchievement(GPGSIds.achievement_gold_miner_iii);
        }
    }

    void IncreaseQuestAchievment()
    {
        PlayGamesController.IncrementAchievement(GPGSIds.achievement_quest_hunter_i, 1);
        PlayGamesController.IncrementAchievement(GPGSIds.achievement_quest_hunter_ii, 1);
        PlayGamesController.IncrementAchievement(GPGSIds.achievement_quest_hunter_iii, 1);
    }
}
