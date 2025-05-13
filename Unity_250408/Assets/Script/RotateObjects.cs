using UnityEngine;

public class RotateObjects : MonoBehaviour
{

    public float rotationSpeed = 2f; 


    // Update is called once per frame
    void Update()
    {
      transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);  
    }
}
