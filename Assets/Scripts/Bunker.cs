using UnityEngine;

public class Bunker : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    private Collider2D _collider;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        ResetBunker();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            _renderer.enabled = false;  // hide the sprite
            if (_collider != null)
                _collider.enabled = false; // disable collisions
        }
    }

    public void ResetBunker()
    {
        currentHealth = maxHealth;
        if (_renderer != null)
            _renderer.enabled = true;      // make sprite visible
        if (_collider != null)
            _collider.enabled = true;      // enable collisions
    }
}