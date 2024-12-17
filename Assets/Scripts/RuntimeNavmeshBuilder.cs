using UnityEngine.AI;
using Unity.AI.Navigation;
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class RuntimeNavmeshBuilder : MonoBehaviour {

	private NavMeshSurface navmeshSurface;

	void Start() {
		navmeshSurface = GetComponent<NavMeshSurface>();
		MRUK.Instance.RegisterSceneLoadedCallback(BuildNavMesh);
	}

	public void BuildNavMesh()
	{
		navmeshSurface.BuildNavMesh();
	}
}