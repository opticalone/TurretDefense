using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnerTest : MonoBehaviour {

    void onTriggerEnter(Collider other)
    {
        other.transform.position = new Vector3(0, 3, 0);
    }
}
