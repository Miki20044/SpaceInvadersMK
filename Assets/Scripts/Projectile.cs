using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction = Vector3.up;
    public float speed = 10f;
    public float lifeTime = 3f;

    public System.Action destroyed;

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
        // Ignore player
        if (other.CompareTag("Player")) return;

        // Hit invader
        if (other.CompareTag("Invader"))
        {
            Invader invader = other.GetComponent<Invader>();
            if (invader != null)
            {
                ScoreManager.Instance.AddScore(invader.points);
            }

            Destroy(other.gameObject);
            destroyed?.Invoke();
            Destroy(gameObject);
            return;
        }

        // Hit invader laser
        if (other.CompareTag("InvaderLaser"))
        {
            Destroy(other.gameObject);
            destroyed?.Invoke();
            Destroy(gameObject);
            return;
        }

        destroyed?.Invoke();
        Destroy(gameObject);
    }
}
