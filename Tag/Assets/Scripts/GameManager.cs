using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _player1;

    [SerializeField]
    private GameObject _player2;

    [SerializeField]
    private GameObject _winTextBackground;

    public UnityEvent OnGameWin;

    private Rigidbody _player1Rigidbody;
    private Rigidbody _player2Rigidbody;
    private TimerSystem _player1Timer;
    private TimerSystem _player2Timer;
    private TagSystem _player1TagSystem;
    private TagSystem _player2TagSystem;
    private PlayerController _player1Controller;
    private PlayerController _player2Controller;

    private bool _gameWon = false;
    private void Start()
    {
        if (_player1)
        {
            if (!_player1.TryGetComponent(out _player1Timer))
                Debug.LogError("GameManager: Could not get Player 1 Timer");
            if (!_player1.TryGetComponent(out _player1TagSystem))
                Debug.LogError("GameManager: Could not get Player 1 Tag System");
            if (!_player1.TryGetComponent(out _player1Controller))
                Debug.LogError("GameManager: Could not get Player 1 Controller");
            if (!_player1.TryGetComponent(out _player1Rigidbody))
                Debug.LogError("GameManager: Could not get Player 1 RigidBody");
        }
        else
            Debug.LogError("GameManager: Player1 not assigned!");

        if (_player2)
        {
            if (!_player2.TryGetComponent(out _player2Timer))
                Debug.LogError("GameManager: Could not get Player 2 Timer");
            if (!_player2.TryGetComponent(out _player2TagSystem))
                Debug.LogError("GameManager: Could not get Player 2 Tag System");
            if (!_player2.TryGetComponent(out _player2Controller))
                Debug.LogError("GameManager: Could not get Player 2 Controller");
            if (!_player2.TryGetComponent(out _player2Rigidbody))
                Debug.LogError("GameManager: Could not get Player 2 RigidBody");
        }
        else
            Debug.LogError("GameManager: Player2 not assigned!");

        if (!_winTextBackground)
            Debug.LogWarning("GameManager: Win Text Background not assigned!");
    }
    private void Update()
    {
        if (!(_player1Timer && _player2Timer))
            return;

        if (_gameWon)
            return;
        if (_player1Timer.TimerRemaining <= 0)
            Win("Player 1 Wins!");
        else if (_player2Timer.TimerRemaining <= 0)
            Win("Player 2 Wins!");

    }

    private void Win(string winText)
    {
        // Enable Win Screen UI and set test to winText
        if(_winTextBackground)
        {
            _winTextBackground.SetActive(true);
            TextMeshProUGUI text = _winTextBackground.GetComponentInChildren<TextMeshProUGUI>(true);
            if(text)
            {
                text.text = winText;
            }
        }
        // Turn off player controllers and tag system and timer
        if (_player1Timer)
            _player1Timer.enabled = false;
        if (_player1TagSystem)
            _player1TagSystem.enabled = false;
        if (_player1Controller)
            _player1Controller.enabled = false;
        if (_player1Rigidbody)
            _player1Rigidbody.isKinematic = true;

        if (_player2Timer)
            _player2Timer.enabled = false;
        if (_player2TagSystem)
            _player2TagSystem.enabled = false;
        if (_player2Controller)
            _player2Controller.enabled = false;
        if (_player2Rigidbody)
            _player2Rigidbody.isKinematic = true;

        _gameWon = true;
        OnGameWin.Invoke();
    }

    public void ResetGame()
    {
        //Reload the Active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
