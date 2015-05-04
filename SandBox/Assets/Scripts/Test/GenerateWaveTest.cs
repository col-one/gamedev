using UnityEngine;
using System.Collections;

public class GenerateWaveTest : MonoBehaviour {

	public GameObject wave;

	public void generateWave()
	{
		SpawnController.Instance.Spawn(wave, transform.position, Quaternion.identity);
	}
}
