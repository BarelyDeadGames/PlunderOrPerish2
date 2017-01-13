using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunitionCannonball : MonoBehaviour {

	public string cName;
	public string correspondingWeapon;
	public int damage;
	public bool active;
	private bool invokeActive;

	private PlayerSetup ps;
	private List<GameObject> ps_list_ps_explosionSmall;

	void Start () {

		cName = "Cannonball";
		correspondingWeapon = "Cannon01";
		damage = 50;

		ps = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponent<PlayerSetup> ();
		ps_list_ps_explosionSmall = ps.list_ps_ExplosionSmall;

	}

	void Update () {
		if (active == true && invokeActive == false) {
			invokeActive = true;
			Invoke ("Reset", 2);
		}
			
	}

	public void Reset() {
		GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
		GetComponent<Rigidbody> ().useGravity = false;
		GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
		transform.position = transform.parent.position;

		active = false;
		invokeActive = false;



	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "EnemyShip") {
			//print ("velocity: " + GetComponent<Rigidbody> ().velocity);

			Vector3 tor = transform.position - other.transform.position;
			other.GetComponent<Rigidbody> ().AddTorque (tor.z*-1000000,0,tor.x*1000000);
			CancelInvoke ();
			Invoke ("Reset",0);
			//print ("hit: " + tor);
			for (int i = 0; i < ps_list_ps_explosionSmall.Count; i++) {
				ParticleSystem es = ps_list_ps_explosionSmall [i].GetComponent < ParticleSystem> ();
				if (!es.isPlaying) {
					ps_list_ps_explosionSmall [i].gameObject.transform.position = transform.position;
					es.Play ();
					break;
				}
			}

		}
	}
}
