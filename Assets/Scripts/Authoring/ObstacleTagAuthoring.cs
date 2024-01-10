using Unity.Entities;
using UnityEngine;

public class ObstacleTagAuthoring : MonoBehaviour
{

    public class Baker : Baker<ObstacleTagAuthoring>
    {
        public override void Bake(ObstacleTagAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new ObstacleTag());
        }
    }
}


public struct ObstacleTag : IComponentData
{

}