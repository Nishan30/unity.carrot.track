using UnityEngine;

public class TruckEyes : MonoBehaviour
{
    public Transform truck;
    public Transform[] eyes;

    private Vector3 eyesPosition;

    private void Start()
    {
        eyesPosition = Vector3.zero;
    }

    private void Update()
    {
        // Actualizează poziția ochilor camionului pe axa dorită
        foreach (Transform eye in eyes)
        {
            Vector3 eyePosition = eyesPosition;
            eyePosition.x += eye.localPosition.x;
            eye.localPosition = eyePosition;
        }
    }
}
