using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bulletSpawnArea;

    public void SpawnBullet(Transform target)
    {
        GameObject bullet = BulletObjectPooling.Instance.GetBullet();
        if (bullet != null)
        {
            bullet.transform.position = bulletSpawnArea.transform.position;

            // Aim at the actual center of the drone's collider in world space.
            // The drone's transform.position (pivot) is 2.2 units above its collider center,
            // so LookAt(target.position) overshoots. bounds.center gives the true center.
            Collider droneCollider = target.GetComponent<Collider>();
            Vector3 aimPoint = droneCollider != null ? droneCollider.bounds.center : target.position;

            bullet.transform.LookAt(aimPoint);
            bullet.SetActive(true);
        }
    }
}
