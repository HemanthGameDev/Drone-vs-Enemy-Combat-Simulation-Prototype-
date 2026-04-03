using UnityEngine;
using System.Collections.Generic;

public class missile_ObjectPooling : MonoBehaviour
{
    public static missile_ObjectPooling Instance;
    [SerializeField] private GameObject missilePrefab;
    private List<GameObject> missilePool = new List<GameObject>();
    private float maxMissiles = 10;
    private void Awake()
    {
       Instance = this;
        for(int i=0;i<maxMissiles;i++)
        {
            GameObject obj = Instantiate(missilePrefab);
            obj.SetActive(false);
            missilePool.Add(obj);
        }

    }
    public GameObject GetMissile()
    {
       foreach (GameObject missile in missilePool)
        {
            if(!missile.activeInHierarchy)
            {
                missile.SetActive(true);
                return missile;
            }
        }
        return null; // No available missiles in the pool
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 
}
