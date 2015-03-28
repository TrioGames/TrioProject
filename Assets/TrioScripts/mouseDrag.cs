using UnityEngine;
using System.Collections;

public class mouseDrag : MonoBehaviour {

	float distance = 20;

	void OnMouseDrag()
	{
		Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y+40, 3);
		Vector3 objPosition = Camera.main.ScreenToWorldPoint (mousePosition);

		transform.position = objPosition;


	}
}
