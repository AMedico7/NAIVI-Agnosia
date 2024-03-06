using UnityEngine;

public class PeriodicSpriteAnimation : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;

    private SpriteRenderer spriteRenderer;
    private bool isSprite1 = true;
    private float animationSpeed = 0.0f; // Default speed

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on the GameObject.");
        }

        // Set the initial sprite
        spriteRenderer.sprite = sprite1;

        // Start the periodic animation with adjustable speed
        InvokeRepeating("ToggleSprite", 0.0f, animationSpeed);
    }

    void ToggleSprite()
    {
        // Toggle between the two sprites
        if (isSprite1)
        {
            spriteRenderer.sprite = sprite2;
        }
        else
        {
            spriteRenderer.sprite = sprite1;
        }

        // Toggle the flag
        isSprite1 = !isSprite1;
    }

    // Public method to change animation speed dynamically
    public void ChangeAnimationSpeed(float newSpeed)
    {
        animationSpeed = Mathf.Max(newSpeed, 0.0f); // Ensure a minimum speed
        CancelInvoke("ToggleSprite");
        InvokeRepeating("ToggleSprite", 0.0f, animationSpeed);
    }
}
