using UnityEngine;

public class Invaders : MonoBehaviour
{
    public Invader[] prefabs;
    public int rows = 5;
    public int columns = 11;
    public float baseSpeed = 1f;        // starting speed
    public float speedIncrease = 0.05f; // speed increase per invader killed
    public float startY = 3.0f;         // starting Y position of formation

    private Vector3 _direction = Vector3.right;
    private int totalInvaders;

    private void Awake()
    {
        SetupInvaders();
    }

    private void SetupInvaders()
    {
        float width = 2.0f * (columns - 1);
        Vector2 centering = new Vector2(-width / 2, startY);

        totalInvaders = rows * columns;

        for (int row = 0; row < rows; row++)
        {
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 2.0f), 0f);

            for (int col = 0; col < columns; col++)
            {
                Invader prefab = prefabs[row % prefabs.Length];
                Invader invader = Instantiate(prefab, this.transform);

                // Top row = highest points, bottom = lowest
                invader.points = 10 + row * 10;

                Vector3 position = rowPosition;
                position.x += col * 2.0f;
                invader.transform.position = position;
            }
        }
    }

    private void Update()
    {
        int aliveInvaders = 0;
        foreach (Transform invader in transform)
        {
            if (invader.gameObject.activeInHierarchy)
                aliveInvaders++;
        }

        // Restart formation if all invaders are dead
        if (aliveInvaders == 0)
        {
            ResetInvaders();
            return;
        }

        float currentSpeed = baseSpeed + speedIncrease * (totalInvaders - aliveInvaders);
        transform.position += _direction * currentSpeed * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy)
                continue;

            if (_direction == Vector3.right && invader.position.x >= rightEdge.x - 1f)
            {
                AdvanceRow();
                break;
            }
            else if (_direction == Vector3.left && invader.position.x <= leftEdge.x + 1f)
            {
                AdvanceRow();
                break;
            }
        }
    }

    private void AdvanceRow()
    {
        _direction.x *= -1f; // reverse direction
        Vector3 pos = transform.position;
        pos.y -= 1f;          // move formation down
        transform.position = pos;
    }

    private void ResetInvaders()
    {
        // Destroy all existing invaders
        foreach (Transform invader in transform)
        {
            Destroy(invader.gameObject);
        }

        // Recreate invader formation
        SetupInvaders();

        // Reset all bunkers in the scene
        GameObject[] bunkers = GameObject.FindGameObjectsWithTag("Bunker");
        foreach (GameObject bunkerGO in bunkers)
        {
            Bunker bunker = bunkerGO.GetComponent<Bunker>();
            if (bunker != null)
                bunker.ResetBunker();
            else
                bunkerGO.SetActive(true); // fallback
        }
    }
}