using Unity.Entities;
using UnityEngine;

public class DisplayAuthoring : MonoBehaviour
{
    public SpriteRenderer Renderer;

    private class Baker : Baker<DisplayAuthoring>
    {
        public override void Bake(DisplayAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            DisplayComponent display = new DisplayComponent();
            display.Renderer = authoring.Renderer;
            AddComponentObject(entity, display);
        }
    }
}


public class DisplayComponent : IComponentData
{
    public SpriteRenderer Renderer;
}