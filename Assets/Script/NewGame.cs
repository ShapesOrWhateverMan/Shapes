using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGame : MonoBehaviour {
	public string startLevel;
	public string levelSelect;

	public void Newgame(){
		Application.LoadLevel (startLevel); 
	}

	public void LevelSelect(){
		Application.LoadLevel (levelSelect);
	}

	public void QuitGame(){
		Application.Quit ();
	}

}
