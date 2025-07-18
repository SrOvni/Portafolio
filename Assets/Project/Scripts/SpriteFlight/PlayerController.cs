using UnityEngine;
using UnityEngine.InputSystem;
namespace SpriteFlight
{
    public class PlayerController : MonoBehaviour
    {
        Rigidbody2D rb;
        public float thrustForce = 1;
        public float maxSpeed = 5;
        [SerializeField] private SpriteRenderer boosterSpriteRenderer;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            if (Mouse.current.leftButton.isPressed)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
                Vector2 direction = (mousePos - transform.position).normalized;
                transform.up = direction;
                rb.AddForce(direction * thrustForce);
                if (rb.linearVelocity.magnitude > maxSpeed)
                {
                    rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
                }
                boosterSpriteRenderer.enabled = true;
            }
            if (Mouse.current.leftButton.wasPressedThisFrame) boosterSpriteRenderer.enabled = true;
            else if (Mouse.current.leftButton.wasReleasedThisFrame) boosterSpriteRenderer.enabled = false;
        }

        void OnCollisionEnter2D(Collision2D _)
        {
            Destroy(gameObject);
        }
    }

}
