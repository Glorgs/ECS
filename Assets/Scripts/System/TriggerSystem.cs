using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;


[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
[UpdateAfter(typeof(PhysicsSystemGroup))]
public partial struct TriggerSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<ProjectileSpeedComponent>();
        state.RequireForUpdate<SimulationSingleton>();
    }


    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        SimulationSingleton simulation = SystemAPI.GetSingleton<SimulationSingleton>();

        state.Dependency = new ProjectileHitJob
        {
            Positions = SystemAPI.GetComponentLookup<LocalTransform>(true),
            Projectiles = SystemAPI.GetComponentLookup<ProjectileSpeedComponent>(),
            Obstacles = SystemAPI.GetComponentLookup<ObstacleTag>(),
            Lifes = SystemAPI.GetComponentLookup<LifeComponent>(),
            ECB = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged)

        }.Schedule(simulation, state.Dependency);
    }

    public struct ProjectileHitJob : ITriggerEventsJob
    {
        [ReadOnly] public ComponentLookup<LocalTransform> Positions;
        public ComponentLookup<ProjectileSpeedComponent> Projectiles;
        public ComponentLookup<ObstacleTag> Obstacles;
        public ComponentLookup<LifeComponent> Lifes;

        public EntityCommandBuffer ECB;

        public void Execute(TriggerEvent triggerEvent)
        {
            Entity projectile = Entity.Null;
            Entity obstacle = Entity.Null;

            UnityEngine.Debug.LogError($"A={triggerEvent.EntityA} & B={triggerEvent.EntityB}");

            // Identiy which entity is which
            if (Projectiles.HasComponent(triggerEvent.EntityA))
                projectile = triggerEvent.EntityA;
            if (Projectiles.HasComponent(triggerEvent.EntityB))
                projectile = triggerEvent.EntityB;
            if (Obstacles.HasComponent(triggerEvent.EntityA))
                obstacle = triggerEvent.EntityA;
            if (Obstacles.HasComponent(triggerEvent.EntityB))
                obstacle = triggerEvent.EntityB;

            UnityEngine.Debug.LogError($"projectile={projectile} & obstacle={obstacle}");

            // if its a pair of entity we don't want to process, exit
            if (Entity.Null.Equals(projectile)
                || Entity.Null.Equals(obstacle)) return;

            if (Lifes.HasComponent(obstacle))
            {

                ApplyDamageComponent damageComponent = new ApplyDamageComponent
                {
                    DamageToApply = 5
                };

                ECB.AddComponent<ApplyDamageComponent>(obstacle, damageComponent);
            }

            ECB.DestroyEntity(projectile);
        }

    }
}
