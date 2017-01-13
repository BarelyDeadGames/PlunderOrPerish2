using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetCannonBall : MonoBehaviour {

	private Collider[] hitColliders;
	public float BlastRadius = 1.0F;
	public float explosionPower = 50000.0F; //100000.0F;
	public LayerMask explosionLayers;
	private bool hasCollided;

	void Start () 
	{

	}

	void Update () 
	{
		
	}

	void OnCollisionEnter(Collision col) 
	{
		if (!hasCollided) {
			hasCollided = true;
			ExplosionWork (col.contacts [0].point);
			Destroy (gameObject);
		}
	}

	void ExplosionWork(Vector3 explosionPoint) 
	{
		hitColliders = Physics.OverlapSphere (explosionPoint, BlastRadius, explosionLayers);

		foreach (Collider hitCol in hitColliders)
		{
			if (hitCol.GetComponent<Rigidbody> () != null) 
			{
				hitCol.GetComponent<Rigidbody> ().isKinematic = false;
				hitCol.GetComponent<Rigidbody> ().AddExplosionForce (explosionPower, explosionPoint, BlastRadius, 1, ForceMode.Impulse);
			}
			break;
		}

	}

		
}
