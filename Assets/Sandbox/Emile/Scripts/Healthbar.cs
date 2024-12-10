using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    //[SerializeField] private Image _healthbarSprite;
    [SerializeField] public Image _backgroundSprite;
    [SerializeField] public TextMeshProUGUI healthText;
    public PointDeVie ScriptVie;
    private Camera _cam;
    private Enemy enemyScript;

    void Start()
    {
        _cam = Camera.main;
		healthText.text = $"{Mathf.CeilToInt(ScriptVie.VieDepart)} HP";
        float newScaleX = Mathf.Lerp(1, 3, (ScriptVie.VieDepart - 100) / (100000 - 100));
        RectTransform rectTransform = _backgroundSprite.rectTransform;
        rectTransform.localScale = new Vector3(newScaleX, rectTransform.localScale.y, rectTransform.localScale.z);
        //UpdateHealthText();
        //SetHealthBarWidth();
    }
    private void Update() {        
        transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
    }

    /*private void SetHealthBarWidth(float vie)
    {
    }*/

    /*public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        _target = currentHealth / maxHealth;
    }*/

    /*public void Hit(float VieRetirer)
    {
        CurrentHealth -= VieRetirer;
        CurrentHealth = Mathf.Max(CurrentHealth, 0);
        UpdateHealthBar(ScriptVie.VieDepart, CurrentHealth);
        UpdateHealthText();
        SetHealthBarWidth(CurrentHealth);
    }*/

}
