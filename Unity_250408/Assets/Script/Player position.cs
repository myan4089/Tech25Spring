using UnityEngine;

public class Playerposition : MonoBehaviour
{

    // We need the position of the user as a Transform
    public Transform playerTransform;

// move the height position below the pavillion
public float y = 0;

    // we need something to hold the position of the circle

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      // create a vector to hold the position of the player
Vector3 pos = new Vector3(playerTransform.position.x, y, playerTransform.position.z);
      // apply the position of the player to sphere 
      transform.position = pos;

    }
}
