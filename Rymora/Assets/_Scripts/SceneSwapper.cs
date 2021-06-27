using UnityEngine;
using System.Collections;

public class SceneSwapper : MonoBehaviour {

	public void swapScene(string scene){
		Application.LoadLevel (scene);
		}
}
