using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
