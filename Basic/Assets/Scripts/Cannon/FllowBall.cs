using UnityEngine;

public class FollowBall : MonoBehaviour
{
    public Transform offset;
    public GameObject cannonBall;
    public float followSpeed = 5f;
    public float minDistance = 2f;
    public float rotationSpeed = 5f;

    private void Start()
    {
        if (offset == null)
        {
            offset = new GameObject().transform;
            offset.position = transform.position;
            offset.rotation = transform.rotation;
        }
    }

    private void Update()
    {
        if (cannonBall == null)
        {
            cannonBall = GameObject.FindGameObjectWithTag("CannonBall");
        }

        if (cannonBall != null)
        {
            float distance = Vector3.Distance(transform.position, cannonBall.transform.position);

            if (distance > minDistance)
            {
                transform.position = Vector3.Lerp(transform.position, cannonBall.transform.position, followSpeed * Time.deltaTime);
            }

            Quaternion targetRotation = Quaternion.LookRotation(cannonBall.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, offset.position, followSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, offset.rotation, rotationSpeed * Time.deltaTime);
        }
    }
}