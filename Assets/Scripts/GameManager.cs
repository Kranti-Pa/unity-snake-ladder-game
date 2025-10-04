using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    public int numberOfPlayers = 2;
    public int boardSize = 100;
    
    [Header("UI References")]
    public Button rollDiceButton;
    public Text diceResultText;
    public Text currentPlayerText;
    public Text gameStatusText;
    public GameObject winPanel;
    public Text winnerText;
    
    [Header("Game Objects")]
    public GameObject[] playerPieces;
    public Transform[] boardPositions;
    public DiceController diceController;
    
    private int currentPlayer = 0;
    private int[] playerPositions;
    private bool gameEnded = false;
    
    // Snake and Ladder positions
    private Dictionary<int, int> snakes = new Dictionary<int, int>()
    {
        {99, 78}, {95, 75}, {92, 88}, {87, 24}, {64, 60},
        {62, 19}, {56, 53}, {49, 11}, {47, 26}, {16, 6}
    };
    
    private Dictionary<int, int> ladders = new Dictionary<int, int>()
    {
        {1, 38}, {4, 14}, {9, 31}, {21, 42}, {28, 84},
        {36, 44}, {51, 67}, {71, 91}, {80, 100}
    };
    
    void Start()
    {
        InitializeGame();
    }
    
    void InitializeGame()
    {
        playerPositions = new int[numberOfPlayers];
        
        // Initialize all players at position 0
        for (int i = 0; i < numberOfPlayers; i++)
        {
            playerPositions[i] = 0;
            playerPieces[i].transform.position = boardPositions[0].position;
        }
        
        rollDiceButton.onClick.AddListener(RollDice);
        UpdateUI();
        winPanel.SetActive(false);
    }
    
    public void RollDice()
    {
        if (gameEnded) return;
        
        rollDiceButton.interactable = false;
        int diceValue = diceController.RollDice();
        diceResultText.text = "Dice: " + diceValue;
        
        StartCoroutine(MovePlayer(diceValue));
    }
    
    System.Collections.IEnumerator MovePlayer(int steps)
    {
        int newPosition = playerPositions[currentPlayer] + steps;
        
        // Check if player exceeds board size
        if (newPosition > boardSize)
        {
            newPosition = boardSize - (newPosition - boardSize);
        }
        
        // Animate movement
        yield return StartCoroutine(AnimatePlayerMovement(currentPlayer, newPosition));
        
        playerPositions[currentPlayer] = newPosition;
        
        // Check for snakes and ladders
        if (snakes.ContainsKey(newPosition))
        {
            gameStatusText.text = "Snake bite! Going down...";
            yield return new WaitForSeconds(1f);
            newPosition = snakes[newPosition];
            yield return StartCoroutine(AnimatePlayerMovement(currentPlayer, newPosition));
            playerPositions[currentPlayer] = newPosition;
        }
        else if (ladders.ContainsKey(newPosition))
        {
            gameStatusText.text = "Ladder climb! Going up...";
            yield return new WaitForSeconds(1f);
            newPosition = ladders[newPosition];
            yield return StartCoroutine(AnimatePlayerMovement(currentPlayer, newPosition));
            playerPositions[currentPlayer] = newPosition;
        }
        
        // Check for win condition
        if (playerPositions[currentPlayer] >= boardSize)
        {
            EndGame();
            yield break;
        }
        
        // Switch to next player
        currentPlayer = (currentPlayer + 1) % numberOfPlayers;
        UpdateUI();
        rollDiceButton.interactable = true;
    }
    
    System.Collections.IEnumerator AnimatePlayerMovement(int playerIndex, int targetPosition)
    {
        Vector3 startPos = playerPieces[playerIndex].transform.position;
        Vector3 targetPos = boardPositions[targetPosition].position;
        
        float duration = 0.5f;
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            playerPieces[playerIndex].transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }
        
        playerPieces[playerIndex].transform.position = targetPos;
    }
    
    void UpdateUI()
    {
        currentPlayerText.text = "Player " + (currentPlayer + 1) + "'s Turn";
        gameStatusText.text = "Roll the dice!";
    }
    
    void EndGame()
    {
        gameEnded = true;
        winPanel.SetActive(true);
        winnerText.text = "Player " + (currentPlayer + 1) + " Wins!";
        rollDiceButton.interactable = false;
    }
    
    public void RestartGame()
    {
        gameEnded = false;
        currentPlayer = 0;
        InitializeGame();
    }
}