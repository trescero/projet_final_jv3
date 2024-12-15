using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField] private TMP_Text _textMoney;
    [SerializeField] private TMP_Text _textPoints;
    [SerializeField] private Player_ScriptableObject _player;

    private void Update()
    {
        _textMoney.text = _player.money.ToString();
        _textPoints.text = _player.points.ToString();
    }
    
}
