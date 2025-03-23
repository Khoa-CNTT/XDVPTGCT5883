using UnityEngine;

namespace dang
{

    public class Waypoint : MonoBehaviour
    {
        [SerializeField] private Vector3[] points;

        public Vector3[] Points => points;
        public Vector3 CurrentPosition => currentPosition;

        private Vector3 currentPosition;
        private bool isGameStarted;

        void Start()
        {
            isGameStarted = true;
            currentPosition = transform.position;
        }

        public void OnDrawGizmos()
        {
            if (!isGameStarted && transform.hasChanged)
            {
                currentPosition = transform.position;
            }

            for (int i = 0; i < points.Length; i++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(points[i] + currentPosition, 0.5f);

                if (i < points.Length - 1)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(points[i] + currentPosition, points[i + 1] + currentPosition);
                }
            }
        }
    }
}
