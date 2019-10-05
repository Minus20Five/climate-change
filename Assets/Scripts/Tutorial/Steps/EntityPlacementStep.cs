﻿using System.Collections.Generic;
using UnityEngine;
using World;

namespace Tutorial.Steps
{
    [CreateAssetMenu(menuName="Tutorial Steps/Entity Placement Step")]
    public class EntityPlacementStep : TutorialStep
    {
        public EntityType entityToDetect;
        private GameBoard gameBoard;
        
        public EntityPlacementStep(string title, string description) : base(title, description)
        {
        }

        public override void OnStepBegin()
        {
            this.gameBoard = GameObject.FindGameObjectWithTag("Board").GetComponent<GameBoard>();
        }

        public override void Update()
        {
            if (this.gameBoard.IsEntityTypeOnBoard(entityToDetect))
            {
                this.stepCompleted = true;
            }
        }
    }
}