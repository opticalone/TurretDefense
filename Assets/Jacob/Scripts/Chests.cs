using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chests : MonoBehaviour {

    public GameObject closedChest;
    public GameObject openChest;
    public GameObject gems;

    public int GemsToSpawn = 5;

    void Start()
    {
        closedChest.SetActive(true);
        openChest.SetActive(false);
    }

    public void OpenChest()
    {
        closedChest.SetActive(false);
        openChest.SetActive(true);
        for (int i = 0; i < GemsToSpawn; i++)
        {
            GameObject spawnedGem = Instantiate(gems);
            spawnedGem.transform.position = transform.position + (Vector3.up * i);
        }
       
    }
    public bool editorOpenChest;
    void Update()
    {
        if(editorOpenChest)
        {
            editorOpenChest = false;
            OpenChest();
        }
    }

}
