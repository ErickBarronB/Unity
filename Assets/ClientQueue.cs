using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientQueue : MonoBehaviour
{



    public List<Vector3> queuePositions = new List<Vector3>();
    public int queueLengthMax;
    public float queuePositionOffset;
    // Start is called before the first frame update
    void Start()
    {
        GeneratePositions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GeneratePositions()
    {

        Vector3 positionToAdd = transform.localPosition;
        for (int i = 0; i < queueLengthMax; i++)
        {
            queuePositions.Add(positionToAdd);
            positionToAdd += new Vector3(0, 0, queuePositionOffset);
        }


    }


}
