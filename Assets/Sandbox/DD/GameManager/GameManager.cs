using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _textMoney;
    [SerializeField] private TMP_Text _textPoints;
    [SerializeField] private Player_ScriptableObject _player;
    [SerializeField] private GameObject uiPointage;
    [SerializeField] private GameObject scriptEnemies;

    private void Update()
    {
        if (uiPointage != null && uiPointage.activeSelf)
        {
            UpdateUI();
        }

        if(_player.hasPlacedFirstTower == true)
        {
            scriptEnemies.SetActive(true);
        }
    }

    private void Start()
    {
        if (uiPointage != null && uiPointage.activeSelf)
        {
            UpdateUI();
        }

        _player.money = 300;
        _player.hasPlacedFirstTower = false;
    }

    private void UpdateUI()
    {
        _textMoney.text = "$: " + _player.money.ToString();
        _textPoints.text = "Pointage: " + _player.points.ToString();
    }

    public void ResetPlayerValues()
    {
        _player.money = 300;
        _player.points = 0;

        UpdateUI();
    }
}
