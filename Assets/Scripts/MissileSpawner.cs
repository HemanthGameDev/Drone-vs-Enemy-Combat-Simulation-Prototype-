using UnityEngine;

public class MiissileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] missileSpawns;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnMissiles();
        }
    }

    void SpawnMissiles()
    {
        GameObject missile = missile_ObjectPooling.Instance.GetMissile();
        if (missile != null)
        {
            int randomIndex = Random.Range(0, missileSpawns.Length);
            Transform spawnPoint = missileSpawns[randomIndex].transform;

            missile.transform.position = spawnPoint.position;

            // Inherit the spawn point's world rotation so the missile faces
            // exactly where the drone arm/wing is pointing — both Z and Y included
            missile.transform.rotation = missile.transform.rotation;

            missile.SetActive(true);
        }
    }
}
