using UnityEngine;

public class mIssileController : MonoBehaviour
{
    [SerializeField] private float speed = 20f;

    private Rigidbody rb;
    [SerializeField] private GameObject explosionEffectPrefab;
    private AudioSource gameAudio;
    [SerializeField] private AudioClip explosionSound;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gameAudio = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        // Reset any leftover velocity from the previous pool use
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Launch in the missile's own forward direction — includes both Z and Y tilt
        // Using velocity instead of AddForce gives consistent speed regardless of frame rate
        rb.linearVelocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            GameManager.instance.PlayAudio();
            gameObject.SetActive(false);
            

        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            GameManager.instance.PlayAudio();
            Destroy(other.gameObject);
            gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Target"))
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            GameManager.instance.PlayAudio();
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
