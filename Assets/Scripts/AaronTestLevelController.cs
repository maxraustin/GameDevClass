using UnityEngine;
using System.Collections;

public class AaronTestLevelController : BaseLevelController {

	// Use this for initialization
	void Start () {
        UnitSpawner.SpawnUnit(UnitReferences.EnemyCruiserLight1, new Vector3(-100, 50, 100));
        UnitSpawner.SpawnUnit(UnitReferences.EnemyCruiserLight1, new Vector3(0, 50, 100));
        UnitSpawner.SpawnUnit(UnitReferences.EnemyCruiserLight1, new Vector3(100, 50, 100));
        UnitSpawner.SpawnUnit(UnitReferences.AlliedCruiserHeavy1, new Vector3(0, 50, -200), Quaternion.Euler(0,0,0));
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
