using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TileMap {
	public List<Layer> layers = new List<Layer>();

	[System.Serializable]
	public class Layer{
		public string name;
		public int number;
		public List<Tile> tiles = new List<Tile>();
	
		[System.Serializable]
		public class Tile{
			public int x;
			public int y;
			public int index;
			public int rot;
			public bool flipX;
		}
		public void AddTile(int tilex,int tiley,int tileindex,int tilerot,bool tileflipX){
			tiles.Add (new Tile(){x=tilex,y=tiley,index=tileindex,rot=tilerot,flipX=tileflipX});
		}
	}

	public Layer AddLayer(string layername,int layernumber){
		Layer layer = new Layer(){name=layername,number=layernumber};
		layers.Add( layer);
		return layer;
	}
	public void ClearLayers(){
		layers.Clear();
	}
}
