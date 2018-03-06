using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chests : MonoBehaviour {

    public GameObject closedChest;
    public GameObject openChest;
    public GameObject gems;

    public int GemsToSpawn = 5;
    private int modelNumber;

    void Start()
    {
        modelNumber = 1;
        closedChest.SetActive(true);
        openChest.SetActive(false);
    }

    public void OpenChest()
    {
     closedChest.SetActive(false);
     openChest.SetActive(true);
     
    }
}
