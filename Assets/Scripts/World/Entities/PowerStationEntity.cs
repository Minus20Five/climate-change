﻿using System;
using UnityEngine;

namespace World.Entities
{
  public class PowerStationEntity : Entity {
      [SerializeField] private EntityStats stats;
      [SerializeField] private EntityUpgradeCosts upgradeCosts;
      public override EntityUpgradeCosts UpgradeCosts => upgradeCosts;
      
      public override EntityStats Stats => stats;
      public override EntityType Type => EntityType.PowerStation;

      public override void Construct() {
          entityHelper.Construct(stats);
      }

      public override void Destruct() {
          entityHelper.Destruct(stats);
      }

  }

}
