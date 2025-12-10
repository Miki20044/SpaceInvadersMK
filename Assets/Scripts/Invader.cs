using UnityEngine;

public class Invader : MonoBehaviour
{
    [Header("Animation")]
    public Sprite[] animationSprites;
    public float animationTime = 1.0f;

    [Header("Score")]
    public int points = 10; // default, overridden by row

    private SpriteRenderer _spriteRenderer;
    private int _animationFrame;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), animationTime, animationTime);
    }

    private void AnimateSprite()
    {
        _animationFrame++;
        if (_animationFrame >= animationSprites.Length)
            _animationFrame = 0;

        _spriteRenderer.sprite = animationSprites[_animationFrame];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy bunkers on contact
        if (other.CompareTag("Bunker"))
        {
            Destroy(other.gameObject);
        }

        // Kill player on contact
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(9999); // kills player instantly
            }
        }
    }
}
