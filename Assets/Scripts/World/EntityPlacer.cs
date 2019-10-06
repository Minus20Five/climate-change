﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using World;
using World.Entities;
using World.Resource;
using World.Tiles;

namespace World
{
    public class EntityPlacer : MonoBehaviour
    {

        [SerializeField] private EntityFactory factory;
        [SerializeField] private ResourceSingleton resources;
        private Entity entity;
        private Plane tilePlane;

        private void Start() {
            tilePlane = new Plane(Vector3.up, 0);
        }

        public void spawn(EntityType type)
        {
            entity = factory.Get(type);
            enabled = true;
        }

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Transform buildingTransform = entity.transform;
            if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, 1 << 10 ))
            {
                Debug.DrawLine (Camera.main.transform.position, hitInfo.point,  Color.red);
                GameObject gameTile = hitInfo.collider.gameObject;
                buildingTransform.SetParent(gameTile.transform);
                buildingTransform.localPosition = Vector3.zero;
                if (Input.GetMouseButtonDown(0))
                {
                    Tile tile = gameTile.GetComponent<Tile>();
                    if (tile.TileType.Equals(TileType.Grass) 
                        && tile.Entity == null
                        && resources.Money.CurAmount >= entity.State.cost) {
                        tile.Entity = entity;
                        enabled = false;
                    }
                }
            }
            else {
                tilePlane.Raycast(ray, out var enter);
                Vector3 hitPoint = ray.GetPoint(enter);
                buildingTransform.parent = null;
                buildingTransform.position = hitPoint;
            }
        }
    


    
}






}
