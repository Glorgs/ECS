using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial class SpawnerProjectileSystem : SystemBase
{

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SpawnerProjectileComponent>();
    }

    protected override void OnUpdate()
    {
        var ecb = new EntityCommandBuffer(Allocator.Temp);

        foreach (var x in SystemAPI.Query<RefRO<SpawnerProjectileComponent>, RefRO<LocalTransform>>())
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Entity projectile = EntityManager.Instantiate(x.Item1.ValueRO.Projectile);

                LocalTransform transform = SystemAPI.GetComponent<LocalTransform>(projectile);

                ecb.AddComponent(projectile, new ProjectileSpeedComponent { Direction = -transform.Forward(), Speed = 5f });
            }
        }

        ecb.Playback(EntityManager);
        ecb.Dispose();
    }
}
