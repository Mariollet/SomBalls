using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem
{
    private int level;
    private int experience;
    private int experienceToNextlvl;
    private int gold;

    public event EventHandler OnGoldChanged;
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    public LevelSystem()
    {
        level = PlayerPrefs.GetInt("PlayerLevel");
        experience = PlayerPrefs.GetInt("PlayerExp");
        experienceToNextlvl = PlayerPrefs.GetInt("PlayerExpToNextLvl");
        gold = PlayerPrefs.GetInt("PlayerGold");
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        while (experience >= experienceToNextlvl)
        {
            level++;
            experience -= experienceToNextlvl;
            experienceToNextlvl += 188 + level * 6;
            OnLevelChanged?.Invoke(this, EventArgs.Empty);
            PlayerPrefs.SetInt("PlayerExpToNextLvl", experienceToNextlvl);
            PlayerPrefs.SetInt("PlayerLevel", level);
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
        }
        OnExperienceChanged?.Invoke(this, EventArgs.Empty);
        PlayerPrefs.SetInt("PlayerExp", experience);
    }
    public void AddGold(int amount)
    {
        gold += amount;
        OnGoldChanged?.Invoke(this, EventArgs.Empty);
        PlayerPrefs.SetInt("PlayerGold", gold);
        PlayerPrefs.SetInt("PlayerTotalGold", PlayerPrefs.GetInt("PlayerTotalGold") + gold);
        if (PlayerPrefs.GetInt("PlayerGold") > 10000)
        {
            PlayGamesController.UnlockAchievement(GPGSIds.achievement_gold_miner_i);
        }
        if (PlayerPrefs.GetInt("PlayerGold") > 25000)
        {
            PlayGamesController.UnlockAchievement(GPGSIds.achievement_gold_miner_ii);
        }
        if (PlayerPrefs.GetInt("PlayerGold") > 50000)
        {
            PlayGamesController.UnlockAchievement(GPGSIds.achievement_gold_miner_iii);
        }

    }

    public int GetlevelNumber()
    {
        return level;
    }

    public int GetExperience()
    {
        return experience;
    }

    public int GetTotalGold()
    {
        return gold;
    }
    public int GetExperienceToNextlvl()
    {
        return experienceToNextlvl;
    }
    
    public float GetExperienceNormalized()
    {
        return (float)experience / experienceToNextlvl;
    }
}
