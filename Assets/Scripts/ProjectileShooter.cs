using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject projectilePrefab; // Assign in Inspector
    public int poolSize = 10; // Number of reusable projectiles
    public float projectileSpeed = 10f;
    public float fireRate = 1f; // Seconds between shots
    public float angleVariance = 20f; // Max angle variation in degrees

    private Queue<GameObject> projectilePool;
    private float nextFireTime;

    void Start()
    {
        InitializePool();
    }

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            ShootProjectiles();
            nextFireTime = Time.time + fireRate;
        }
    }

    void InitializePool()
    {
        projectilePool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject proj = Instantiate(projectilePrefab);
            proj.SetActive(false);
            projectilePool.Enqueue(proj);
        }
    }

    void ShootProjectiles()
    {
        for (int i = 0; i < 3; i++) // Shoot 3 projectiles
        {
            GameObject proj = GetPooledProjectile();
            if (proj != null)
            {
                proj.transform.position = transform.position;
                proj.transform.rotation = Quaternion.identity;

                // Generate a random angle variation (rightward with a spread)
                float randomAngle = Random.Range(-angleVariance, angleVariance);
                Vector3 direction = Quaternion.Euler(0, 0, randomAngle) * Vector3.right; // Rotate rightward direction

                proj.GetComponent<Rigidbody>().linearVelocity = direction.normalized * projectileSpeed;
            }
        }
    }

    GameObject GetPooledProjectile()
    {
        if (projectilePool.Count > 0)
        {
            GameObject proj = projectilePool.Dequeue();
            proj.SetActive(true);
            StartCoroutine(DisableProjectileAfterTime(proj, 2f)); // Disable after 2 seconds
            return proj;
        }
        return null; // No available projectiles
    }

    IEnumerator DisableProjectileAfterTime(GameObject proj, float delay)
    {
        yield return new WaitForSeconds(delay);
        proj.SetActive(false);
        projectilePool.Enqueue(proj);
    }
}
