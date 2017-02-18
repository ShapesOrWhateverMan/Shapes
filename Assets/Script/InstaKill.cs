using UnityEngine;

public class InstaKill : MonoBehaviour {

	public void OnTriggerEnte2D(Collider2D other){
		var player = other.GetComponent<shapes> ();
		if (player == null)
			return;

		levelManage.instance.killPlayer ();
	}
}
