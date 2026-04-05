using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bulletSpawnArea;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnBullet()
    {
        GameObject bullet = BulletObjectPooling.Instance.GetBullet();
        if (bullet != null)
        {
            bullet.transform.position = bulletSpawnArea.transform.position;
            bullet.transform.rotation = bulletSpawnArea.transform.rotation;
            bullet.SetActive(true);
        }
    }
}
