using UnityEngine;
using System.Collections.Generic;

public class BulletObjectPooling : MonoBehaviour
{
    public static BulletObjectPooling Instance;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float poolSize;
   private List<GameObject> bulletPool = new List<GameObject>();

    void Awake()
    {
        Instance = this;
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    public GameObject GetBullet()
    {
        foreach (GameObject bullet in bulletPool)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }
        // If all bullets are active, you can choose to expand the pool or return null
        return null;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
