using UnityEngine;

public class CanvasFollowCamera : MonoBehaviour
{
    // R�f�rence � la cam�ra principale
    public Camera mainCamera;

    // Distance de l'UI par rapport � la cam�ra
    public float distanceFromCamera = 2f;

    // Angle d'orientation de l'UI par rapport � la cam�ra (facultatif)
    public Vector3 rotationOffset = Vector3.zero;

    // ^Distance de l'UI par rapport � la cam�ra (facultatif)
    public Vector3 postionOffset = Vector3.zero;


    void Update()
    {
        // Si la cam�ra principale n'est pas assign�e, on r�cup�re la cam�ra principale par d�faut
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // Placer le canvas devant la cam�ra en ajustant la distance
        if (mainCamera != null)
        {
            Vector3 directionToFace = mainCamera.transform.forward;
            Vector3 newPosition = mainCamera.transform.position + directionToFace + postionOffset;

            // Positionner le Canvas devant la cam�ra
            transform.position = newPosition;

            // Optionnel : Ajuster la rotation pour toujours faire face � la cam�ra
            transform.rotation = Quaternion.LookRotation(directionToFace) * Quaternion.Euler(rotationOffset);
        }
    }
}
