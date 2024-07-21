using UnityEngine;

namespace PirateJam.Controllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Basic movement variables")]
        [SerializeField, Range(0.1f, 1.0f)] private float groundDecay;
        [SerializeField] private float acceleration;

        [Space]
        [SerializeField] private float maxSpeed;

        [Header("Wwise")]
        [SerializeField] private AK.Wwise.Event footstepEvent;
        [SerializeField] private float timeBetweenFootsteps;
        [SerializeField] private AK.Wwise.RTPC wwiseSpeed;

        private Vector2 input;
        private Rigidbody2D body;

        // Wwise
        private bool canTriggerFootstepEvent = true;
        private float lastFootstepTime;

        private void Start()
        {
            body = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            GetInput();
        }

        private void FixedUpdate()
        {
            HandleMovement();
            ApplyFriction();
            HandleAudioEvents();
        }

        private void GetInput()
        {
            var x = Input.GetAxisRaw("Horizontal");
            var y = Input.GetAxisRaw("Vertical");
            input = new Vector2(x, y);
        }

        private void HandleMovement()
        {
            // Return if no input is given
            if (input == Vector2.zero) return;

            // Increment velocity by acceleration, then clamp the velocity's magnitude with 
            // the max speed of the player
            Vector2 increment = input * acceleration;
            Vector2 newVel = body.velocity + increment;
            newVel = Vector2.ClampMagnitude(newVel, maxSpeed);

            // Change velocity
            body.velocity = newVel;
        }

        private void ApplyFriction()
        {
            // Apply friction if only no input is being received
            if (input == Vector2.zero)
            {
                // multiply ground decay to the velocity
                Vector2 newVelocity = new Vector2(body.velocity.x * groundDecay, body.velocity.y * groundDecay);
                body.velocity = newVelocity;
            }
        }

        private void HandleAudioEvents()
        {
            // Set Wwise speed parameter, which is being used to control footstep volume
            wwiseSpeed.SetGlobalValue(body.velocity.magnitude);

            // Trigger footstep event if there is user input, velocity is not 0,
            // and the event is able to be triggered
            if (canTriggerFootstepEvent && body.velocity != Vector2.zero && input != Vector2.zero)
            {
                // Trigger Wwise event
                footstepEvent.Post(gameObject);

                // Reset the footstep timer and stop the event from triggering again until enough time has elapsed
                lastFootstepTime = Time.time;
                canTriggerFootstepEvent = false;
            }
            else
            {
                // Enable the footstep event if enough time has elapsed since the last event
                if (Time.time - lastFootstepTime > timeBetweenFootsteps) canTriggerFootstepEvent = true;
            }
        }
    }
}