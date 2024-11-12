using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTracker : MonoBehaviour
{
    public GameObject prefab;
    public List<GameObject> spawnPositions;
    public GameObject target;
    public float speed = 1f;
    public KeyCode keyToPress;


    private void Update()
    {
        if(Input.GetKeyDown(keyToPress))
        {
            GameObject rocket = Instantiate(prefab, spawnPositions[Random.Range(0, 4)].transform.position, prefab.transform.rotation);
            rocket.transform.LookAt(target.transform);
            StartCoroutine(SendHoming(rocket));
        }
    }

    public IEnumerator SendHoming(GameObject rocket)
    {
        while (Vector3.Distance(target.transform.position, rocket.transform.position)>0.3f)
        {
            rocket.transform.position += (target.transform.position - rocket.transform.position).normalized * speed * Time.deltaTime;
            rocket.transform.LookAt(target.transform);
            yield return null;
        }
        Destroy(rocket);
    }
}
