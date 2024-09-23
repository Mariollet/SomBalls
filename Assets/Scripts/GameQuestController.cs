using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameQuestController : MonoBehaviour
{
    public Quest01 quest01;
    public Quest02 quest02;
    public Quest03 quest03;
    public List<Text> titleText;
    public List<Text> goldText;
    public List<Text> expText;
    public List<Image> questImg;
    public List<Text> questProgress;
    public List<Slider> questSlider;
    public List<GameObject> questRewardImg;
    public List<GameObject> questRewardBtn;
    public Sprite[] imgQuest;

    private void Start()
    {
        Quest01 quest01 = new Quest01();
        Quest02 quest02 = new Quest02();
        Quest03 quest03 = new Quest03();
        this.quest01 = quest01;
        this.quest02 = quest02;
        this.quest03 = quest03;
    }
    public void TakeBulletTime()
    {
        quest01.TakeBulletTime();
        quest02.TakeBulletTime();
        quest03.TakeBulletTime();
        Debug.Log("quest Add BulletTime");
    }
    public void TakeHighScore(int score)
    {
        quest01.TakeHighScore(score);
        quest02.TakeHighScore(score);
        quest03.TakeHighScore(score);
        Debug.Log("quest Add HighScore");
    }
    public void TakeRevive()
    {
        quest01.TakeRevive();
        quest02.TakeRevive();
        quest03.TakeRevive();
        Debug.Log("quest Add Revive");
    }
    public void TakeDoubleReward()
    {
        quest01.TakeDoubleReward();
        quest02.TakeDoubleReward();
        quest03.TakeDoubleReward();
        Debug.Log("quest Add DoubleReward");
    }
    public void TakeCrystal()
    {
        quest01.TakeCrystal();
        quest02.TakeCrystal();
        quest03.TakeCrystal();
        Debug.Log("Quest01 Type : " + quest01.questType);
        Debug.Log("Quest01 Type : " + quest01.GetTypeQuest());
        Debug.Log("Quest01 : " + quest01.currentAmount + " / " + quest01.requiredAmount);
        Debug.Log("Quest02 Type : " + quest02.questType);
        Debug.Log("quest02 : " + quest02.currentAmount + " / " + quest02.requiredAmount);
        Debug.Log("quest03 Type : " + quest03.questType);
        Debug.Log("quest03 : " + quest03.currentAmount + " / " + quest03.requiredAmount);
    }
    public void TakeGold(int gold)
    {
        quest01.TakeGold(gold);
        quest02.TakeGold(gold);
        quest03.TakeGold(gold);
        Debug.Log("Quest01 Type : " + quest01.questType);
        Debug.Log("Quest01 Type : " + quest01.GetTypeQuest());
        Debug.Log("Quest01 : " + quest01.currentAmount + " / " + quest01.requiredAmount);
        Debug.Log("Quest02 Type : " + quest02.questType);
        Debug.Log("quest02 : " + quest02.currentAmount + " / " + quest02.requiredAmount);
        Debug.Log("quest03 Type : " + quest03.questType);
        Debug.Log("quest03 : " + quest03.currentAmount + " / " + quest03.requiredAmount);
    }
    public void TakeExp(int exp)
    {
        quest01.TakeExp(exp);
        quest02.TakeExp(exp);
        quest03.TakeExp(exp);
        Debug.Log("Quest01 Type : " + quest01.questType);
        Debug.Log("Quest01 Type : " + quest01.GetTypeQuest());
        Debug.Log("Quest01 : " + quest01.currentAmount + " / " + quest01.requiredAmount);
        Debug.Log("Quest02 Type : " + quest02.questType);
        Debug.Log("quest02 : " + quest02.currentAmount + " / " + quest02.requiredAmount);
        Debug.Log("quest03 Type : " + quest03.questType);
        Debug.Log("quest03 : " + quest03.currentAmount + " / " + quest03.requiredAmount);
    }
    public void CheckLevel()
    {
        quest01.CheckLevel();
        quest02.CheckLevel();
        quest03.CheckLevel();
    }

    public void CheckQuest()
    {
        CheckLevel();

        titleText[0].text = quest01.title;
        if (quest01.questType == 41)
        {
            questImg[0].sprite = imgQuest[quest01.maxQuestypes - 1];
        }
        else
        {
            questImg[0].sprite = imgQuest[quest01.questType];
        }
        goldText[0].text = quest01.goldReward.ToString();
        expText[0].text = quest01.expReward.ToString();
        questProgress[0].text = quest01.currentAmount + " / " + quest01.requiredAmount;
        questSlider[0].maxValue = quest01.requiredAmount;
        questSlider[0].value = quest01.currentAmount;

        titleText[1].text = quest02.title;
        if (quest02.questType == 42)
        {
            questImg[1].sprite = imgQuest[quest02.maxQuestypes];
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
            questImg[2].sprite = imgQuest[quest03.maxQuestypes];
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
    }

}
