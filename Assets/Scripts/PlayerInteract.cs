using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float reach = 2f;
    public float reachWidth = 0.5f;
    private Vector3 interactDirection = Vector3.right;
    private Interactive target = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Vector3 getInteractPosition() {
        return transform.position + interactDirection;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position + interactDirection.normalized * reachWidth, transform.position + interactDirection * reach);
        if(target == null) {
            Gizmos.DrawWireCube(transform.position + interactDirection * reach, Vector3.one * 2 * reachWidth);
        } else {
            Gizmos.DrawWireCube(target.transform.position, target.transform.localScale);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Find selection area
        Vector3 delta = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        if (delta.magnitude > 0.1f) {
            interactDirection = delta;
            interactDirection.Normalize();
            interactDirection.x = Mathf.Round(delta.x);
            interactDirection.y = Mathf.Round(delta.y);
            interactDirection.z = 0;
        }
        
        // Search for objects in selection area
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position + interactDirection.normalized * reachWidth, 0.5f, interactDirection, reach - reachWidth);
        Interactive foundTarget = null;
        foreach (RaycastHit2D hit in hits) {
            foundTarget = hit.transform.gameObject.GetComponent<Interactive>();
            if (foundTarget != null) {
                break;
            }
        }
        target = foundTarget;
    }
}
