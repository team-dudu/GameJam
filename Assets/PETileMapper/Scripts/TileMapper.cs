using UnityEngine;
using System.Collections;
using System.Xml;

public class TileMapper : MonoBehaviour {
	public TileMap tilemap;
	public string xmlTileMapName;
	public string tilesetName;
	public string colliderLayerName;

	[HideInInspector]
	public Sprite Current_Sprite;


	public void Clear(){
		int retrycount = 0;
		while(this.transform.childCount > 0 && retrycount <20){//Really tries to make sure all the child gameobjects are destroyed!
			foreach(Transform child in this.transform){
				DestroyImmediate(child.gameObject);
			}
			retrycount++;
		}
	}

	public void Generate(){
		Clear();
		if(this.transform.childCount>0) return;//Risk doubling up game objects if we continue...
		XMLRead(xmlTileMapName);
		DrawTileMap(tilesetName);
	}
	void XMLRead(string xmltilemapname){
		tilemap.ClearLayers();
		TextAsset textAsset = (TextAsset) Resources.Load(xmltilemapname); 
		if(textAsset == null){
			Debug.Log("No such file in resources folder: " + xmltilemapname + ".xml");
			return;
		}
		XmlDocument xmldoc = new XmlDocument();
		xmldoc.LoadXml(textAsset.text);
		XmlNode root = xmldoc.FirstChild;
		TileMap.Layer currentlayer = null;
		foreach(XmlNode layernode in root.ChildNodes){
			int number = XmlConvert.ToInt32(layernode.Attributes.GetNamedItem("number").Value);
			string name = layernode.Attributes.GetNamedItem("name").Value;
			currentlayer = tilemap.AddLayer(name,number);
			foreach(XmlNode tilenode in layernode.ChildNodes){
				int x = XmlConvert.ToInt32(tilenode.Attributes.GetNamedItem("x").Value);
				int y = XmlConvert.ToInt32(tilenode.Attributes.GetNamedItem("y").Value);
				int tile = XmlConvert.ToInt32(tilenode.Attributes.GetNamedItem("tile").Value);
				int rot = XmlConvert.ToInt32(tilenode.Attributes.GetNamedItem("rot").Value);
				bool flipX = XmlConvert.ToBoolean(tilenode.Attributes.GetNamedItem("flipX").Value);
				if(tile != -1) currentlayer.AddTile(x,y,tile,rot,flipX);
			}
		}
	}

	GameObject FindPrefab(GameObject[] prefabs,string index){
		foreach (var p in prefabs) {
			if(p.name.StartsWith(index + "_")) return p;
		}
		return null;
	}


	void DrawTileMap(string tilesetfilename){
		Sprite[] sprites = Resources.LoadAll<Sprite>(tilesetfilename);
		GameObject[] prefabs = Resources.LoadAll<GameObject> ("Prefabs");
		foreach (var p in prefabs) {
			Debug.Log("p:" + p.name);
		}
		if(sprites.Length == 0){
			Debug.Log("No such file in resources folder: " + tilesetfilename + ".png");
			return;
		}
		for (int layerindex = 0; layerindex < tilemap.layers.Count; layerindex++) {//Iterate through each layer
			GameObject golayer = new GameObject(tilemap.layers[layerindex].name);
			golayer.transform.parent = this.transform;
			golayer.transform.localPosition = new Vector3(0,0,-layerindex);
			for (int i = 0; i < tilemap.layers[layerindex].tiles.Count; i++) {//Iterate through each tile
				TileMap.Layer.Tile tile = tilemap.layers[layerindex].tiles[i];
				if(tilemap.layers[layerindex].name == colliderLayerName){
					GameObject goCollider = (GameObject)Instantiate(Resources.Load<GameObject>("boxcollider2D"));
					goCollider.transform.parent = golayer.transform;
					goCollider.transform.localPosition = new Vector3(tile.x,-tile.y,0);
				}else{

					//GameObject prefab = Resources.Load<GameObject>(tile.index.ToString());
					GameObject prefab = FindPrefab(prefabs,tile.index.ToString());

					if(prefab == null) prefab = Resources.Load<GameObject>("sprite_basic");


					GameObject go = (GameObject)GameObject.Instantiate(prefab);
					go.transform.parent = golayer.transform;

						

					SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
					spriteRenderer.sprite = sprites[tile.index];
					go.transform.localPosition = new Vector3(tile.x,-tile.y,0);
					go.transform.localRotation = Quaternion.Euler(new Vector3(0,0,-90 * tile.rot));
					if(tile.flipX) go.transform.localScale = new Vector3(-1,1,1);
				}
			}
		}
		Debug.Log("Success!");
	}


}
