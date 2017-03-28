using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour {

	GravityAttractor planet;
	private Rigidbody rbody;

	private void Awake()
	{
		planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
		rbody = GetComponent<Rigidbody>();
		rbody.useGravity = false;
		rbody.constraints = RigidbodyConstraints.FreezeRotation;

	}

	private void FixedUpdate()
	{
		planet.Attract(transform);
	}
}
