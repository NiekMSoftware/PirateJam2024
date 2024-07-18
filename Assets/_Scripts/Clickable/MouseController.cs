using UnityEngine;

public class MouseController : MonoBehaviour
{
    public Camera Camera;
    public LayerMask TargetLayer;
    public float dragForce = 10f;

    private Rigidbody2D selectedRb;
    private Vector3 grabPointOffset;

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
                    Vector3 grabPoint = Camera.ScreenToWorldPoint(Input.mousePosition);
                    grabPointOffset = selectedRb.transform.InverseTransformPoint(grabPoint);

                    // Ensure the rigidbody is at interpolate
                    selectedRb.interpolation = RigidbodyInterpolation2D.Interpolate;
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
            Vector2 grabWorldPoint = selectedRb.transform.TransformPoint(grabPointOffset);

            // Calculate the force direction
            Vector2 forceDirection = targetPosition - grabWorldPoint;

            // Apply force to the grabbed point
            selectedRb.AddForceAtPosition(forceDirection * dragForce, grabWorldPoint);
        }
    }
}
