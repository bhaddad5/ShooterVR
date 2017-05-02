using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMP_EnemySpawner : MonoBehaviour
{
	public GameObject enemyPrefab;
	public int NumToMaintain;

	private List<GameObject> enemies = new List<GameObject>();
	
	// Update is called once per frame
	void Update () {
		for (int i = enemies.Count-1; i >=0 ; i--)
		{
			if (enemies[i] == null)
				enemies.RemoveAt(i);
		}

		if (enemies.Count < NumToMaintain)
			enemies.Add(Instantiate(enemyPrefab, transform.position, Quaternion.identity));
	}
}
