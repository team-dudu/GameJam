PETileMapper for Unity v1.21

PETileMapper does the following:

1) Imports tilemaps made in Pyxel Edit into Unity
2) Includes a Tile Map Editor Window so you can easily create Tile Maps within Unity
3) That's it, nice and simple!


Here's the process (for importing from Pyxel Edit):


Create a Unity Project 
Create a Resources folder (this is where you will export the Pyxel Edit files into)

Create your tile map in Pyxel Edit
File->Export->Export tileset : Export directly into your Resources folder
File->Export->Export tilemap : Export directly into your Resources folder, choose xml format (must be different from above name) 

Select your tileset in Unity, set to Sprite,Multiple, 8,16 or 32 in Pixels To Units,Filter Mode Point
Click Sprite Editor, Slice, Grid (Type), 8by8,16by16 or 32by32(Pixel Size),Slice,Apply
Drag the MyTileMap prefab into your scene
Enter the tilemap name and the tileset name
Click Generate 

Done!

Here's the process (for creating Tile Maps from within Unity)

Create a Unity Project
Create a Resources folder
Copy a tileset into the Resources folder
Set texture type to Sprite, Sprite Mode to multiple,Sprite Editor->Grid->Slice,Set Pixels to Units to pixel size of tile,Filter Mode Point
Go to Window->Tile Map Editor
Enter the tileset name, click Edit TileMap

Useful keys:
Hold Ctrl and left click to delete a tile (D and left click on a Mac)
Hit Alt to rotate a tile (R on a Mac)


Additional Notes(for importing from Pyxel Edit):

You can automatically create colliders for your map by creating an extra layer in Pyxel Edit.
Any tiles specified on this layer will be converted to colliders.
Just specifiy the name of the layer in the Collision Layer Name field.

Additional Notes
If you see seams between tiles, try slightly lowering Pixels to Units to force the tiles to slightly overlap


All tilesets used in this asset are from http://opengameart.org/

v1.21
Holding D while left clicking will delete a tile (Left Control doesn't work on Mac for some reason)
R will now also rotate a tile

v1.2
Added the Tile Map Editor Window

v1.1
Added tile->prefab: Just put the prefab in Resources\Prefabs and start the name with the index of the tile you want to swap prefab with plus an underscore ie 17_Crate (after the underscore
you can type whatever you want, usually a sensible identifying name!)

v1.01
Added missing process step.
