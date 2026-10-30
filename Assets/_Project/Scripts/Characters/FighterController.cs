using UnityEngine;

namespace LegendsOfTheArena.Characters
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public sealed class FighterController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField, Min(0f)] private float moveSpeed = 7f;
        [SerializeField, Min(0f)] private float jumpVelocity = 13f;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundMask = ~0;
        [SerializeField, Min(0.01f)] private float groundRadius = 0.12f;

        [Header("Player input")]
        [SerializeField] private bool playerTwo;
        [SerializeField] private KeyCode leftKey = KeyCode.A;
        [SerializeField] private KeyCode rightKey = KeyCode.D;
        [SerializeField] private KeyCode jumpKey = KeyCode.W;
        [SerializeField] private KeyCode crouchKey = KeyCode.S;

        private Rigidbody2D body;
        private Collider2D bodyCollider;
        private float horizontal;
        private bool grounded;
        private bool controlsLocked;

        public float Facing { get; private set; } = 1f;
        public bool IsGrounded => grounded;
        public bool IsCrouching { get; private set; }
        public bool IsMoving => Mathf.Abs(horizontal) > 0.01f;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            bodyCollider = GetComponent<Collider2D>();
            body.freezeRotation = true;
            body.interpolation = RigidbodyInterpolation2D.Interpolate;
        }

        private void Update()
        {
            if (controlsLocked) { horizontal = 0f; return; }

            bool left = Input.GetKey(leftKey);
            bool right = Input.GetKey(rightKey);
            horizontal = (right ? 1f : 0f) - (left ? 1f : 0f);
            IsCrouching = Input.GetKey(crouchKey);

            if (horizontal != 0f) Facing = Mathf.Sign(horizontal);
            if (Input.GetKeyDown(jumpKey) && grounded && !IsCrouching)
                body.velocity = new Vector2(body.velocity.x, jumpVelocity);
        }

        private void FixedUpdate()
        {
            if (groundCheck != null)
                grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundMask) != null;
            else
                grounded = Mathf.Abs(body.velocity.y) < 0.02f &&
                           bodyCollider.bounds.min.y <= transform.position.y - 0.2f;

            float targetSpeed = controlsLocked || IsCrouching ? 0f : horizontal * moveSpeed;
            body.velocity = new Vector2(targetSpeed, body.velocity.y);
        }

        public void SetControlsLocked(bool locked)
        {
            controlsLocked = locked;
            if (locked && body != null)
                body.velocity = new Vector2(0f, body.velocity.y);
        }

        public void Face(float direction)
        {
            if (Mathf.Abs(direction) < 0.01f) return;
            Facing = Mathf.Sign(direction);
        }

        private void OnDrawGizmosSelected()
        {
            if (groundCheck == null) return;
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }
}
