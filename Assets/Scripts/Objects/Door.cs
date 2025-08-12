using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Collider2D))]
public class Door : MonoBehaviour
{
    public GameObject doorGO;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        
        if(doorGO == null) return;
        doorGO.transform.DOScale(Vector3.zero, 0.3f);
        Collider2D collider = doorGO.GetComponent<Collider2D>();    
        collider.enabled = false;
        
        BallCtrl ball = FindObjectOfType<BallCtrl>();
        ball.MoveAfterDoorBtnClick();
    }
}
