using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class DroneDetection : MonoBehaviour
{
    [SerializeField] private EnemyPatrol enemyPatrol;
    [SerializeField] private BulletSpawner bulletSpawner;
    [SerializeField] private GameObject aimTarget;
    [SerializeField] private float rotationSpeed = 5f; // How fast the soldier turns his body


    private Animator anim;
    private NavMeshAgent agent;
    private Transform detectedDroneTransform;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (detectedDroneTransform != null)
        {
            bulletSpawner.SpawnBullet();
            // 1. Keep the IK Aim Target on the drone
            aimTarget.transform.position = detectedDroneTransform.position;

            // 2. Rotate the entire body toward the drone
            RotateBodyTowardDrone();
        }
    }

    private void RotateBodyTowardDrone()
    {
        // Calculate direction to drone
        Vector3 direction = (detectedDroneTransform.position - transform.position).normalized;

        // CRITICAL: Ignore height differences so the soldier doesn't tilt into the ground
        direction.y = 0;

        // Check if direction is not zero (to avoid errors)
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Smoothly rotate toward the drone over time
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Drone"))
        {
            anim.SetTrigger("Shoot");
            detectedDroneTransform = other.transform;
            StartCoroutine(ShootAtDrone());
            bulletSpawner.SpawnBullet();

            enemyPatrol.enabled = false;
            if (agent != null)
            {
                agent.isStopped = true;
                agent.enabled = false;
            }

            anim.SetTrigger("Shoot");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Drone"))
        {
            detectedDroneTransform = null;
            anim.SetTrigger("TriggeredZone");


            // Re-enable NavMesh logic
            if (agent != null)
            {
                agent.enabled = true;
                agent.isStopped = false;
            }
            enemyPatrol.enabled = true;

            // Return target to a default position in front of the character
            aimTarget.transform.localPosition = new Vector3(0, 1.5f, 2f);
        }
    }
    IEnumerator ShootAtDrone()
    {
        
        {
            yield return new WaitForSeconds(0.5f); // Initial delay before shooting starts
            bulletSpawner.SpawnBullet();
            yield return new WaitForSeconds(0.5f); // Adjust shooting frequency as needed
        }
    }
}