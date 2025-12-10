using UnityEngine;

public class InvaderLaser : MonoBehaviour
{
    public Vector3 direction = Vector3.down;
    public float speed = 7f;
    public float lifeTime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Laser vs Player laser
        if (other.CompareTag("PlayerLaser"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            return;
        }

        // Bunker takes damage
        if (other.CompareTag("Bunker"))
        {
            Bunker bunker = other.GetComponent<Bunker>();
            if (bunker != null)
            {
                bunker.TakeDamage(1);
            }

            Destroy(gameObject);
            return;
        }

        // Hit player
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(1); // <-- THIS WAS MISSING
            }

            Destroy(gameObject);
            return;
        }
    }
}