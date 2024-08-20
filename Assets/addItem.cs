using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class addItem : MonoBehaviour
{
    // Start is called before the first frame update
    private int positionIndex;
    public LayerMask shelf;
    public Vector3 positionToAddItem;
    public float positionOffset;
    public int listLength;
    public int listCap;
    public GameObject itemPrefab;

    public List<Vector3> itemPosition = new List<Vector3>();
    private Stack<GameObject> shelfItems = new Stack<GameObject>();
    void Start()
    {

    }

    void Update()
    {
        positionIndex = itemPosition.Count;

        listUpdate(positionIndex);
        UpdateItem(positionIndex);
    }


    public void UpdateItem(int positionIndex)
    {
        int vectorIndex = positionIndex;
        if (Input.GetMouseButtonDown(0) && detectedShelf())
        {

            if (vectorIndex < listCap)
            {
                GameObject createdItem = Instantiate(itemPrefab, transform);
                shelfItems.Push(createdItem);
                createdItem.transform.position = itemPosition[vectorIndex];
                vectorIndex++;
            }


        }

        if (Input.GetMouseButtonDown(1) && detectedShelf())
        {
        if (vectorIndex > 0)
            {
                Destroy(shelfItems.Pop());
                vectorIndex--;
            }


        }


    }





    private bool detectedShelf()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, shelf))
            {
                Debug.Log("Hit Shelf");
                return true;
            }

        }



        return false;
    }


    public void listUpdate(int positionIndex)
    {
        if (detectedShelf())
        {

            if (listLength < listCap)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (listLength >= 0)
                    {

                        itemPosition.Add(transform.position + positionToAddItem);
                        listLength++;
                        positionToAddItem += new Vector3(0, 0, positionOffset);

                    }

                }

            }

            if (Input.GetMouseButtonDown(1))
            {
                if (itemPosition.Count > 0 && listLength > 0)
                {
                    itemPosition.RemoveAt(positionIndex - 1);
                    positionToAddItem -= new Vector3(0, 0, positionOffset);
                    listLength--;
                }

            }

        }



    }
}
