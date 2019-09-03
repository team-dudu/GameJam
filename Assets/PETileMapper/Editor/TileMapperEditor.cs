using UnityEngine;
using System.Collections;
using UnityEditor;

//[CanEditMultipleObjects]
[CustomEditor(typeof(TileMapper))]
public class TileMapperEditor : Editor {

	public override void OnInspectorGUI(){
		base.OnInspectorGUI ();
		GUILayout.Label ("Note: Both the above files must be in a Resources folder.");
		GUILayout.Label ("Note: Don't specify the file extension above.");
		GUILayout.Label ("Note: Names must not match.");
		if (GUILayout.Button ("Clear")) {
			TileMapper tilemapper = (TileMapper)target;
			tilemapper.Clear ();
		}
		if (GUILayout.Button ("Generate")) {
			TileMapper tilemapper = (TileMapper)target;
			tilemapper.Generate ();
		}
	}
}
