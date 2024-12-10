using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
        GetComponentInChildren<TextMeshPro>().text = data.ToString();
        mr.material = color == NodeColor.Red ? red_material : black_material;  ;
            
        if (parent != nilNode)
        {
            if (parent.left != nilNode&& parent.left == this)
            {
                transform.localPosition = new Vector3(-3, -3, 0);
                parentLineRenderer.SetPositions(new Vector3[] {parent.transform.position , transform.position});
            }
            else if (parent.right != nilNode && parent.right == this)
            {
                transform.localPosition = new Vector3(3, -3, 0);
                parentLineRenderer.SetPositions(new Vector3[] {parent.transform.position , transform.position});
            }
            else
            {
                parentLineRenderer.SetPositions(new Vector3[] { });
            }
            
        }
        else
        {
            parentLineRenderer.SetPositions(new Vector3[] { });
            transform.localPosition = new Vector3(0, 0, 0);
        }

        if (left != nilNode)
        {
            leftLineRenderer.SetPositions(new Vector3[]{transform.position, left.transform.position});
        }
        else
        {
            leftLineRenderer.SetPositions(new Vector3[]{});
        }
        
        if (right != nilNode)
        {
            rightLineRenderer.SetPositions(new Vector3[]{transform.position, right.transform.position});
        }
        else
        {
            rightLineRenderer.SetPositions(new Vector3[]{});
        }
    }
}
