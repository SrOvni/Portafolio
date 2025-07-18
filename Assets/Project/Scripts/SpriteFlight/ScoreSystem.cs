using UnityEngine;

namespace SpriteFlight
{
    public class ScoreSystem : MonoBehaviour
    {
        private float elapsedTime = 0f;
        void Update()
        {
            elapsedTime += Time.deltaTime;
        }
    }
}
