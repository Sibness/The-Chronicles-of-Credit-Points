using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;

    private int currentIndex = 0;
    private bool isReversing = false;
    private bool isMoving = false;

    public void MoveToWaypoints(Transform[] points, float moveSpeed)
    {
        waypoints = points;
        speed = moveSpeed;
        currentIndex = 0;
        isReversing = false;
        isMoving = true;
    }

    public void StopMoving()
    {
        isMoving = false;
    }

    void Update()
    {
        if (!isMoving || waypoints == null || waypoints.Length == 0) return;

        Transform target = waypoints[currentIndex];
        Vector3 direction = (target.position - transform.position).normalized;
        float step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            if (!isReversing)
            {
                currentIndex++;
                if (currentIndex >= waypoints.Length)
                {
                    isReversing = true;
                    currentIndex = waypoints.Length - 2;
                }
            }
            else
            {
                currentIndex--;
                if (currentIndex < 0)
                {
                    isReversing = false;
                    currentIndex = 1;
                }
            }
        }
    }
}
