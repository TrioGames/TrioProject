using UnityEngine;
using System.Collections;

public class mouseDrag : MonoBehaviour {

	void OnMouseDrag()
	{
		Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y+45, 3);
		Vector3 objPosition = Camera.main.ScreenToWorldPoint (mousePosition);

		objPosition = new Vector3 (objPosition.x, objPosition.y, -17.0f);
		transform.position = objPosition;
		Destroy(transform.collider);
		gameObject.AddComponent<MeshCollider>();

		if (Gamer.instance.GameStatus == Constants.GAME_STATUS_PAUSE) {
			//Gamer.instance.StartGame ();
			mainMenuButtons.instance.ResumeGame ();
		}
			
	}
}
