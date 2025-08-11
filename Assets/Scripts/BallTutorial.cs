using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class BallTutorial : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;               // 이동 속도
    public float minSwipePixels = 30f;          // 스와이프 최소 픽셀
    public float skin = 0.01f;                  // 벽에 닿기 전 여유
    public LayerMask obstacleMask;              // 장애물 레이어

    Rigidbody2D rb;
    Collider2D col;
    bool isMoving;

    Vector2 startTouchPos;
    bool touching;

    //Undo 저장소
    Stack<Vector2> positionHistory = new Stack<Vector2>();

    bool isTrigger = false;
    float triggerTime = 0.0f;

    int moveCount = 1;
    float moveTime = 2.0f;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        rb.isKinematic = true;
        rb.gravityScale = 0f;

        // 시작 위치 기록
        positionHistory.Push(rb.position);
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        HandleMouse();
#else
        HandleTouch();
#endif
        if (isTrigger)
        {
            triggerTime += Time.deltaTime;
            if (triggerTime >= 1.0f && !GameManager.instance.isEndGame) EndGame();
        }
        if (moveCount == 0)
        {
            moveTime -= Time.deltaTime;
            if(moveTime <=0.0f)
            {
                moveTime = 2.0f;
                moveCount = 1;
                UndoMove();
            }
        }
        // 테스트용 -> UI 연결
        if (Input.GetKeyDown(KeyCode.Z))
        {
            UndoMove();
        }
    }

    void HandleMouse()
    {
        if (moveCount > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                touching = true;
                startTouchPos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0) && touching && !isMoving)
            {
                Vector2 delta = (Vector2)Input.mousePosition - startTouchPos;
                TrySwipe(delta);
                touching = false;
            }
        }
    }

    void HandleTouch()
    {
        if (Input.touchCount == 0) return;

        var t = Input.GetTouch(0);
        if (t.phase == TouchPhase.Began)
        {
            touching = true;
            startTouchPos = t.position;
        }
        else if ((t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled) && touching && !isMoving)
        {
            Vector2 delta = t.position - startTouchPos;
            TrySwipe(delta);
            touching = false;
        }
    }

    void TrySwipe(Vector2 delta)
    {
        if (delta.magnitude < minSwipePixels) return;

        Vector2 dir = Mathf.Abs(delta.x) > Mathf.Abs(delta.y)
            ? new Vector2(Mathf.Sign(delta.x), 0f)
            : new Vector2(0f, Mathf.Sign(delta.y));

        Vector2 target = ComputeTargetPoint(dir);
        if ((Vector2)transform.position != target)
            StartCoroutine(MoveTo(target));
    }

    Vector2 ComputeTargetPoint(Vector2 dir)
    {
        Vector2 origin = rb.position;

        float radius = 0f;
        if (col is CircleCollider2D cc)
            radius = cc.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y);
        else
        {
            var b = col.bounds.extents;
            radius = dir.x != 0 ? b.x : b.y;
        }

        Vector2 castOrigin = origin + dir * (radius - 0.001f);

        float maxDist = 1000f;
        RaycastHit2D hit = Physics2D.Raycast(castOrigin, dir, maxDist, obstacleMask);

        if (hit.collider != null)
        {
            return hit.point - dir * (radius + skin);
        }
        else
        {
            return origin;
        }
    }

    IEnumerator MoveTo(Vector2 target)
    {
        isMoving = true;
        moveCount--;

        // ?? 이동 시작 전 현재 위치를 히스토리에 저장
        positionHistory.Push(rb.position);

        while ((rb.position - target).sqrMagnitude > 0.0001f)
        {
            Vector2 next = Vector2.MoveTowards(rb.position, target, moveSpeed * Time.deltaTime);
            rb.MovePosition(next);
            yield return null;
        }
        rb.MovePosition(target);

        isMoving = false;
    }

    // ?? Undo 기능
    public void UndoMove()
    {
        if (isMoving) return;
        if (positionHistory.Count <= 1) return; // 현재 위치밖에 없음

        // 현재 위치 제거
        positionHistory.Pop();

        // 이전 위치로 즉시 이동
        Vector2 prev = positionHistory.Peek();
        rb.MovePosition(prev);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTrigger = false;
        triggerTime = 0;
    }
    void EndGame()
    {
        GameManager.instance.GameOver();
    }
}