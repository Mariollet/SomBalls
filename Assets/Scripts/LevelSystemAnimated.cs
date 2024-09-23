using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class LevelSystemAnimated
{
    private LevelSystem levelSystem;
    private bool isAnimating;

    private int level;
    private int experience;
    private int experienceToNextlvl;
    private int gold;
    private float updateTimer;
    private float updateTimer2;
    private float updateTimerMax;
    private float updateTimerMax2;

    public event EventHandler OnGoldChanged;
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    public LevelSystemAnimated(LevelSystem levelSystem)
    {
        SetLevelSystem(levelSystem);
        updateTimerMax = 0f;
        updateTimerMax2 = -10f;
        FunctionUpdater.Create(() => Update());
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;

        level = levelSystem.GetlevelNumber();
        experience = levelSystem.GetExperience();
        experienceToNextlvl = levelSystem.GetExperienceToNextlvl();
        gold = levelSystem.GetTotalGold();

        levelSystem.OnGoldChanged += LevelSystem_OnGoldChanged;
        levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
    }

    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
    {
        isAnimating = true;
    }
    private void LevelSystem_OnExperienceChanged(object sender, System.EventArgs e)
    {
        isAnimating = true;
    }
    private void LevelSystem_OnGoldChanged(object sender, System.EventArgs e)
    {
        isAnimating = true;
    }

    private void Update()
    {
        if (isAnimating)
        {
            updateTimer += Time.deltaTime;
            if (updateTimer > updateTimerMax)
            {
                updateTimer -= updateTimerMax;
                UpdateAddGold();
            }
            updateTimer2 += Time.deltaTime;
            if (updateTimer2 > updateTimerMax2)
            {
                updateTimer2 -= updateTimerMax2;
                UpdateAddExperience();
            }
        }
    }

    private void UpdateAddGold()
    {
            if(gold < levelSystem.GetTotalGold())
            {
                AddGold();
            }
    }
    private void UpdateAddExperience()
    {

        if (level < levelSystem.GetlevelNumber())
        {
            AddExp();
        }
        else
        {
            if (experience < levelSystem.GetExperience())
            {
                AddExp();
            }
            else
            {
                isAnimating = false;
            }
        }

    }

    private void AddExp()
    {
        if (experience < levelSystem.GetExperience() - 12 && experience < experienceToNextlvl - 12)
        {
            experience += 11;
        }
        else
        {
            experience++;
        }
        if (experience >= experienceToNextlvl)
        {
            level++;
            experience = 0;
            experienceToNextlvl += 188 + level * 6;
            OnLevelChanged?.Invoke(this, EventArgs.Empty);
        }
        OnExperienceChanged?.Invoke(this, EventArgs.Empty);
    }
    private void AddGold()
    {
        if (gold < levelSystem.GetTotalGold() - 17)
        {
            gold += 16;
        }
        else
        {
            gold++;
        }
        OnGoldChanged?.Invoke(this, EventArgs.Empty);
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
