using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class StretchSpriteToCollider : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        BoxCollider2D bc = GetComponent<BoxCollider2D>();

        Vector2 size = bc.size;

        // Adjust scale based on sprite's original size
        Vector2 spriteSize = sr.sprite.bounds.size;

        Vector3 newScale = new Vector3(
            size.x / spriteSize.x,
            size.y / spriteSize.y,
            1f
        );

        transform.localScale = newScale;
    }
}