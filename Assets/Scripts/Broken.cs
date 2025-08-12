using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broken : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D co)
    {
        if (!co.CompareTag("Player")) return;

        this.gameObject.SetActive(false);

    }

}
