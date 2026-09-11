using UnityEngine;
using System.Collections;
public class DoorAnimator : MonoBehaviour
{
    public float openAngle = 90f;
    public float duration = 1.0f;
    public Vector3 rotationAxis = Vector3.up;

    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = transform.localRotation;
        openRotation = closedRotation * Quaternion.AngleAxis(openAngle, rotationAxis);
    }

    public void Open()
    {
        if (!isOpen)
            StartCoroutine(AnimateRotation(closedRotation, openRotation));
    }

    public void Close()
    {
        if (isOpen)
            StartCoroutine(AnimateRotation(openRotation, closedRotation));
    }

    IEnumerator AnimateRotation(Quaternion from, Quaternion to)
    {
        isOpen = !isOpen;
        float elapsed = 0f;

        // Slerp between the two rotations over the specified duration
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.localRotation = Quaternion.Slerp(from, to, elapsed / duration);
            yield return null;
        }
        transform.localRotation = to;
    }
}