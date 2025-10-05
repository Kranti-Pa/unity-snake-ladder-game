using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DiceController : MonoBehaviour
{
    [Header("Dice Settings")]
    public Sprite[] diceSprites;
    public Image diceImage;
    public float rollDuration = 1f;
    public AudioClip rollSound;
    
    private AudioSource audioSource;
    private bool isRolling = false;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }
    
    public int RollDice()
    {
        if (isRolling) return 0;
        
        int result = Random.Range(1, 7);
        StartCoroutine(AnimateDiceRoll(result));
        return result;
    }
    
    IEnumerator AnimateDiceRoll(int finalResult)
    {
        isRolling = true;
        
        // Play roll sound
        if (rollSound != null && audioSource != null)
            audioSource.PlayOneShot(rollSound);
        
        float elapsed = 0f;
        
        // Animate dice rolling
        while (elapsed < rollDuration)
        {
            if (diceSprites != null && diceSprites.Length >= 6 && diceImage != null)
            {
                int randomFace = Random.Range(0, 6);
                diceImage.sprite = diceSprites[randomFace];
            }
            
            elapsed += Time.deltaTime;
            yield return new WaitForSeconds(0.1f);
        }
        
        // Show final result
        if (diceSprites != null && diceSprites.Length >= finalResult && diceImage != null)
        {
            diceImage.sprite = diceSprites[finalResult - 1];
        }
        
        isRolling = false;
    }
    
    public bool IsRolling()
    {
        return isRolling;
    }
}