using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Extensions;
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
        foreach (var x in SystemAPI.Query<RefRO<SpawnerProjectileComponent>, RefRO<LocalTransform>>())
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Entity projectile = EntityManager.Instantiate(x.Item1.ValueRO.Projectile);

                if (SystemAPI.HasComponent<LocalTransform>(projectile))
                {
                    RefRW<LocalTransform> transform = SystemAPI.GetComponentRW<LocalTransform>(projectile);
                    transform.ValueRW = transform.ValueRO.RotateX(-90);

                    if (SystemAPI.HasComponent<PhysicsVelocity>(projectile))
                    {
                        if (SystemAPI.HasComponent<PhysicsMass>(projectile))
                        {
                            RefRO<PhysicsMass> mass = SystemAPI.GetComponentRO<PhysicsMass>(projectile);
                            RefRW<PhysicsVelocity> velocity = SystemAPI.GetComponentRW<PhysicsVelocity>(projectile);
                            velocity.ValueRW.ApplyLinearImpulse(mass.ValueRO, transform.ValueRO.Forward() * 20);
                        }
                    }
                }
            }
        }
    }
}
