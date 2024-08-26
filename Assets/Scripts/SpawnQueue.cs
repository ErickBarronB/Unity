using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnQueue : MonoBehaviour
{
    public Queue<GameObject> clientQueue = new Queue<GameObject>();
    private Transform initialSpawn;
    public float zOffset;
    public GameObject spawnPrefab;
    public int queueLength;
    private GameObject clientToSpawn;
    private clientScript scriptToActivate;
    private Vector3 spawnPosition;
    public GameObject clientToActivate;

    private void Awake()
    {
        initialSpawn = transform;
    }

    void Start()
    {
        InitialSpawn();
    }

    void Update()
    {

        checkFirstClient();
        updateQueue();



    }

    public void InitialSpawn()
    {
        spawnPosition = initialSpawn.position;

        for (int i = 0; i < queueLength; i++)
        {

            //Debug.Log("Will spawn client at : " + spawnPosition);
            clientToSpawn = Instantiate(spawnPrefab, spawnPosition, Quaternion.identity);
            //Debug.Log("Instantiated");
            clientQueue.Enqueue(clientToSpawn);
            //Debug.Log("Queued");
            spawnPosition += new Vector3(0, 0, zOffset);
            //Debug.Log("Updated spawn position to : " + spawnPosition);
        }
    }

    public void checkFirstClient()
    {
        if (clientQueue.Count == 0) return;

        if (clientToActivate == null)
        {
            //Debug.Log("Grabbing client");
            //Debug.Log("list count is " + clientQueue.Count);
            clientToActivate = clientQueue.Dequeue();
            
            //Debug.Log("now it is " + clientQueue.Count);
            if (clientToActivate != null)
            {
                scriptToActivate = clientToActivate.GetComponentInChildren<clientScript>();
                if (scriptToActivate != null)
                {
                    scriptToActivate.willMove = true;
                    //Debug.Log("Client script willMove set to true");
                }

            }
        }


    }



    public void updateQueue()
    {
        if (clientQueue.Count < queueLength)
        {
        Debug.Log("Clients in queue" + clientQueue.Count);

        }
        if ((clientQueue.Count + 1) < queueLength)
        {
            clientToActivate.transform.position -= new Vector3(0, 0, zOffset);
            Debug.Log("gello");

            GameObject lastClient = clientQueue.Last();

            Vector3 lastClientPosition = lastClient.transform.position;

            foreach (var client in clientQueue)
            {
                client.transform.position -= new Vector3(0, 0, zOffset);
            }

            GameObject clientToAdd = Instantiate(spawnPrefab, lastClientPosition, Quaternion.identity);
            clientQueue.Enqueue(clientToAdd);


        }



    }

    //public IEnumerator nullClient()
    //{
    //    if (clientQueue.Count <= queueLength && clientQueue.Count > queueLength-1)
    //    {
    //    yield return new WaitForSeconds(4);
    //    clientToActivate = null;

    //    }
    //}












}
