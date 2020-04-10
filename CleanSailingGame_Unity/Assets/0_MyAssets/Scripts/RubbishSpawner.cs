
//Class description: spawns rubbish objects randomly across NavMesh

using UnityEngine;
using UnityEngine.AI;

public class RubbishSpawner : MonoBehaviour
{
    //public float radius = 100;

    public Vector3 center;
    public GameObject[] rubbishPrefabList;
    public BoatPropierties boatPropierties;

    #region Singleton pattern

    public static RubbishSpawner instance = null;

    // Game Instance Singleton
    public static RubbishSpawner Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    #endregion

    //void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.Space))
    //    {
    //        Debug.Log("Trying to instantiate");
    //        Vector3 _point = GetRandomPoint();
    //        InstantiateObjectAt(_point);
    //    }
    //}

    //Gets a valid random position inside NavMesh
    public Vector3 GetRandomPoint(float _maxRadius, float _scaleFactor)
    {
        bool _validPoint = false;
        Vector3 _randomPoint = Vector3.zero;

        //Loop until retrieved positionis valid
        do
        {
            _randomPoint = center + Random.insideUnitSphere * _maxRadius * _scaleFactor;
            _randomPoint.y = 0;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(_randomPoint, out hit, 1f, NavMesh.AllAreas))
            {
                _validPoint = true;
                _randomPoint = hit.position;
                _randomPoint -= center;
            }
        }
        while (!_validPoint);

        _randomPoint.y = 0;
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
        pawnInstance.GetComponentInChildren<RubbishBehaviour>().BoatPropierties = boatPropierties;
    }

}
