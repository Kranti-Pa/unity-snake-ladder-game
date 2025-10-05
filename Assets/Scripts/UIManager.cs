using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Main Menu UI")]
    public GameObject mainMenuPanel;
    public Button playButton;
    public Button settingsButton;
    public Button quitButton;
    public TMP_Dropdown playerCountDropdown;
    
    [Header("Game UI")]
    public GameObject gameUIPanel;
    public Button pauseButton;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    
    [Header("Pause Menu")]
    public GameObject pausePanel;
    public Button resumeButton;
    public Button mainMenuButton;
    public Button restartButton;
    
    [Header("Settings Panel")]
    public GameObject settingsPanel;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public Button backButton;
    
    [Header("Audio")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    
    private bool isPaused = false;
    private float gameTimer = 0f;
    
    void Start()
    {
        InitializeUI();
        LoadSettings();
    }
    
    void Update()
    {
        if (!isPaused && gameUIPanel != null && gameUIPanel.activeInHierarchy)
        {
            gameTimer += Time.deltaTime;
            UpdateTimer();
        }
        
        // Handle pause input
        if (Input.GetKeyDown(KeyCode.Escape) && gameUIPanel != null && gameUIPanel.activeInHierarchy)
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }
    
    void InitializeUI()
    {
        // Main Menu buttons
        if (playButton != null)
            playButton.onClick.AddListener(StartGame);
        if (settingsButton != null)
            settingsButton.onClick.AddListener(OpenSettings);
        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
        
        // Game UI buttons
        if (pauseButton != null)
            pauseButton.onClick.AddListener(PauseGame);
        
        // Pause Menu buttons
        if (resumeButton != null)
            resumeButton.onClick.AddListener(ResumeGame);
        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(GoToMainMenu);
        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);
        
        // Settings buttons
        if (backButton != null)
            backButton.onClick.AddListener(CloseSettings);
        if (musicVolumeSlider != null)
            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        if (sfxVolumeSlider != null)
            sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        
        // Initialize panels
        ShowMainMenu();
    }
    
    public void StartGame()
    {
        int playerCount = 2; // Default value
        if (playerCountDropdown != null)
            playerCount = playerCountDropdown.value + 2; // 2-4 players
        
        // Pass player count to GameManager
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.numberOfPlayers = playerCount;
        }
        
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(false);
        if (gameUIPanel != null)
            gameUIPanel.SetActive(true);
        gameTimer = 0f;
        
        // Start background music
        if (musicSource != null && !musicSource.isPlaying)
            musicSource.Play();
    }
    
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        if (pausePanel != null)
            pausePanel.SetActive(true);
    }
    
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void ShowMainMenu()
    {
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);
        if (gameUIPanel != null)
            gameUIPanel.SetActive(false);
        if (pausePanel != null)
            pausePanel.SetActive(false);
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }
    
    public void OpenSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(false);
    }
    
    public void CloseSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);
        SaveSettings();
    }
    
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    
    public void SetMusicVolume(float volume)
    {
        if (musicSource != null)
            musicSource.volume = volume;
    }
    
    public void SetSFXVolume(float volume)
    {
        if (sfxSource != null)
            sfxSource.volume = volume;
    }
    
    void UpdateTimer()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(gameTimer / 60f);
            int seconds = Mathf.FloorToInt(gameTimer % 60f);
            timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
        }
    }
    
    public void UpdateScore(int currentPlayer, int position)
    {
        if (scoreText != null)
            scoreText.text = "Player " + (currentPlayer + 1).ToString() + ": Position " + position.ToString();
    }
    
    void SaveSettings()
    {
        if (musicVolumeSlider != null)
            PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        if (sfxVolumeSlider != null)
            PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
        PlayerPrefs.Save();
    }
    
    void LoadSettings()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.7f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.8f);
        
        if (musicVolumeSlider != null)
            musicVolumeSlider.value = musicVolume;
        if (sfxVolumeSlider != null)
            sfxVolumeSlider.value = sfxVolume;
        
        SetMusicVolume(musicVolume);
        SetSFXVolume(sfxVolume);
    }
    
    public void ShowWinScreen(int winnerPlayer)
    {
        // This can be called by GameManager when game ends
        GameObject winScreen = GameObject.Find("WinPanel");
        if (winScreen != null)
        {
            winScreen.SetActive(true);
            TextMeshProUGUI winnerText = winScreen.GetComponentInChildren<TextMeshProUGUI>();
            if (winnerText != null)
                winnerText.text = "Player " + (winnerPlayer + 1).ToString() + " Wins!";
        }
    }
}