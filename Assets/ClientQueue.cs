using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientQueue : MonoBehaviour
{



    public List<Vector3> queuePositions = new List<Vector3>();
    public int queueLengthMax;
    public float queuePositionOffset;
    public GameObject clientGameobject;

    void Start()
    {
        GeneratePositions();
    }

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


    public void CreateClient(Vector3 spawnPos)
    {

        Instantiate(clientGameobject, spawnPos, Quaternion.identity);

    }

}
