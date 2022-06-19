using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
//	public RectTransform forEditorIphoneXImage;
	public RectTransform Panel;
	Rect LastSafeArea = new Rect (0, 0, 0, 0);

	void OnEnable ()
	{
//		Panel = GetComponent<RectTransform> ();
//		Debug.Log ("SafeArea");
		Refresh ();
	}

//	void Update ()
//	{
//		Refresh ();
//	}

	public void Refresh ()
	{
		Rect safeArea;

//		#if UNITY_EDITOR || UNITY_STANDALONE_LINUX || UNITY_WEBGL
//		safeArea = forEditorIphoneXImage.rect;
//		#elif UNITY_ANDROID || UNITY_IPHONE
		safeArea = GetSafeArea ();


//		#endif

		if (safeArea != LastSafeArea)
			ApplySafeArea (safeArea);
	}

	Rect GetSafeArea ()
	{
		return Screen.safeArea;
	}

	void ApplySafeArea (Rect r)
	{
		LastSafeArea = r;

		Vector2 anchorMin = r.position;
		Vector2 anchorMax = r.position + r.size;
		anchorMin.x /= Screen.width;
		anchorMin.y /= Screen.height;
		anchorMax.x /= Screen.width;
		anchorMax.y /= Screen.height;
		Panel.anchorMin = anchorMin;
		Panel.anchorMax = anchorMax;
	}
}
