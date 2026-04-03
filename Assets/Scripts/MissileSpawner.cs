using UnityEngine;

public class MiissileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] missileSpawns;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnMissiles();
        }
    }
    void SpawnMissiles()
    {
        GameObject missile = missile_ObjectPooling.Instance.GetMissile();
        if(missile != null)
        {
            int randomIndex = Random.Range(0, missileSpawns.Length);
            missile.transform.position = missileSpawns[randomIndex].transform.position;
            missile.transform.rotation = missile.transform.rotation;
        }
    }
}
