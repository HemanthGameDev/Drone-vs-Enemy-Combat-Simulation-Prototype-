using UnityEngine;
using System.Collections;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float speed = 20f;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        // Always zero out leftover velocity from the previous pool use
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        StartCoroutine(DeactivateAfterTime());
    }

    IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
           if (other.gameObject.CompareTag("Drone"))
        {
            GameManager.instance.Dronekill(other.gameObject);
            gameObject.SetActive(false);
            
        }
    }
}
