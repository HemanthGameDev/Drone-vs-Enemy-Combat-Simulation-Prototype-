using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class DroneDetection : MonoBehaviour
{
    [SerializeField] private EnemyPatrol enemyPatrol;
    [SerializeField] private BulletSpawner bulletSpawner;
    [SerializeField] private GameObject aimTarget;
    [SerializeField] private Transform gunMuzzle;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float aimThreshold = 5f;
    [SerializeField] private Vector3 offset;

    private Animator anim;
    private NavMeshAgent agent;
    private Transform detectedDroneTransform;
    private Coroutine shootingCoroutine;

    // NEW: Flag to track if we are currently in "Combat Mode"
    private bool isTargetingDrone = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // FAIL-SAFE: Check if the drone was destroyed while inside the trigger
        if (isTargetingDrone && detectedDroneTransform == null)
        {
            ResetEnemyState();
            return; // Stop further Update logic for this frame
        }

        if (detectedDroneTransform != null)
        {
            // Update aim target position
            aimTarget.transform.position = detectedDroneTransform.position - offset;
            RotateBodyTowardDrone();
        }
    }

    private void RotateBodyTowardDrone()
    {
        Vector3 direction = (detectedDroneTransform.position - transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Drone"))
        {
            detectedDroneTransform = other.transform;
            isTargetingDrone = true; // Mark as targeting

            enemyPatrol.enabled = false;
            if (agent != null) { agent.isStopped = true; agent.enabled = false; }

            anim.SetTrigger("Shoot");

            if (shootingCoroutine == null)
            {
                shootingCoroutine = StartCoroutine(ShootAtDrone());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Drone"))
        {
            ResetEnemyState();
        }
    }

    // NEW: Reusable Reset Function
    private void ResetEnemyState()
    {
        isTargetingDrone = false;
        detectedDroneTransform = null;

        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
        }

        anim.SetTrigger("TriggeredZone");

        if (agent != null)
        {
            agent.enabled = true;
            agent.isStopped = false;
        }
        enemyPatrol.enabled = true;

        // Reset aim to idle position
        aimTarget.transform.localPosition = new Vector3(0, 1.5f, 2f);
    }

    IEnumerator ShootAtDrone()
    {
        // Added extra safety: check both null and active state
        while (detectedDroneTransform != null && detectedDroneTransform.gameObject.activeInHierarchy)
        {
            Vector3 dirToDrone = (detectedDroneTransform.position - transform.position).normalized;
            dirToDrone.y = 0f;

            float angle = Vector3.Angle(transform.forward, dirToDrone);

            if (angle <= aimThreshold)
            {
                bulletSpawner.SpawnBullet(detectedDroneTransform);
                yield return new WaitForSeconds(fireRate);
            }
            else
            {
                yield return null;
            }
        }
    }
}