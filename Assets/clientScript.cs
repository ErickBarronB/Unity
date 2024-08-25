using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class clientScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    private Rigidbody rb;
    private GameObject objectToPickup;
    public List<addItem> shelfIndex = new List<addItem>();
    private addItem ShelfSelected;


    private void Awake()
    {
        
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        objectToPickup = null;
        ShelfSelected = shelfSelected();
    }

    // Update is called once per frame
    void Update()
    {

        

        if (ShelfSelected.shelfItems.Count == 0 ) 
        {
            ShelfSelected = shelfSelected();
        }

        if (ShelfSelected.shelfItems.Count > 0)
        {
            objectToPickup = ShelfSelected.shelfItems.Last();
            rb.MovePosition(Vector3.MoveTowards(rb.position, ShelfSelected.transform.position, moveSpeed * Time.deltaTime));
        }

    }


    public addItem shelfSelected()
    {
        if (shelfIndex.Count == 0) return null;
        int index = UnityEngine.Random.Range(0, shelfIndex.Count);
        return shelfIndex[index];
    }

}
