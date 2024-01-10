
using Unity.Entities;
using Unity.Mathematics;

public partial struct ProjectileSpeedComponent : IComponentData
{
    public float Speed;
    public float3 Direction;
}