using UnityEngine;

public class CameraTargetCycler : MonoBehaviour
{
    public Transform[] targets;
    public float moveSpeed = 5f;
    public float rotateSpeed = 5f;

    private int currentIndex = 0;

    void Update()
    {
        // Switch to next target with spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentIndex = (currentIndex + 1) % targets.Length;
        }

        if (targets.Length == 0) return;

        // Move camera to target position
        transform.position = Vector3.Lerp(transform.position, targets[currentIndex].position, Time.deltaTime * moveSpeed);

        // Rotate camera to match target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targets[currentIndex].rotation, Time.deltaTime * rotateSpeed);
    }
}