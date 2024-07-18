using UnityEngine;

public class MouseController : MonoBehaviour
{
    public Camera Camera;
    public LayerMask TargetLayer;
    private Rigidbody2D selectedRb;
    private Vector3 offset;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, TargetLayer);
            if (hit.collider != null)
            {
                selectedRb = hit.collider.attachedRigidbody;
                if (selectedRb != null)
                {
                    offset = selectedRb.transform.position - Camera.ScreenToViewportPoint(Input.mousePosition);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectedRb = null;
        }
    }

    private void FixedUpdate()
    {
        if (selectedRb != null)
        {
            Vector3 mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 targetPosition = new Vector2(mousePosition.x, mousePosition.y);
            Vector2 currentPosition = selectedRb.position;
            Vector2 direction = (targetPosition - currentPosition).normalized;
            float distance = Vector2.Distance(currentPosition, targetPosition);

            // Adjust force based on the distance to the target position
            float force = Mathf.Clamp(distance * 10, 0, 100);
            selectedRb.velocity = direction * force;
        }
    }
}
