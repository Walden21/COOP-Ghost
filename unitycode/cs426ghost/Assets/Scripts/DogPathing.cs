using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class DogPathing : MonoBehaviour
{

	public GameObject ghost;
	public GameObject human;

	public GameObject dogModel;
	public GameObject dogBillboard;
	public GameObject growlCollider;
	public GameObject dogName;

	private Rigidbody rb;

	public Transform[] points;
	private int destPoint = 0;
	private NavMeshAgent agent;
	public int state = 1;

	public const int guarding = 1;
	public const int followingGhost = 2;
	public const int growling = 3;
	public const int playingWithToy = 4;
	private bool trans = false;

	public bool hasToy = false;
	public int treats = 0;

	public const float distToHuman = 15f;
	public const float distToGhost = 25f;

	AudioSource audio;

	public AudioClip barkingAudio;
	public AudioClip whiningAudio;

	void Start()
	{
		//rb = GetComponent<Rigidbody> ();
		agent = GetComponent<NavMeshAgent>();
		agent.isStopped = true;
		audio = GetComponent<AudioSource> ();
	}

	public void catchTreat(){
		treats++;
		if (treats >= 3) {
			catchToy ();
		}
	}

	public void catchToy(){
		state = playingWithToy;
		dogBillboard.SetActive (false);
		growlCollider.SetActive (false);
		audio.clip = null;
		//add collider to show dog name
	}

	/* helper functions */

	void pauseMove(){
		agent.isStopped = true;
	}

	void resumeMove(){
		agent.isStopped = false;
	}

	void Look(GameObject tar){
		var tarRotation = Quaternion.LookRotation (tar.transform.position - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, tarRotation, 2f * Time.deltaTime);
	}


	/* states */

	void Guard(){
		//Debug.Log ("guard");
		if (trans) {
			dogBillboard.SetActive (false);
			audio.clip = null;
			if (Vector3.Distance (points [1].position, transform.position) >= 1f) {
				agent.destination = points [1].position;
				resumeMove ();
			} 
			trans = false;
		}
		else if (Vector3.Distance (points [1].position, transform.position) < 1f) {
			pauseMove ();
		}
	}
		
	void Follow(){
		if (trans == true) {
			audio.clip = whiningAudio;
			audio.Play ();

			dogBillboard.SetActive (false);
			agent.destination = points [0].position;
			resumeMove ();
			trans = false;
		}
		//Debug.Log ("follow");
		agent.destination = points [0].position;
		Look(ghost);
	}

	void Growl(){
		
		//Debug.Log ("growl");
		if (trans == true) {
			audio.clip = barkingAudio;
			audio.Play ();
			pauseMove ();
			trans = false;
			dogBillboard.SetActive (true);
		}
			
		Look (human);
	}

	void PlayWithToy(){
		//Debug.Log ("playing with toy");
		if (Vector3.Distance (human.transform.position, transform.position) < distToHuman - 3f) {
			hasToy = true;
			dogName.SetActive (true);
			Look (human);
		} else {
			dogName.SetActive (false);
		}
	}

	void FixedUpdate()
	{
		
		//FSM
		if (state == guarding) {
			Guard ();

			//transitions
			if (Vector3.Distance (human.transform.position, transform.position) < distToHuman) {
				state = growling;
				trans = true;
			} else if (Vector3.Distance (ghost.transform.position, transform.position) < distToGhost) {
				trans = true;
				state = followingGhost;
			}
		} else if (state == followingGhost) {
			Follow ();

			//transitions
			if (Vector3.Distance (human.transform.position, transform.position) < distToHuman) {
				state = growling;
				trans = true;
			} else if (Vector3.Distance (ghost.transform.position, transform.position) > distToGhost) {
				state = guarding;
				trans = true;
			}
		} else if (state == growling) {
			Growl ();

			//transitions
			if (Vector3.Distance (human.transform.position, transform.position) > distToHuman) {
				if (Vector3.Distance (ghost.transform.position, transform.position) < distToGhost) {
					state = followingGhost;
					trans = true;
				} else {
					state = guarding;
					trans = true;
				}
			}
		} else if (state == playingWithToy) {
			PlayWithToy ();
		}
	}
}
