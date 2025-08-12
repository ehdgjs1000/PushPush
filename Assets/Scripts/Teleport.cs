using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Teleport : MonoBehaviour
{
    [Tooltip("상대 포탈을 연결하세요.")]
    public Teleport other;

    [Tooltip("나가는 위치 기준(없으면 이 오브젝트 위치)")]
    public Transform exitPoint;

    private void Reset()
    {
        var col = GetComponent<Collider2D>();
        col.isTrigger = true; // 포탈은 트리거
    }
    private void Start()
    {
        SpriteRenderer thisSR = GetComponent<SpriteRenderer>();
        thisSR.color = Color.white;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (other == null) return;
        if (!col.CompareTag("Player")) return;

        var ball = col.GetComponent<BallCtrl>();
        if (ball == null) ball = col.gameObject.AddComponent<BallCtrl>();
        if (ball.teleportCount <= 0) return;
        if (ball.lastPortal == this) return;

        // 이동
        ball.teleportCount--;
        ball.isTeleporting = true;
        ball.StopAllCouroutinesBall();
        var rb = col.attachedRigidbody;
        Vector2 outPos = other.transform.position;

        if (rb != null) rb.position = outPos;
        else col.transform.position = outPos;
        ball.isTeleporting = false;

        // 상태 갱신
        ball.lastPortal = other;
        SpriteRenderer thisSR = GetComponent<SpriteRenderer>();
        SpriteRenderer otherSR = other.GetComponent<SpriteRenderer>();
        thisSR.color = Color.black;
        otherSR.color = Color.black;
        
    }
}
