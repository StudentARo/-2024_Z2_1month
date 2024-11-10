using System;
using UnityEngine;
using Player;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private PlayerConfig _playerConfig;
    LevelSwap lswap;
    public static event Action<int> OnScoreUpdated;
    public int _currentScore;   //As public for testing purposes
    internal readonly int coins;
    int _saveScore;

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
        _saveScore = _currentScore;
        OnScoreUpdated?.Invoke(_currentScore);
    }
    public int ReloadScore()
    {
         _currentScore = _saveScore;
        return _currentScore;
    }
}
