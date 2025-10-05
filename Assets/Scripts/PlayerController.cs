using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public int playerID;
    public Color playerColor = Color.red;
    public float moveSpeed = 5f;
    public float bounceHeight = 0.5f;
    
    [Header("Visual Effects")]
    public ParticleSystem moveEffect;
    public AudioClip moveSound;
    
    private Vector3 targetPosition;
    private bool isMoving = false;
    private AudioSource audioSource;
    private Renderer playerRenderer;
    private Vector3 originalScale;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
            
        playerRenderer = GetComponent<Renderer>();
        if (playerRenderer != null)
            playerRenderer.material.color = playerColor;
            
        originalScale = transform.localScale;
        targetPosition = transform.position;
    }
    
    void Update()
    {
        if (isMoving)
        {
            MoveToTarget();
        }
    }
    
    public void SetTargetPosition(Vector3 newTarget)
    {
        targetPosition = newTarget;
        isMoving = true;
        
        // Play move sound
        if (moveSound != null && audioSource != null)
            audioSource.PlayOneShot(moveSound);
            
        // Play particle effect
        if (moveEffect != null)
            moveEffect.Play();
    }
    
    void MoveToTarget()
    {
        float step = moveSpeed * Time.deltaTime;
        
        // Add bounce effect during movement
        Vector3 currentPos = Vector3.MoveTowards(transform.position, targetPosition, step);
        float bounceOffset = Mathf.Sin(Time.time * 10f) * bounceHeight * 0.1f;
        currentPos.y += bounceOffset;
        
        transform.position = currentPos;
        
        // Check if reached target
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            transform.position = targetPosition;
            isMoving = false;
            
            // Stop particle effect
            if (moveEffect != null)
                moveEffect.Stop();
                
            // Bounce animation when reaching target
            StartCoroutine(BounceAnimation());
        }
    }
    
    IEnumerator BounceAnimation()
    {
        float duration = 0.3f;
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            
            // Scale bounce
            float scaleMultiplier = 1f + Mathf.Sin(t * Mathf.PI) * 0.2f;
            transform.localScale = originalScale * scaleMultiplier;
            
            yield return null;
        }
        
        transform.localScale = originalScale;
    }
    
    public bool IsMoving()
    {
        return isMoving;
    }
    
    public void SetPlayerColor(Color color)
    {
        playerColor = color;
        if (playerRenderer != null)
            playerRenderer.material.color = playerColor;
    }
    
    public void PlayCelebration()
    {
        StartCoroutine(CelebrationAnimation());
    }
    
    IEnumerator CelebrationAnimation()
    {
        float duration = 2f;
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            
            // Spin and scale animation
            transform.Rotate(0, 0, 360 * Time.deltaTime);
            float scaleMultiplier = 1f + Mathf.Sin(elapsed * 5f) * 0.3f;
            transform.localScale = originalScale * scaleMultiplier;
            
            yield return null;
        }
        
        transform.localScale = originalScale;
        transform.rotation = Quaternion.identity;
    }
}