using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidBoneController : MonoBehaviour {

    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void SetBoneRotation(HumanBodyBones bone, Vector3 eular)
    {
        SetBoneRotation(bone, Quaternion.Euler(eular));
    }

    public void SetBoneRotation (HumanBodyBones bone, Quaternion q)
    {
        animator.GetBoneTransform(bone).localRotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
    }
}
