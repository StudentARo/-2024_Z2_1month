using System;
using UnityEngine;
using Player;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private PlayerConfig _playerConfig;
    public static event Action<int> OnScoreUpdated;
    public int _currentScore;   //As public for testing purposes
    internal readonly int coins;

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
        OnScoreUpdated?.Invoke(_currentScore);
    }
}
