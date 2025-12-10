using UnityEngine;
using System.Collections;

public class InvaderShooting : MonoBehaviour
{
    [Header("Laser Settings")]
    public GameObject invaderLaserPrefab; // Assign in Inspector
    public float minShootTime = 5f;       // Min time between shots
    public float maxShootTime = 8f;       // Max time between shots

    private float nextShootTime;
    private Collider2D ownCollider;
    private bool hasActiveLaser = false;  // Prevent spamming

    private void Start()
    {
        ownCollider = GetComponent<Collider2D>();
        ScheduleNextShot();
    }

    private void Update()
    {
        if (Time.time >= nextShootTime)
        {
            Shoot();
            ScheduleNextShot();
        }
    }

    private void ScheduleNextShot()
    {
        nextShootTime = Time.time + Random.Range(minShootTime, maxShootTime);
    }

    private void Shoot()
    {
        // Only shoot if bottom-most and no active laser
        if (!IsBottomMost() || hasActiveLaser) return;

        Vector3 spawnPos = transform.position + Vector3.down * 0.6f;
        GameObject laser = Instantiate(invaderLaserPrefab, spawnPos, Quaternion.identity);

        hasActiveLaser = true;

        // Automatically reset after laser's lifetime
        float laserLifetime = 5f;
        Destroy(laser, laserLifetime);
        StartCoroutine(ResetLaserAfterDelay(laserLifetime));

        Debug.Log($"{gameObject.name} fired a laser!");
    }

    private IEnumerator ResetLaserAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        hasActiveLaser = false;
    }

    private bool IsBottomMost()
    {
        // Cast a ray downward slightly below the invader
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.down * 0.6f, Vector2.down, 10f);

        if (hit.collider != null && hit.collider != ownCollider && hit.collider.CompareTag("Invader"))
        {
            // Another invader below → can't shoot
            return false;
        }

        return true;
    }

    // Optional: visualize the ray in Scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.down * 0.6f,
                        transform.position + Vector3.down * 10f);
    }
}