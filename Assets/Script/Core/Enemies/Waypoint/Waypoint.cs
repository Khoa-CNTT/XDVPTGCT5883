using UnityEngine;

namespace dang
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] private Vector3[] points;

        public Vector3[] Points => points;
        public Vector3 CurrentPosition => currentPosition;

        [Range(0.01f, 1f)]
        public float SnapValue = 0.1f; // <-- Thêm dòng này

        private Vector3 currentPosition;
        private bool isGameStarted;

        void Start()
        {
            isGameStarted = true;
            currentPosition = transform.position;
        }

        public Vector3 GetWaypointPosition(int index)
        {
            if (index < 0 || index >= points.Length)
            {
                Debug.LogError("Index out of bounds for waypoints array.");
                return Vector3.zero;
            }
            return points[index] + currentPosition;
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
