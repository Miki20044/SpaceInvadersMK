using UnityEngine;

public class Player : MonoBehaviour
{
    public Projectile laserPrefab;       // Assign in Inspector
    public float speed = 5f;

    [Header("Shooting")]
    public float shootCooldown = 0.5f;

    private bool laserActive = false;
    private float nextShootTime = 0f;

    private void Update()
    {
        // --- Player Movement ---
        float horizontal = 0f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) horizontal = -1f;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) horizontal = 1f;

        transform.position += Vector3.right * horizontal * speed * Time.deltaTime;

        // --- Shooting ---
        if (Input.GetKeyDown(KeyCode.Space) && !laserActive && Time.time >= nextShootTime)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // Spawn laser slightly above player to avoid collisions
        Vector3 spawnPos = transform.position + Vector3.up * 0.6f;
        Projectile laser = Instantiate(laserPrefab, spawnPos, Quaternion.identity);

        // Set direction upward
        laser.direction = Vector3.up;

        // Callback to reset shooting
        laser.destroyed += OnLaserDestroyed;

        // Lock shooting until laser destroyed or cooldown
        laserActive = true;
        nextShootTime = Time.time + shootCooldown;
    }

    private void OnLaserDestroyed()
    {
        laserActive = false;
    }
}