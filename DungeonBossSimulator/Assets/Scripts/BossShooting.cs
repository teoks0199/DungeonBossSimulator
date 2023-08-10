using UnityEngine;

public class BossAI : MonoBehaviour
{
    public float attackRadius = 5.0f; // Radius of the attack
    public int numProjectiles = 12; // Number of projectiles in the ring
    public float attackSpeed = 5.0f; // Speed of projectiles
    public float attackDelay = 2.0f; // Delay between attacks

    private float attackTimer = 0.0f; // Timer for tracking attack delay

    public GameObject projectilePrefab; // Prefab of the projectile

    private void Update()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackDelay)
        {
            attackTimer = 0.0f;
            ExecuteRingAttack();
        }
    }

    private void ExecuteRingAttack()
    {
        float angleIncrement = 360.0f / numProjectiles;

        for (int i = 0; i < numProjectiles; i++)
        {
            float angle = i * angleIncrement;
            float x = attackRadius * Mathf.Cos(Mathf.Deg2Rad * angle);
            float y = attackRadius * Mathf.Sin(Mathf.Deg2Rad * angle);

            Vector3 projectilePosition = transform.position + new Vector3(x, y, 0.0f);
            Quaternion projectileRotation = Quaternion.Euler(0.0f, 0.0f, angle);

            GameObject projectile = Instantiate(projectilePrefab, projectilePosition, projectileRotation);
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.Initialize(attackSpeed);
        }
    }
}

public class Projectile : MonoBehaviour
{
    private float speed;

    public void Initialize(float speed)
    {
        this.speed = speed;
    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // Check for collision with the player or other game objects
        // Apply damage or trigger appropriate game events
    }
}
