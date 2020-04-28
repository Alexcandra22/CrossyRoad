using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour
{
    float dist = 10f;
    Vector3 dir = new Vector3(0, -10, 0);
    RaycastHit hit;

    public void FixedUpdate()
    {
        Ray ray = new Ray(gameObject.transform.position, dir);
        Physics.Raycast(ray, out hit);
    
        if (hit.collider == null)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
}
