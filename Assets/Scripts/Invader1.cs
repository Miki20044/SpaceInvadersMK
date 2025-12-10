using UnityEngine;

public class Invader1 : MonoBehaviour
{
    public int points = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if hit by a laser
        Projectile laser = other.GetComponent<Projectile>();

        if (laser != null)
        {
            Destroy(gameObject);
        }
    }
}