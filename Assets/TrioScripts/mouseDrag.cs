using UnityEngine;
using System.Collections;

public class mouseDrag : MonoBehaviour {

	void OnMouseDrag()
	{
		Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y+45, 3);
		Vector3 objPosition = Camera.main.ScreenToWorldPoint (mousePosition);

		objPosition = new Vector3 (objPosition.x, objPosition.y, -17.0f);
		transform.position = objPosition;
	}
}
