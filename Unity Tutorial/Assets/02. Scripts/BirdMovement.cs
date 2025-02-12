using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public SoundManager soundManager; // SoundManager Ÿ���� ����

    private Rigidbody2D myRigid;
    public float flyPower = 10f; // ���ƿ����� ��

    public float limitPower = 5f; // ���� �ӵ�

    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>(); // �ڱ� �ڽ��� Rigidbody2D�� �Ҵ�
    }

    // Ű���� �����̽��ٸ� ������
    // ���� ���ƿ����� ���
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // �����̽��� ������ �� ���ǹ�
        {
            soundManager.OnJumpSound();

            myRigid.AddForce(Vector2.up * flyPower, ForceMode2D.Impulse); // �������� ���� ����

            // �ӵ� üũ �� ����
            if (myRigid.velocity.y > limitPower) // ���� y�� �ӵ��� ���� �ӵ����� Ŀ������ Ȯ���ϴ� ���ǹ�
            {
                // �ӵ��� ������
                myRigid.velocity = new Vector2(myRigid.velocity.x, limitPower);
            }
        }

        var playerEulerAngle = transform.eulerAngles;
        playerEulerAngle.z = myRigid.velocity.y * 5f;

        transform.eulerAngles = playerEulerAngle;
    }
}