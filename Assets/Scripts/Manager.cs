using Unity.Entities;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        ApplyDamageSystem system = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<ApplyDamageSystem>();
        system.TakeDamageEvent += OnTakeDamage;
    }

    private void OnTakeDamage(DisplayComponent c)
    {

        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);

        spriteRenderer.color = new Color(r, g, b);
        c.Renderer.color = new Color(r, g, b);
    }
}
