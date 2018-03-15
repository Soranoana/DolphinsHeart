using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrainVector3 : MonoBehaviour {

    float height;
    Vector3 here;

	void Start () {
        here = transform.position;
        height = Terrain.activeTerrain.terrainData.GetInterpolatedHeight(
        here.x / Terrain.activeTerrain.terrainData.size.x,
        here.z / Terrain.activeTerrain.terrainData.size.z);
    }
	
	void Update () {
        here = transform.position;
        height = Terrain.activeTerrain.terrainData.GetInterpolatedHeight(
        here.x / Terrain.activeTerrain.terrainData.size.x,
        here.z / Terrain.activeTerrain.terrainData.size.z);
    }
}
