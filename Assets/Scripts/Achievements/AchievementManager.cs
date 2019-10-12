﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    private Achievement[] achievements;
    public Achievement[] Achievements => achievements; 

    // Start is called before the first frame update
    void Awake()
    {
        this.achievements = gameObject.GetComponentsInChildren<Achievement>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var achievement in achievements)   {
            if (!achievement.Done)  {
                achievement.AchievementUpdate();
                if (achievement.Done)   {
                    this.TriggerAchievement(achievement);
                }
            }
        }
    }

    // Displays achievement when it is earned
    public void TriggerAchievement(Achievement achievement)
    {
        //throw new NotImplementedException();
    }
}
