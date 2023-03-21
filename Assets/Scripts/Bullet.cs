using UnityEngine;

public class Bullet : MonoBehaviour {

	private Transform target;

	public float speed = 70f;
	public int damage = 50;

	public float explosionRadius = 0f;
	public GameObject impactEffect;
	
	// look for the target
	public void Seek(Transform target)
	{
		this.target = target;
	}
	
	private void Update()
	{
		// target is already dead
		if (target == null)
		{
			Destroy(gameObject);
			return;
		}
		
		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		// if the bullet reaches the target
		if (dir.magnitude <= distanceThisFrame)
		{
			HitTarget();
			return;
		}

		// go to the target and look at the target
		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
		transform.LookAt(target);
	}

	void HitTarget ()
	{
		// instantiate impact effect
		GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
		Destroy(effectIns, 5f);

		// if grenade
		if (explosionRadius > 0f)
		{
			Explode();
		} 
		
		else
		{
			Damage(target);
		}

		Destroy(gameObject);
	}

	void Explode ()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach (Collider collider in colliders)
		{
			if (collider.CompareTag("Enemy"))
			{
				Damage(collider.transform);
			}
		}
	}

	void Damage (Transform enemy)
	{
		Enemy e = enemy.GetComponent<Enemy>();

		if (e != null)
		{
			e.TakeDamage(damage);
		}
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}
}
