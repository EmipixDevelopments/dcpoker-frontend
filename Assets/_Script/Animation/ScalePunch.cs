using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePunch : MonoBehaviour
{
	public Vector3 scaleTo = new Vector3 (1.1f, 1.1f, 1.1f);
	public float time = 1f;
	// Use this for initialization

	void OnEnable ()
	{
		LeanTween.scale (gameObject, scaleTo, time).setEase (LeanTweenType.punch).setLoopPingPong ();
	}

	void OnDisable ()
	{
		LeanTween.cancel (gameObject);
		LeanTween.scale (gameObject, new Vector3 (1f, 1f, 1f), 0);
	}
}
