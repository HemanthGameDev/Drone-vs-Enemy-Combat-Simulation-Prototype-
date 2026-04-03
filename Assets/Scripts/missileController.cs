using UnityEngine;

public class mIssileController : MonoBehaviour
{
    [SerializeField] private float speed; // Speed of the missile
    Rigidbody rb; // Reference to the Rigidbody component
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component attached to the missile
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(transform.forward * speed * Time.deltaTime); 
        //rb.AddForce(transform.forward * speed * Time.deltaTime); // Move the missile forward based on its speed


    }
}
