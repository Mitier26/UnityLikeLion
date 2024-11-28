using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TransitionMovement : MonoBehaviour
{
    public float moveSpeed = 10f;

    public TextMeshProUGUI positionText;
    public TextMeshProUGUI velocityText;
    public TextMeshProUGUI directionText;

    private Vector3 target;
    private Vector3 lastDirection;
    private float lastMoveSpeed;

    private const float TargetThreshold = 0.01f;

    private void OnEnable()
    {
        transform.position = Vector3.up;
        moveSpeed = 10f;
        target = Vector3.up;

        UpdateVelocityText();
        UpdatePositionText();
        UpdateDirectionText(Vector3.zero);
    }

    private void Update()
    {
        // 마우스 휠로 속도 조정
        float wheelSpeed = Input.GetAxis("Mouse ScrollWheel");

        if (wheelSpeed != 0)
        {
            moveSpeed += wheelSpeed * 10f;
            moveSpeed = Mathf.Clamp(moveSpeed, 1f, 30f);

            if (Mathf.Abs(moveSpeed - lastMoveSpeed) > Mathf.Epsilon)
            {
                UpdateVelocityText();
                lastMoveSpeed = moveSpeed;
            }
        }

        // 마우스 클릭으로 목표 지점 설정
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                target = new Vector3(hit.point.x, 1f, hit.point.z);
                UpdatePositionText();
            }
        }

        // 방향 계산 및 이동
        Vector3 dir = target - transform.position;
        if (dir.sqrMagnitude > TargetThreshold)
        {
            dir.Normalize();

            if ((dir - lastDirection).sqrMagnitude > Mathf.Epsilon)
            {
                UpdateDirectionText(dir);
                lastDirection = dir;
            }

            transform.Translate(dir * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = target;

            if (lastDirection != Vector3.zero)
            {
                UpdateDirectionText(Vector3.zero);
                lastDirection = Vector3.zero;
            }
        }
    }

    private void UpdateVelocityText()
    {
        velocityText.text = "속도 : " + moveSpeed.ToString("F1");
    }

    private void UpdatePositionText()
    {
        positionText.text = "( " + target.x.ToString("F1") + ", " + target.y.ToString("F1") + ", " + target.z.ToString("F1") + " )";
    }

    private void UpdateDirectionText(Vector3 dir)
    {
        directionText.text = "( " + dir.x.ToString("F1") + ", " + dir.y.ToString("F1") + ", " + dir.z.ToString("F1") + " )";
    }
}
