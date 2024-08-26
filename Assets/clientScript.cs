using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class clientScript : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody rb;
    private GameObject objectToPickup;
    public List<addItem> shelfIndex = new List<addItem>();
    private addItem shelfSelected;
    public Transform itemHoldPosition;
    public Transform targetPosition;
    public Transform exitPosition;
    private float rotationSpeed = 2f;
    private bool hasItem;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        objectToPickup = null;
        shelfSelected = SelectShelf();
    }

    void FixedUpdate()
    {
        if (shelfSelected != null && shelfSelected.shelfItems.Count == 0)
        {
            shelfSelected = SelectShelf();
        }

        if (shelfSelected != null && shelfSelected.shelfItems.Count > 0)
        {
            objectToPickup = shelfSelected.shelfItems.Peek();
            MoveTowardsShelf();
        }
    }

    public addItem SelectShelf()
    {
        if (shelfIndex.Count == 0) return null;
        int index = UnityEngine.Random.Range(0, shelfIndex.Count);
        return shelfIndex[index];
    }

    private void MoveTowardsShelf()
    {
        Vector3 targetPosition = objectToPickup.transform.position;
        if (!hasItem)
        {
            rb.MovePosition(Vector3.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime));

            if ((Vector3.Distance(rb.position, targetPosition) < 2f))
            {

                PickupItem();
            }


        }
        if (hasItem)
        {
            MoveToExit();
        }
    }

    public void PickupItem()
    {
        if (objectToPickup != null)
        {
            Debug.Log("Attempting to pick up item: " + objectToPickup.name);

            if (shelfSelected.shelfItems.Count > 0)
            {
                Debug.Log("Items in shelf before pop: " + shelfSelected.shelfItems.Count);
                shelfSelected.shelfItems.Pop();
                Debug.Log("Item picked up. Items left in shelf: " + shelfSelected.shelfItems.Count);
            }
            else
            {
                Debug.Log("Shelf items are empty.");
            }

            objectToPickup.transform.SetParent(null);
            objectToPickup.transform.position = itemHoldPosition.position;
            objectToPickup.transform.SetParent(itemHoldPosition);
            objectToPickup.transform.localPosition = Vector3.zero;
            objectToPickup.transform.localRotation = Quaternion.identity;
            hasItem = true;



        }
    }



    public void MoveToExit()
    {
        if (hasItem)
        {
            Vector3 directionToExit = (exitPosition.position - rb.position).normalized;
            Vector3 targetPosition = exitPosition.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToExit);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MovePosition(Vector3.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime));

            if (Vector3.Distance(rb.position, targetPosition) < 2f)
            {
                moneyUpdate.addMoney(20);
                Destroy(gameObject);

            }
        }
    }


}
