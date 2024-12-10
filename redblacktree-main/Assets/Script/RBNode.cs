using System.Collections;
using TMPro;
using UnityEngine;

public class RBNode : MonoBehaviour
{
    public int data;
    public RBNode left, right;

    public RBNode nilNode;

    public RBNode _parent;
    
    public Material red_material;
    public Material black_material;
    
    public LineRenderer leftLineRenderer;
    public LineRenderer rightLineRenderer;
    public LineRenderer parentLineRenderer;

    public Vector3 targetPosition; // 목표 위치
    public float moveSpeed = 5f;   // 이동 속도
    
    public RBNode parent
    {
        get { return _parent; }
        set
        {
            _parent = value;
            if (_parent != nilNode)
                transform.SetParent(_parent.transform);
        }
    }

    public NodeColor color;
    
    public MeshRenderer mr;

    void Update()
    {
        // 부드럽게 이동
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * moveSpeed);
        
        // 텍스트와 색상 업데이트
        GetComponentInChildren<TextMeshPro>().text = data.ToString();
        mr.material = color == NodeColor.Red ? red_material : black_material;

        // 부모-자식 간 선 연결
        if (parent != nilNode)
        {
            if (parent.left != nilNode && parent.left == this)
            {
                targetPosition = new Vector3(-3, -3, 0); // 왼쪽 자식 목표 위치
                parentLineRenderer.SetPositions(new Vector3[] { parent.transform.position, transform.position });
            }
            else if (parent.right != nilNode && parent.right == this)
            {
                targetPosition = new Vector3(3, -3, 0); // 오른쪽 자식 목표 위치
                parentLineRenderer.SetPositions(new Vector3[] { parent.transform.position, transform.position });
            }
            else
            {
                parentLineRenderer.SetPositions(new Vector3[] { });
            }
        }
        else
        {
            parentLineRenderer.SetPositions(new Vector3[] { });
            targetPosition = new Vector3(0, 0, 0); // 루트 노드 위치
        }

        // 왼쪽 자식과 연결
        if (left != nilNode)
        {
            leftLineRenderer.SetPositions(new Vector3[] { transform.position, left.transform.position });
        }
        else
        {
            leftLineRenderer.SetPositions(new Vector3[] { });
        }

        // 오른쪽 자식과 연결
        if (right != nilNode)
        {
            rightLineRenderer.SetPositions(new Vector3[] { transform.position, right.transform.position });
        }
        else
        {
            rightLineRenderer.SetPositions(new Vector3[] { });
        }
    }
}
