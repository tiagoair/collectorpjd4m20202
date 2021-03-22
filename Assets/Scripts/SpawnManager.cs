using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region Serializable Private Fields

    [SerializeField] private GameObject objectToSpawn;

    [SerializeField] private float timeToSpawn;
    

    #endregion

    #region Private Fields

    private float _currentTime;

    #endregion
    
    
    #region MonoBehaviour Callbacks

    // Update is called once per frame
    void Update()
    {
        if (_currentTime < timeToSpawn)
        {
            _currentTime += Time.deltaTime;
        }
        else
        {
            SpawnObject();
            _currentTime = 0f;
        }
    }

    #endregion

    #region Private Fields

    private void SpawnObject()
    {
        Vector3 cameraPosition = Camera.main.transform.position;

        Vector3 leftBottomLimit = new Vector3(cameraPosition.x - 12.45f, cameraPosition.y - 7f, 0f);

        Vector3 rightUpperLimit = new Vector3(cameraPosition.x + 12.45f, cameraPosition.y + 7f, 0f);

        Vector3 objectPosition = new Vector3(Random.Range(leftBottomLimit.x, rightUpperLimit.x),
            Random.Range(leftBottomLimit.y, rightUpperLimit.y), 0f);

        Instantiate(objectToSpawn, objectPosition, Quaternion.identity);

    }

    #endregion
}
