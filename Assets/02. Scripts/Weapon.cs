using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform weaponPosition; // 무기의 시작 위치
    public Transform hitPosition; // 목표 위치
    public Vector2 controlPointOffset; // 베지어 곡선의 제어점 오프셋
    public float moveDuration = 1f; // 이동하는데 걸리는 시간

    private float elapsedTime = 0f; // 경과 시간
    public bool isMovingForward = false; // 전진 중인지 여부
    public bool isReturning = false; // 되돌아가는 중인지 여부

    void Update()
    {
        if (isMovingForward || isReturning)
        {
            MoveAlongCurve();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Attack();
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            isMovingForward = false;
            isReturning = true;
            elapsedTime = 0f; // 경과 시간 재설정
        }
    }

    public void Attack()
    {
        if (!isMovingForward && !isReturning)
        {
            elapsedTime = 0f;
            isMovingForward = true;
        }
    }

    private void MoveAlongCurve()
    {
        elapsedTime += Time.deltaTime;
        float t = elapsedTime / moveDuration;

        if (isMovingForward)
        {
            t = Mathf.Clamp(t, 0f, 1f);
        }
        else if (isReturning)
        {
            t = 1f - Mathf.Clamp(t, 0f, 1f);
        }

        // 베지어 곡선 계산
        Vector2 startPosition = weaponPosition.position;
        Vector2 endPosition = hitPosition.position;
        Vector2 controlPosition = (Vector2)weaponPosition.position + controlPointOffset; 
        Vector2 position = CalculateQuadraticBezierPoint(t, startPosition, controlPosition, endPosition);

        transform.position = position;

        if (t >= 1f && isMovingForward)
        {
            isMovingForward = false;
        }
        else if (t <= 0f && isReturning)
        {
            isReturning = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "monster" && isMovingForward)
        {
            isMovingForward = false;
            isReturning = true;
            elapsedTime = 0f; // 경과 시간 재설정
        }
    }

    private Vector2 CalculateQuadraticBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector2 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;

        return p;
    }
}
