using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour {

    public float gravity = -10f;

    public void Attract(Transform body)
    {
        Debug.Log(body.position - transform.position + "Not normilized");
        Vector3 targetDir = (body.position - transform.position).normalized;
        Debug.Log("Normilized " + targetDir);
        Vector3 bodyUp = body.up;

        body.rotation = Quaternion.FromToRotation(bodyUp, targetDir) * body.rotation;
        body.GetComponent<Rigidbody>().AddForce(targetDir * gravity);
    }
}
