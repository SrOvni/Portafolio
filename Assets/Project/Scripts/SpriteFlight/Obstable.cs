using UnityEngine;
namespace SpriteFlight
{
    public class Obstable : MonoBehaviour
    {
        [SerializeField] private float minSize = 0.5f;
        [SerializeField] private float maxSize = 2f;

        [SerializeField] private float minSpeed = 50;
        // [SerializeField] private float maxSpeed = 150;
        [SerializeField] private float maxSpinSpeed = 10;

        Rigidbody2D rb;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            var randomSize = Random.Range(minSize, maxSize);
            transform.localScale = new Vector3(randomSize, randomSize, 1);
            rb = GetComponent<Rigidbody2D>();

            var randomSpeed = Random.Range(minSpeed, minSpeed) / randomSize;

            var randoDirection = Random.insideUnitCircle;


            rb.mass = randomSize / maxSize;

            rb.AddForce(randoDirection * randomSpeed);

            var randomSpinSpeed = Random.Range(-maxSpinSpeed, maxSpinSpeed);
            rb.AddTorque(randomSpinSpeed);
        }
    }

}
