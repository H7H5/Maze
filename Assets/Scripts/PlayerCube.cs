using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("redzone"))
        {
            gameObject.transform.parent.GetComponent<Player>().ColisionRedZone();
        }
    }
}
