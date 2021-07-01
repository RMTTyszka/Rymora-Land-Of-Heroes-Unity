using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[CreateAssetMenu(fileName = "floor", menuName = "TIles/floor")]
public class Floor : Tile {

	public bool isWalkable = true;
    public float moveMultiplier;
}
