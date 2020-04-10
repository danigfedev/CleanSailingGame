
//Class description: spawns rubbish objects randomly across NavMesh

using UnityEngine;
using UnityEngine.AI;

public class RubbishSpawner : MonoBehaviour
{
    public float radius = 100;
    public Vector3 center;
    public GameObject[] rubbishPrefabList;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Vector3 _point = GetRandomPoint();
            InstantiateObjectAt(_point);                
        }
    }

    //Gets a valid random position inside NavMesh
    public Vector3 GetRandomPoint()
    {
        bool _validPoint = false;
        Vector3 _randomPoint = Vector3.zero;

        //Loop until retrieved positionis valid
        do
        {
            _randomPoint = center + Random.insideUnitSphere * radius;
            _randomPoint.y = 0;
            NavMeshHit hit;
            if(NavMesh.SamplePosition(_randomPoint, out hit, 0.5f, NavMesh.AllAreas))
            {
                _validPoint = true;
                _randomPoint = hit.position;
                _randomPoint -= center;
            }
        }
        while (!_validPoint);
        
        return _randomPoint;
    }

    //Instantiates random rubbish object from list (organic, plastic, glass) at given position
    public void InstantiateObjectAt(Vector3 _pos) 
    {
        int _spawnIdx = Random.Range(0, rubbishPrefabList.Length);
        GameObject pawnInstance = Instantiate(rubbishPrefabList[_spawnIdx]);
        //GameObject pawnInstance = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere));
        pawnInstance.transform.position = _pos;
    }

}
