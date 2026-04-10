using UnityEngine;

public class DroneController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float upDownSpeed;
    float horizontalInput;
    float verticalInput;
    Rigidbody droneRb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        droneRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
       
        droneRb.AddForce(Vector3.forward * speed * -verticalInput * Time.fixedDeltaTime);
        droneRb.AddForce(Vector3.right * speed * -horizontalInput * Time.fixedDeltaTime);
        if(Input.GetKey(KeyCode.Q))
        {
            droneRb.AddForce(Vector3.up * upDownSpeed * Time.fixedDeltaTime);
        }
        else if(Input.GetKey(KeyCode.E))
        {
            droneRb.AddForce(Vector3.down * upDownSpeed * Time.fixedDeltaTime);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Container"))
        {
            Debug.Log("Collided with Container");
        }
    }
}
