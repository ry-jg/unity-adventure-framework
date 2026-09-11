using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    public Transform[] nodes;
    public Camera mainCam;
    public float snapSpeed = 10f;

    private int current = 0;

    void Update()
    {
        // Smoothly move and rotate the camera towards the current node
        mainCam.transform.position = Vector3.Lerp(
            mainCam.transform.position, nodes[current].position, Time.deltaTime * snapSpeed);
        mainCam.transform.rotation = Quaternion.Slerp(
            mainCam.transform.rotation, nodes[current].rotation, Time.deltaTime * snapSpeed);
    }

    public void GoToNode(int index)
    {
        if (index >= 0 && index < nodes.Length)
            current = index;
    }
}
