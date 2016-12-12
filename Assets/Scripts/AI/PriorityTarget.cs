using UnityEngine;
using System.Collections.Generic;

public class PriorityTarget : MonoBehaviour {

    public float priorityRatio = 0.0f;

    List<GameObject> enemyPool = new List<GameObject>();
    GameObject enemyPriority = null;
	
	void Update () {
        int numEnemiesOnPriority = 0;

        if (enemyPriority) {
            foreach (GameObject enemy in enemyPool) {
                if ((enemy.GetComponent("AIController") as AIController) != null) {
                    AIController controller = enemy.GetComponent("AIController") as AIController;
                    if (controller.getTarget() == enemyPriority)
                        numEnemiesOnPriority++;
                }
            }

            Debug.Log((float)numEnemiesOnPriority / (float)enemyPool.Count);

            if (((float)numEnemiesOnPriority / (float)enemyPool.Count) < priorityRatio) {
                int numEnemiesNeeded = (enemyPool.Count * (int)priorityRatio) - numEnemiesOnPriority;
                foreach (GameObject enemy in enemyPool) {
                    if (((enemy.GetComponent("AIController") as AIController) != null) && (numEnemiesNeeded > 0)) {
                        AIController controller = enemy.GetComponent("AIController") as AIController;
                        if (controller.setTarget(enemyPriority))
                            numEnemiesNeeded--;
                    }
                }
            }
        }
    }

    public void addToEnemyPool(List<GameObject> list) {
        foreach (GameObject enemy in list) {
            enemyPool.Add(enemy);
        }
    }

    public void setEnemyPriority(GameObject obj) {
        enemyPriority = obj;
    }
}
