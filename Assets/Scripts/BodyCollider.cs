using UnityEngine;
using System.Collections;

public class BodyCollider : MonoBehaviour
{
    public Transform head;
    
    void FixedUpdate()
    {
        float distanceFromFloor = Vector3.Dot(head.localPosition, Vector3.up);
        //capsuleCollider.height = Mathf.Max(capsuleCollider.radius, distanceFromFloor);
        float height = Mathf.Max(transform.localScale.x, distanceFromFloor) / 2;
        transform.localScale = new Vector3(transform.localScale.x, height, transform.localScale.z);
        transform.localPosition = head.localPosition - 0.5f * distanceFromFloor * Vector3.up;
    }
}