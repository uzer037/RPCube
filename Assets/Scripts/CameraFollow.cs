using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public float tolerance = 0.01f;
    public float radius = 1f;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        if(target == null) {
            Debug.LogError("Target is not set");
        }
        cam = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null) {
            Vector3 delta = target.transform.position - cam.transform.position;
            delta.z = 0;
            if (delta.magnitude > tolerance + radius) {
                cam.transform.position += delta * Time.deltaTime;
            }
        }
    }
}
