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
    public bool willMove = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        FindShelves();
        exitPosition = GameObject.Find("ExitPosition").GetComponent<Transform>();
    }

    void Start()
    {

        objectToPickup = null;
        shelfSelected = SelectShelf();

    }

    void FixedUpdate()
    {
        if (willMove)
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

            if (hasItem)
            {
                MoveToExit();
            }
        }
    }


    private void FindShelves()
    {
        addItem[] shelves = FindObjectsOfType<addItem>();
        shelfIndex.AddRange(shelves);
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
                shelfSelected.itemPosition.Remove(shelfSelected.itemPosition[shelfSelected.shelfItems.Count]);
                shelfSelected.positionToAddItem -= new Vector3(0, 0, shelfSelected.positionOffset);
                Debug.Log("Item picked up. Items left in shelf: " + shelfSelected.shelfItems.Count);
            }
            else
            {
                Debug.Log("Shelf items are empty.");
            }

            hasItem = true;
            objectToPickup.transform.SetParent(null);
            objectToPickup.transform.position = itemHoldPosition.position;
            objectToPickup.transform.SetParent(itemHoldPosition);
            objectToPickup.transform.localPosition = Vector3.zero;
            objectToPickup.transform.localRotation = Quaternion.identity;



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
                Destroy(transform.parent.gameObject);
                Destroy(gameObject);

            }
        }
    }

    public void SetMovementActive()
    {
        willMove = true;
    }


}
