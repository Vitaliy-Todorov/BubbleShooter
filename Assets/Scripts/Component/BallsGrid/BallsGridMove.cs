using System.Collections;
using UnityEngine;

namespace Component.BallsGrid
{
    public class BallsGridMove : MonoBehaviour
    {
        [SerializeField]
        private float _speed;
        private Vector3 _oneStep;

        public void Construct(Vector3 oneStep)
        {
            _oneStep = oneStep;
        }

        public void MoveOneStep() =>
            StartCoroutine(MoveToPoint(transform.position, transform.position - _oneStep));
        
        private IEnumerator MoveToPoint(Vector3 startPoint, Vector3 endPoint)
        {
            float distance = Vector3.Distance(startPoint, endPoint);
            float rate = _speed / distance;

            for (float t = 0; t < 1; t += rate * Time.deltaTime)
            {
                transform.position = Vector3.Lerp(startPoint, endPoint, Mathf.SmoothStep(0f, 1f, t));
                yield return null;
            }
        }
    }
}