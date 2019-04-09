using UnityEngine;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

public class SocketClient : MonoBehaviour {

    private Queue<Pose> poseQueue = new Queue<Pose>();

    [SerializeField]
    private string address = "localhost:3000";

    [SerializeField]
    private HumanoidBoneController boneController;

    [SerializeField]
    private SkinnedMeshRenderer faceRenderer;

    void Start()
    {
        // Todo: TCP connect
    }

    // Todo: TCP receive callback

    void Update()
    {
        // Dequeuing and run commands
        while (poseQueue.Count != 0)
        {
            Pose pose = poseQueue.Dequeue();
            // Adopting pose to model
            AdoptPoseToBone(boneController, pose);
            // Set eye opening weight
            SetEyesWeight((1.0f - pose.face.left_eye.open) * 85.0f);
        }
    }

    void OnDestroy()
    {
        // Todo: TCP close
    }

    private void AdoptPoseToBone(HumanoidBoneController boneController, Pose pose)
    {
        boneController.SetBoneRotation(HumanBodyBones.LeftShoulder, pose.left_arm.shoulder);
        boneController.SetBoneRotation(HumanBodyBones.LeftLowerArm, pose.left_arm.elbow);
        boneController.SetBoneRotation(HumanBodyBones.RightShoulder, pose.right_arm.shoulder);
        boneController.SetBoneRotation(HumanBodyBones.RightLowerArm, pose.right_arm.elbow);
        boneController.SetBoneRotation(HumanBodyBones.LeftUpperLeg, pose.left_leg.groin);
        boneController.SetBoneRotation(HumanBodyBones.LeftLowerLeg, pose.left_leg.knee);
        boneController.SetBoneRotation(HumanBodyBones.RightUpperLeg, pose.right_leg.groin);
        boneController.SetBoneRotation(HumanBodyBones.RightLowerLeg, pose.right_leg.knee);
        boneController.SetBoneRotation(HumanBodyBones.Neck, pose.neck);
    }

    private void SetEyesWeight(float weight)
    {
        faceRenderer.SetBlendShapeWeight(0, weight);
    }

    [System.Serializable]
    public class Eye
    {
        public float open;
    }

    [System.Serializable]
    public class Mouth
    {
        public float open;
        public float form;
    }

    [System.Serializable]
    public class Face
    {
        public Eye left_eye;
        public Eye right_eye;
        public Vector3 look_at_pos;
        public Mouth mouth;
    }

	[System.Serializable]
	public class Arm
	{
		public Vector3 shoulder;
		public Vector3 elbow;
	}

	[System.Serializable]
	public class Leg
	{
		public Vector3 groin;
		public Vector3 knee;
	}

	[System.Serializable]
	public class Pose
	{
        public Face face;
		public Arm left_arm;
		public Arm right_arm;
		public Leg left_leg;
		public Leg right_leg;
		public Vector3 neck;
        public float left_wheel_speed;
        public float right_wheel_speed;
	}
}
