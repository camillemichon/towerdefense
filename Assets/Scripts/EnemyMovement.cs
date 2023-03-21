using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {

	private Transform target;
	private int waypointIndex = 0;

	private Enemy enemy;

	private void Start()
	{
		enemy = GetComponent<Enemy>();
		target = Waypoints.points[waypointIndex];
	}

	private void Update()
	{
		// go to the target
		Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

		// target reached = go to the next waypoint
		if (Vector3.Distance(transform.position, target.position) <= 0.4f)
		{
			GetNextWaypoint();
		}
		
		// if the enemy is not slow anymore reset the speed
		enemy.speed = enemy.startSpeed;
	}

	private void GetNextWaypoint()
	{
		// last waypoint
		if (waypointIndex >= Waypoints.points.Length - 1)
		{
			EndPath();
			return;
		}

		// change to the next waypoint
		target = Waypoints.points[++waypointIndex];
	}

	private void EndPath()
	{
		PlayerStats.lives--;
		Destroy(gameObject);
	}
}
