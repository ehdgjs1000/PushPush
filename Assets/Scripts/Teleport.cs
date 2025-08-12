using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Teleport : MonoBehaviour
{
    [Tooltip("��� ��Ż�� �����ϼ���.")]
    public Teleport other;

    [Tooltip("������ ��ġ ����(������ �� ������Ʈ ��ġ)")]
    public Transform exitPoint;

    private void Reset()
    {
        var col = GetComponent<Collider2D>();
        col.isTrigger = true; // ��Ż�� Ʈ����
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

        // �̵�
        ball.teleportCount--;
        ball.isTeleporting = true;
        ball.StopAllCouroutinesBall();
        var rb = col.attachedRigidbody;
        Vector2 outPos = other.transform.position;

        if (rb != null) rb.position = outPos;
        else col.transform.position = outPos;
        ball.isTeleporting = false;

        // ���� ����
        ball.lastPortal = other;
        SpriteRenderer thisSR = GetComponent<SpriteRenderer>();
        SpriteRenderer otherSR = other.GetComponent<SpriteRenderer>();
        thisSR.color = Color.black;
        otherSR.color = Color.black;
        
    }
}
