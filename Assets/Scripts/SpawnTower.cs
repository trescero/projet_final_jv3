using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnTower : MonoBehaviour
{
    [Header("Raycast Settings")]
    [Tooltip("Point de départ du rayon pour détecter les objets.")]
    [SerializeField] private Transform rayStartPoint;

    [Tooltip("Longueur maximale du rayon.")]
    [SerializeField] private float rayLength = 8.0f;

    [Tooltip("Tag des objets valides pour placer une tour.")]
    [SerializeField] private string validTag = "Anchor";

    [Header("Gizmo Display")]
    [Tooltip("Objet Gizmo affiché à l'emplacement de l'objet détecté.")]
    [SerializeField] private GameObject gizmoDisplay;

    [Tooltip("Texte affiché sur le Gizmo.")]
    [SerializeField] private TextMeshPro gizmoLabelText;

    [Tooltip("Détermine si le texte du Gizmo est visible.")]
    [SerializeField] private bool showGizmoLabelText;

    [Header("Tower Spawn")]
    [Tooltip("Préfabriqué de la tour à instancier.")]
    [SerializeField] private GameObject selectedTowerPrefab;

    [Tooltip("Bouton utilisé pour faire apparaître une tour.")]
    [SerializeField] private OVRInput.Button spawnButton;

    [Header("Informations Player")]
    [SerializeField] private Player_ScriptableObject player;
    [SerializeField] private int towerCost = 500;

    [Header("Audios")]
    [SerializeField] private AudioSource audioPlacement;

    [SerializeField] private AudioSource audioWrong;


    // Variables privées
    private Vector3 hitPoint; // Point d'impact du rayon
    private bool canPlaceTower = false;


    private void Start()
    {
        // Configurer la visibilité du texte du Gizmo
        gizmoLabelText.enabled = showGizmoLabelText;
    }

    private void Update()
    {
        // Gérer le raycast pour détecter des objets avec un tag valide
        ProcessRaycast();

        if (OVRInput.GetDown(spawnButton, OVRInput.Controller.RTouch))
        {
            if (selectedTowerPrefab == null)
            {
                Debug.LogWarning("Pas de tour selectionner");
                return;
            }

            if (player.money >= towerCost)
            {
                canPlaceTower = true;
                Debug.Log("Peut placer une tour");
                
            }
            else
            {
                Debug.Log("Pas assez d'argent pour placer une tour");
            }
        }

    }

    /// <summary>
    /// Effectue un raycast à partir du point de départ pour détecter les objets avec le tag spécifié.
    /// </summary>
    private void ProcessRaycast()
    {
        Ray ray = new Ray(rayStartPoint.position, rayStartPoint.forward);

        // Lancer le rayon et vérifier les collisions avec des objets
        if (Physics.Raycast(ray, out RaycastHit hitInfo, rayLength))
        {
            if (hitInfo.collider.CompareTag(validTag))
            {
                // Si un objet avec un tag valide est détecté, gérer l'affichage du Gizmo
                HandleGizmoDisplay(hitInfo);

                // Vérifier si l'angle du Gizmo est correct
                if (IsGizmoPointingSkyward())
                {
                    HandleControllerAction(OVRInput.Controller.RTouch);
                }
            }
            else
            {
                // Désactiver le Gizmo si l'objet n'a pas le tag valide
                gizmoDisplay.SetActive(false);
            }
        }
        else
        {
            // Désactiver le Gizmo si aucun objet n'est détecté
            gizmoDisplay.SetActive(false);
        }
    }

    /// <summary>
    /// Active et configure l'affichage du Gizmo en fonction de l'objet détecté.
    /// </summary>
    /// <param name="hitInfo">Informations sur l'impact du rayon.</param>
    private void HandleGizmoDisplay(RaycastHit hitInfo)
    {
        gizmoDisplay.SetActive(true);

        // Positionner et orienter le Gizmo
        hitPoint = hitInfo.point;
        gizmoDisplay.transform.position = hitPoint;
        gizmoDisplay.transform.rotation = Quaternion.LookRotation(-hitInfo.normal);

        // Mettre à jour le texte affiché
        gizmoLabelText.text = $"Tag: {hitInfo.collider.tag}";
    }

    /// <summary>
    /// Vérifie si le Gizmo pointe dans une direction "ciel".
    /// </summary>
    /// <returns>True si l'angle est dans les limites acceptables.</returns>
    private bool IsGizmoPointingSkyward()
    {
        float rotationXGizmo = gizmoDisplay.transform.rotation.eulerAngles.x;
        const float minAngle = 89.0f;
        const float maxAngle = 91.0f;

        // Vérifier si l'angle est dans la plage acceptée
        if (rotationXGizmo >= minAngle && rotationXGizmo <= maxAngle)
        {
            Debug.LogWarning("L'objet pointe vers le ciel !");
            return true;
        }
        else
        {
            Debug.LogWarning("L'objet ne pointe pas vers le ciel.");
            return false;
        }
    }

    //Verifie que le joueur peut placer une tour lorsque le bouton est pressed et sassure qu le joueur peut juste placer une tourelle a la fois
    private void HandleControllerAction(OVRInput.Controller controller)
    {
        if (OVRInput.GetDown(spawnButton, controller) && canPlaceTower)
        {
            Instantiate(selectedTowerPrefab, hitPoint, Quaternion.identity);
            player.money -= towerCost;
            selectedTowerPrefab = null;
            canPlaceTower = false;
            ToggleSocles(false);
            Debug.Log("Tour placee avec succes. choisir autre tour");

            player.hasPlacedFirstTower = true;

            audioPlacement.Play();
        }

        else if(OVRInput.GetDown(spawnButton, controller) && canPlaceTower == false)
        {
            audioWrong.Play();
        }
    }

    // Change prefab tower pour celui du bouton
    public void SetTowerPrefab(GameObject newTowerPrefab)
    {
        selectedTowerPrefab = newTowerPrefab;

        Turret turretComponent = newTowerPrefab.GetComponent<Turret>();

         
        Debug.Log($"Tour sélectionnée : {newTowerPrefab.name}");
        towerCost = turretComponent.tourrellesValues.cost;

        ToggleSocles(true);

    }

    public void ToggleSocles(bool state)
{
    GameObject[] socles = GameObject.FindGameObjectsWithTag("TowerInHand");

    Debug.Log($"transform les socles en {state}. trouver {socles.Length} socles.");
    
    foreach (GameObject socle in socles)
    {
        MeshRenderer renderer = socle.GetComponent<MeshRenderer>();

        if(renderer != null)
        {
            renderer.enabled = state;
        }
    }


}

}
