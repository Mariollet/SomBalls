using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanel : MonoBehaviour
{
    [SerializeField] private Text levelText;
    [SerializeField] private Slider experienceBarImg;
    [SerializeField] private Text experienceBarTxt;
    [SerializeField] private Text totalgoldTxt;
    private LevelSystem levelSystem;
    private LevelSystemAnimated levelSystemAnimated;

    private void Awake()
    {
        levelText.text = PlayerPrefs.GetInt("PlayerLevel").ToString();
        totalgoldTxt.text = PlayerPrefs.GetInt("PlayerGold").ToString();
    }

    private void SetExperienceBarValue(int experience, int experienceToNextLvl, float experienceNormalized)
    {
        experienceBarImg.value = experienceNormalized * 100;
        experienceBarTxt.text = experience.ToString() + " Exp / " + experienceToNextLvl.ToString() + " Exp";
    }

    private void SetLevelNumber(int levelNumber)
    {
        levelText.text = levelNumber.ToString();
    }

    private void SetTotalGold(int gold)
    {
        totalgoldTxt.text = gold.ToString();
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;
    }

    public void SetLevelSystemAnimated(LevelSystemAnimated levelSystemAnimated)   
    { 
        this.levelSystemAnimated = levelSystemAnimated;

        SetLevelNumber(levelSystemAnimated.GetlevelNumber());
        SetTotalGold(levelSystemAnimated.GetTotalGold());
        SetExperienceBarValue(levelSystemAnimated.GetExperience(), levelSystemAnimated.GetExperienceToNextlvl(), levelSystemAnimated.GetExperienceNormalized());

        levelSystemAnimated.OnGoldChanged += LevelSystemAnimated_OnGoldChanged;
        levelSystemAnimated.OnExperienceChanged += LevelSystemAnimated_OnExperienceChanged;
        levelSystemAnimated.OnLevelChanged += LevelSystemAnimated_OnLevelChanged;
    }

    private void LevelSystemAnimated_OnGoldChanged(object sender, System.EventArgs e)
    {
        SetTotalGold(levelSystemAnimated.GetTotalGold());
    }

    private void LevelSystemAnimated_OnExperienceChanged(object sender, System.EventArgs e)
    {
        SetExperienceBarValue(levelSystemAnimated.GetExperience(), levelSystemAnimated.GetExperienceToNextlvl(), levelSystemAnimated.GetExperienceNormalized());
    }
    private void LevelSystemAnimated_OnLevelChanged(object sender, System.EventArgs e)
    {
        SetLevelNumber(levelSystemAnimated.GetlevelNumber());
    }
}
