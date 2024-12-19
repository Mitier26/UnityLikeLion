using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    private Camera camera;
    private Transform owner;
    private Camera ui_camera;
    
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    public void UpdateOwner(Transform owner, Camera ui_camera)
    {
        this.owner = owner;
        this.ui_camera = ui_camera;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (owner != null && camera != null)
        {
            Vector3 screenPoint = camera.WorldToScreenPoint(owner.position);
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform.parent.GetComponent<RectTransform>(), screenPoint, ui_camera, out localPoint);
            
            transform.localPosition = localPoint;
            
        }
    }

}