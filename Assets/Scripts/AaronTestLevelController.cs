using UnityEngine;
using System.Collections;

public class AaronTestLevelController : BaseLevelController {

	// Use this for initialization
	void Start () {
        UnitSpawner.SpawnUnit(UnitReferences.EnemyCruiser1, new Vector3(0, 50, 100));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
