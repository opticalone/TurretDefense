using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildPlatform : MonoBehaviour {

    GameObject player;

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Player")
        {
            player = c.gameObject;
            Debug.Log("collision");

            player.transform.parent = transform.parent;
        }
    }

      private void OnCollisionExit(Collision c)
    {
        player.transform.parent = null;
    }
}
