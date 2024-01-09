using Unity.Entities;
using UnityEngine;

public class SpawnProjectileAuthoring : MonoBehaviour
{
    public GameObject Prefab;

    private class Baker : Baker<SpawnProjectileAuthoring>
    {
        public override void Bake(SpawnProjectileAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new SpawnerProjectileComponent
            {
                Projectile = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic)
            });
        }
    }
}


public partial struct SpawnerProjectileComponent : IComponentData
{
    public Entity Projectile;
}