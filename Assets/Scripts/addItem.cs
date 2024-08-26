using System.Collections.Generic;
using UnityEngine;

public class addItem : MonoBehaviour
{
    private int positionIndex;
    public LayerMask shelfLayer;
    public Vector3 positionToAddItem;
    public float positionOffset;
    public int listCap;
    public GameObject itemPrefab;

    public List<Vector3> itemPosition = new List<Vector3>();
    [HideInInspector] public Stack<GameObject> shelfItems = new Stack<GameObject>();

    void Update()
    {
        if (detectedShelf(out RaycastHit hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                AddItemToShelf(hit.transform);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                RemoveItemFromShelf(hit.transform);
            }
        }
    }

    private void AddItemToShelf(Transform shelfTransform)
    {
        if (itemPosition.Count < listCap)
        {
            Vector3 newPosition = shelfTransform.position + positionToAddItem;
            itemPosition.Add(newPosition);
            positionToAddItem += new Vector3(0, 0, positionOffset);

            GameObject createdItem = Instantiate(itemPrefab, shelfTransform);
            shelfItems.Push(createdItem);
            createdItem.transform.position = newPosition;
        }
    }

    private void RemoveItemFromShelf(Transform shelfTransform)
    {
        if (itemPosition.Count > 0)
        {
            //Destroy(shelfItems.Pop());
            GameObject RemovedGameObject = shelfItems.Pop();
            Destroy(RemovedGameObject);
            itemPosition.Remove(itemPosition[itemPosition.Count - 1]);
            //itemPosition.RemoveAt(itemPosition.Count - 1);
            positionToAddItem -= new Vector3(0, 0, positionOffset);
        }
    }

    private bool detectedShelf(out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, shelfLayer))
        {
            return hit.transform == transform; // Ensure it only interacts with the current shelf
        }

        hit = default;
        return false;
    }
}
