using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] public Image _healthbarSprite;
    [SerializeField] public Image _backgroundSprite;
    [SerializeField] public TextMeshProUGUI healthText;
    public EnnemiValeurs ScriptEnnemiValeurs;
    private Camera _cam;
    private Enemy enemyScript;

    void Start()
    {
        _cam = Camera.main;
		healthText.text = $"{Mathf.CeilToInt(ScriptEnnemiValeurs.VieDepart)} HP";
        float newScaleX = Mathf.Lerp(1, 3, (ScriptEnnemiValeurs.VieDepart - 100) / (100000 - 100));
        RectTransform rectTransform = _backgroundSprite.rectTransform;
        rectTransform.localScale = new Vector3(newScaleX, rectTransform.localScale.y, rectTransform.localScale.z);
    }
    private void Update() {        
        transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
    }

}
