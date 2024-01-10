using Unity.Entities;
using UnityEngine;

public class LifeAuthoring : MonoBehaviour
{
    public int MaxHP;
    public int CurrentHP;

    public class Baker : Baker<LifeAuthoring>
    {
        public override void Bake(LifeAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new LifeComponent
            {
                MaxHP = authoring.MaxHP,
                CurrentHP = authoring.MaxHP
            });
        }
    }
}


public struct LifeComponent : IComponentData
{
    public int MaxHP;
    public int CurrentHP;
}