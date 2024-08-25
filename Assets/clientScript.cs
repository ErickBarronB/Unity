using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clientScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    private Rigidbody rb;
    private GameObject objectToPickup;
    public List<addItem> itemSpots = new List<addItem>();

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (objectToPickup != null)
        {
            
        }
    }
}
