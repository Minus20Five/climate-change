using UnityEngine;

namespace World.Entities
{
    [CreateAssetMenu]
    public class EntityFactory : ScriptableObject
    {
        [SerializeField] private ForestEntity forestPrefab;
        [SerializeField] private HouseEntity housePrefab;
        [SerializeField] private TownHallEntity townHallPreFab;
        [SerializeField] private FactoryEntity factoryEntity;
        [SerializeField] private FarmEntity farmEntity;

        [SerializeField] private PowerStationEntity powerStationEntity;
        public Entity Get (EntityType entityType)
        {
            Entity entity = GetPrefab(entityType);
            return Instantiate(entity);
        }

        public int GetCost(EntityType entityType)
        {
            return GetPrefab(entityType).Stats.cost;
        }
        
        public Entity GetPrefab(EntityType entityType)
        {
            switch (entityType)
            {
                case EntityType.Forest:
                    return forestPrefab;
                case EntityType.House:
                    return housePrefab;
                case EntityType.TownHall:
                    return townHallPreFab;
                case EntityType.Factory:
                    return factoryEntity;
                case EntityType.Farm:
                    return farmEntity;
                case EntityType.PowerStation:
                    return powerStationEntity;
                default:
                    Debug.Log ("Unknown entity type");
                    return null;
            }
        }
    }
}