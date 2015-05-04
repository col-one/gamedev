using UnityEngine;
using System.Collections;

public class CrossWaveManager : MonoBehaviour {

	public GameObject enemy;
	public int enemyNumber;
	public GameObject path;
	public int speed = 1;
	public float delayStep;
	public bool lookPath;

	private Transform parentTransform;
	private Transform tempEnemy;
	private Transform pathTransform;
	private SplineWalker walker;

	void Start()
	{
		parentTransform = transform;
		pathTransform = SpawnController.Instance.Spawn(path, Vector3.zero, Quaternion.identity);
		pathTransform.parent = parentTransform;
		for(int i=1; i <= enemyNumber; i++)
		{
			tempEnemy = SpawnController.Instance.Spawn(enemy, parentTransform.position, Quaternion.identity);
			tempEnemy.parent = parentTransform;
			walker = tempEnemy.gameObject.AddComponent<SplineWalker>();
			walker.duration = speed;
			walker.spline = pathTransform.gameObject.GetComponent<BezierSpline>();
			walker.delay = i * delayStep ;
			walker.lookForward = lookPath;
		}
	}
}
