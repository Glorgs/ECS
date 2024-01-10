using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;


[BurstCompile]
public partial struct SpeedProjectileSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<ProjectileSpeedComponent>();
    }

    public void OnUpdate(ref SystemState state)
    {
        foreach (var x in SystemAPI.Query<RefRO<ProjectileSpeedComponent>, RefRW<LocalTransform>>())
        {
            x.Item2.ValueRW = x.Item2.ValueRW.Translate(x.Item1.ValueRO.Direction * x.Item1.ValueRO.Speed * SystemAPI.Time.DeltaTime);
        }
    }
}
