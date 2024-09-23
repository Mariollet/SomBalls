using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private LevelPanel levelPanel;
    [SerializeField] private GameManager gameManager;
    private void Awake()
    {
        LevelSystem levelSystem = new LevelSystem();
        levelPanel.SetLevelSystem(levelSystem);
        gameManager.SetLevelSystem(levelSystem);

        LevelSystemAnimated levelSystemAnimated = new LevelSystemAnimated(levelSystem);
        levelPanel.SetLevelSystemAnimated(levelSystemAnimated);
        gameManager.SetLevelSystemAnimated(levelSystemAnimated);
    }
}
