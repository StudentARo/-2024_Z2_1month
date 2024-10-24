using System;
using UnityEngine;
using Player;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private PlayerConfig _playerConfig;
    public int _currentScore;   //As public for testing purposes

    private void Start()
    {
        _currentScore = _playerConfig.BaseScore;
    }

    public void AddScore(int pointsAmount)
    {
        if (pointsAmount < 0)
        {
            return;
        }

        _currentScore += pointsAmount;
    }
}
