using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Main Menu UI")]
    public GameObject mainMenuPanel;
    public Button playButton;
    public Button settingsButton;
    public Button quitButton;
    public Dropdown playerCountDropdown;
    
    [Header("Game UI")]
    public GameObject gameUIPanel;
    public Button pauseButton;
    public Text scoreText;
    public Text timerText;
    
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
        if (!isPaused && gameUIPanel.activeInHierarchy)
        {
            gameTimer += Time.deltaTime;
            UpdateTimer();
        }
        
        // Handle pause input
        if (Input.GetKeyDown(KeyCode.Escape) && gameUIPanel.activeInHierarchy)
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
        playButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(OpenSettings);
        quitButton.onClick.AddListener(QuitGame);
        
        // Game UI buttons
        pauseButton.onClick.AddListener(PauseGame);
        
        // Pause Menu buttons
        resumeButton.onClick.AddListener(ResumeGame);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
        restartButton.onClick.AddListener(RestartGame);
        
        // Settings buttons
        backButton.onClick.AddListener(CloseSettings);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        
        // Initialize panels
        ShowMainMenu();
    }
    
    public void StartGame()
    {
        int playerCount = playerCountDropdown.value + 2; // 2-4 players
        
        // Pass player count to GameManager
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.numberOfPlayers = playerCount;
        }
        
        mainMenuPanel.SetActive(false);
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
        pausePanel.SetActive(true);
    }
    
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
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
        mainMenuPanel.SetActive(true);
        gameUIPanel.SetActive(false);
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }
    
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }
    
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
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
        int minutes = Mathf.FloorToInt(gameTimer / 60f);
        int seconds = Mathf.FloorToInt(gameTimer % 60f);
        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }
    
    public void UpdateScore(int currentPlayer, int position)
    {
        scoreText.text = $"Player {currentPlayer + 1}: Position {position}";
    }
    
    void SaveSettings()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
        PlayerPrefs.Save();
    }
    
    void LoadSettings()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.7f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.8f);
        
        musicVolumeSlider.value = musicVolume;
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
            Text winnerText = winScreen.GetComponentInChildren<Text>();
            if (winnerText != null)
                winnerText.text = $"Player {winnerPlayer + 1} Wins!";
        }
    }
}