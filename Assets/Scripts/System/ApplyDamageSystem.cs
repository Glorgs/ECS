using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

[UpdateAfter(typeof(TriggerSystem))]
public partial class ApplyDamageSystem : SystemBase
{
    public event Action<DisplayComponent> TakeDamageEvent;

    [BurstCompile]
    protected override void OnCreate()
    {
        RequireForUpdate<ApplyDamageComponent>();
    }


    [BurstCompile]
    protected override void OnUpdate()
    {
        var ecb = new EntityCommandBuffer(Allocator.Temp);

        foreach (var x in SystemAPI.Query<RefRO<ApplyDamageComponent>, RefRW<LifeComponent>, DisplayComponent>().WithEntityAccess())
        {
            x.Item2.ValueRW.CurrentHP -= x.Item1.ValueRO.DamageToApply;

            TakeDamageEvent?.Invoke(x.Item3);
            ecb.RemoveComponent<ApplyDamageComponent>(x.Item4);
        }

        ecb.Playback(EntityManager);
        ecb.Dispose();
    }
}
