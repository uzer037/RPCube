using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public float tolerance = 0.01f;
    public float radius = 1f;
    public float speed = 1.5f;
    [Range(0.001f,1f)]
    public float shakeSpeed = 0.15f;
    private Camera cameraComponent;
    private Transform camRig;
    private Transform camShaker;

    // Start is called before the first frame update
    void Start()
    {
        if(target == null) {
            Debug.LogError("Target is not set");
        }
        cameraComponent = this.GetComponent<Camera>();
        camShaker = cameraComponent.transform;
        camRig = camShaker.parent;
        if (camRig == null) {
            Debug.LogError("Camera configured incorrectly. It MUST have parent object to support effects.");
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            camShaker.localPosition = -Vector3.down * Random.Range(0.75f,1.25f) + Vector3.right * Random.Range(-0.25f,0.25f);
        }
        if (target != null) {
            Vector3 delta = target.transform.position - camRig.position;
            delta.z = 0;
            if (delta.magnitude > tolerance + radius) {
                camRig.position += delta * Time.deltaTime * speed;
            }
        }
        if (camShaker.localPosition.magnitude >= 0.01f) {
            camShaker.localPosition -= camShaker.localPosition * shakeSpeed;
        } else {
            camShaker.localPosition = Vector3.zero;
        }
    }
}
