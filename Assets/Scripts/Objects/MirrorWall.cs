using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D co)
    {
        if (!co.CompareTag("Player")) return;
        if (co.GetComponent<BallCtrl>() == null) return;
        BallCtrl ball = co.GetComponent<BallCtrl>();
        ball.MoveBack();

    }

}
