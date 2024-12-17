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

    private void Update()
    {
        if (uiPointage != null && uiPointage.activeSelf)
        {
            UpdateUI();
        }
    }

    private void Start()
    {
        if (uiPointage != null && uiPointage.activeSelf)
        {
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        _textMoney.text = "$: " + _player.money.ToString();
        _textPoints.text = "Pointage: " + _player.points.ToString();
    }

    public void ResetPlayerValues()
    {
        _player.money = 0;
        _player.points = 0;

        UpdateUI();
    }
}
