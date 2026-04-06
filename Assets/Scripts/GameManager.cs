using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private float count;
    private AudioSource gameAudio;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private GameObject explosionEffectPrefab;


    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameAudio = GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        gameAudio.PlayOneShot(explosionSound);
    }
    public void Dronekill(GameObject drone)
    {
        count++;
        if(count >= 10)
        {
            Instantiate(explosionEffectPrefab, drone.transform.position, Quaternion.identity);
            gameAudio.PlayOneShot(explosionSound);
            Destroy(drone);
                

        }
    }
}
