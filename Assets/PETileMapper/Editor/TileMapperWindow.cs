using UnityEngine;
using System.Collections;
using UnityEditor;

public class TileMapperWindow : EditorWindow {//Hold left ctrl to delete,shift to rotate
	string tilesetName;
	static Vector3[] rect = new Vector3[4];
	enum EditModes{None,TileMap,Colliders,Prefabs}
	EditModes editmode = EditModes.None;
	enum ColliderTypes{box,circle,custom1,custom2,custom3}
	ColliderTypes collidertype = ColliderTypes.box;
	Sprite[] sprites;
	int currentSpriteIndex;
	int columns = 8;
	int tilesize = 64;
	int tileoffsety = 120;
	bool delete = false;
	GameObject currentCursor = null;
	Transform currentParent = null;
	int currentCursorRotation = 0;
	int currentLayer = 0;
	float layeroffset = -0.01f;
	string currentPrefabName;
	GameObject currentPrefab;

	// Add menu named "My Window" to the Window menu
	[MenuItem ("Window/Tile Map Editor")]
	static void Init () {
		TileMapperWindow window = (TileMapperWindow)EditorWindow.GetWindow (typeof (TileMapperWindow));
		window.title = "Tile Map Editor";
	}

	public void OnEnable(){
		SceneView.onSceneGUIDelegate = SceneUpdate;
	}

	void SceneUpdate(SceneView sceneview)
	{
		if (editmode == EditModes.TileMap || editmode == EditModes.Colliders || editmode == EditModes.Prefabs) {
			int controlID = GUIUtility.GetControlID (FocusType.Passive);
			HandleUtility.AddDefaultControl (controlID);
			Tools.current = Tool.None;
			HandleCursor ();
			Event e = Event.current;
			Ray r = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
			Vector3 mousePos = r.origin;
			int cursorX = Mathf.RoundToInt(mousePos.x / 1);
			int cursorY = Mathf.RoundToInt(mousePos.y / 1);
			if (currentCursor) {
				currentCursor.transform.position = new Vector3 (cursorX, cursorY, currentLayer * layeroffset);
			}
			DrawRect(cursorX, cursorY, 1, 1, Color.white, new Color(1, 1, 1, 0f));
			if ((e.type == EventType.MouseDown || e.type == EventType.MouseDrag) && e.button < 1) {
				if (editmode == EditModes.TileMap) {
					PlaceTile(cursorX,cursorY,currentLayer,false,currentSpriteIndex,"sprite_basic",delete,null);	
				}
				if (editmode == EditModes.Colliders) {
					PlaceTile(cursorX,cursorY,0,false,currentSpriteIndex,collidertype.ToString() + "collider2D",delete,null);	//Colliders all on same layer
				}
				if (editmode == EditModes.Prefabs) {
					PlaceTile(cursorX,cursorY,currentLayer,false,currentSpriteIndex,currentPrefab.name,delete,currentPrefab);
				}
			}
		}
	}

	void PlaceTile(int x,int y,int tilelayer,bool flipX,int tileindex,string prefabname,bool delete,GameObject prefab){
		string tilename = "tile x:" + x + " y:" + y + " layer:" + tilelayer + " " + prefabname;
		GameObject go = GameObject.Find (tilename);
		if (delete) {
			GameObject.DestroyImmediate(go);
			return;
		}
		if (!go) {//No existing tile here so create a new one
			if (prefab) {//prefab specified so use this
				go = (GameObject)PrefabUtility.InstantiatePrefab (prefab);
			} else {
				GameObject resourceprefab = Resources.Load<GameObject> (prefabname);
				go = (GameObject)PrefabUtility.InstantiatePrefab (resourceprefab);
			}
		}
		if (!go) {
			Debug.Log("Prefab doesn't exist!");
			return;
		}
		SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer> ();
		if(spriteRenderer && !prefab) spriteRenderer.sprite = sprites [currentSpriteIndex];//Only amend if a tile
		go.name = tilename;
		go.transform.position = new Vector3 (x, y, tilelayer * layeroffset);
		if(currentCursor) go.transform.rotation = currentCursor.transform.rotation;

//		GameObject parentGO = (GameObject)Selection.activeObject;
//		if (parentGO) {
//			go.transform.parent = parentGO.transform;
//		}
		if (currentParent) {
			go.transform.parent = currentParent;
		}
	}

	void CreateCursor(){
		if (currentCursor) GameObject.DestroyImmediate (currentCursor);
		if (editmode != EditModes.TileMap) return; //Only create cursor in tilemap mode
		GameObject prefab = Resources.Load<GameObject> ("sprite_basic");
		currentCursor = (GameObject)PrefabUtility.InstantiatePrefab (prefab);
		SpriteRenderer spriteRenderer = currentCursor.GetComponent<SpriteRenderer> ();
		spriteRenderer.sprite = sprites [currentSpriteIndex];//sprites[0];
		currentCursor.name = "cursor";
	}

	void HandleCursor(){//Handles rotation and delete mode of cursor
		Event e = Event.current;
		if (currentCursor && e.type == EventType.KeyDown && (e.keyCode == KeyCode.LeftAlt || e.keyCode == KeyCode.R)) {
			currentCursorRotation++;
			currentCursor.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, -90 * currentCursorRotation));
			Debug.Log("rot:" + currentCursor.transform.localRotation);
		}
		if (e.type == EventType.KeyDown && (e.keyCode == KeyCode.LeftControl || e.keyCode == KeyCode.D)) {
			delete = true;
			if (currentCursor) GameObject.DestroyImmediate (currentCursor);
		}
		if (e.type == EventType.KeyUp && (e.keyCode == KeyCode.LeftControl || e.keyCode == KeyCode.D)) {
			delete = false;
			CreateCursor();
		}

	}

	void OnGUI () {
		tilesetName = EditorGUILayout.TextField ("Tileset Name", tilesetName);
		currentParent = EditorGUILayout.ObjectField ("Parent",currentParent, typeof(Transform),true) as Transform;
		if (editmode == EditModes.TileMap) {
			if (GUILayout.Button ("Stop Editing")) {
				editmode = EditModes.None;
				GameObject.DestroyImmediate (currentCursor);
				currentCursor = null;
				Tools.current = Tool.Move;
			} 
			columns = (int)EditorGUILayout.Slider ("Columns", columns,1,64);
			tilesize = (int)EditorGUILayout.Slider ("Tile Size", tilesize,16,128);
			currentLayer = (int)EditorGUILayout.Slider ("Layer", currentLayer,0,16);
			HandleCursor ();
			int x = 0;
			int y = 0;
			for (int i = 0; i < sprites.Length; i++) {
				Sprite sprite = sprites [i];
				Rect trect = sprite.textureRect;
				Rect srect = new Rect (trect.x / sprite.texture.width, trect.y / sprite.texture.height, trect.width / sprite.texture.width, trect.height / sprite.texture.height);
				if (GUI.Button (new Rect (tilesize * x, (tilesize * y) + tileoffsety, tilesize, tilesize), "")) {
					currentSpriteIndex = i;
					currentCursorRotation = 0;
					CreateCursor ();
				}
				int margin = tilesize / 8;//Scale margin to tilesize
				GUI.DrawTextureWithTexCoords (new Rect ((tilesize * x) + margin / 2, + (tilesize * y) + tileoffsety + margin / 2, tilesize - margin, tilesize - margin), sprite.texture, srect, true);
				x++;
				if (x >= columns) {
					x = 0;
					y++;
				}
			}
		} 
		if (editmode == EditModes.None) {
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("Edit TileMap")) {
				editmode = EditModes.TileMap;
				sprites = Resources.LoadAll<Sprite> (tilesetName);
				Debug.Log ("sprites:" + sprites.Length);
				//EditorUtility.SetDirty(Selection.activeGameObject);
				CreateCursor ();
			}
			if (GUILayout.Button ("Edit Colliders")) {
				editmode = EditModes.Colliders;
			}
			if (GUILayout.Button ("Edit Prefabs")) {
				editmode = EditModes.Prefabs;
			}
			GUILayout.EndHorizontal ();
		}
		if (editmode == EditModes.Colliders) {
			if (GUILayout.Button ("Stop Editing")) {
				editmode = EditModes.None;
			}
			collidertype = (ColliderTypes)EditorGUILayout.EnumPopup("Collider", collidertype);
		}
		if (editmode == EditModes.Prefabs) {
			if (GUILayout.Button ("Stop Editing")) {
				editmode = EditModes.None;
			}
			//currentPrefabName = EditorGUILayout.TextField ("Prefab Name", currentPrefabName);
			currentPrefab = EditorGUILayout.ObjectField ("Prefab",currentPrefab, typeof(GameObject),false) as GameObject;
			currentLayer = (int)EditorGUILayout.Slider ("Layer", currentLayer,0,16);
		}

	}


	void OnDestroy(){
		GameObject.DestroyImmediate (currentCursor);
	}

	void DrawGrid(){
		float width = 1.0f;
		float height = 1.0f;
		Vector3 pos = Camera.current.transform.position;
		for (float y = pos.y - 800.0f; y < pos.y + 800.0f; y+= height){
			Debug.DrawLine(new Vector3(-1000000.0f, (Mathf.Floor(y/height) * height)+height, 0.0f),new Vector3(1000000.0f, Mathf.Floor(y/height) * height, 0.0f));
		}
		for (float x = pos.x - 1200.0f; x < pos.x + 1200.0f; x+= width){
			Debug.DrawLine(new Vector3((Mathf.Floor(x/width) * width)+width, -1000000.0f, 0.0f),new Vector3(Mathf.Floor(x/width) * width, 1000000.0f, 0.0f));
		}
	}
	void DrawRect(int x, int y, int sizeX, int sizeY, Color outline, Color fill)
	{
		Handles.color = Color.white;
		var min = new Vector2(x - 0.5f,  y - 0.5f );
		var max = min + new Vector2(sizeX * 1, sizeY * 1);
		rect[0].Set(min.x,  min.y,0);
		rect[1].Set(max.x,  min.y,0);
		rect[2].Set(max.x,  max.y,0);
		rect[3].Set(min.x,  max.y,0);
		Handles.DrawSolidRectangleWithOutline(rect, fill, outline);
	}
}
