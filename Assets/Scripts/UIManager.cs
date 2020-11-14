using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class UIManager : MonoBehaviour
{

    public Image healthIcon;
    public List<Sprite> healthIcons;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public GameObject gameOverPopup;
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnScoreChange += ChangeScore;
        GameManager.Instance.OnLevelChange += ChangeLevel;
        GameManager.Instance.OnHealthChange += ChangeHealth;
        GameManager.Instance.OnDeath += ShowDialog;
        healthIcon.sprite = healthIcons[healthIcons.Capacity - 1];
        gameOverPopup.SetActive(false);
    }

    private void ShowDialog()
    {
        gameOverPopup.SetActive(true);
    }

    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }

    private void ChangeLevel(int value)
    {
        levelText.text = "Level: " + value.ToString();
    }

    private void ChangeHealth(int value)
    {
        switch (value)
        {
            case 1:
                healthIcon.sprite = healthIcons[0];
                break;
            case 2:
                healthIcon.sprite = healthIcons[1];
                break;
            case 3:
                healthIcon.sprite = healthIcons[2];
                break;
            default:
                healthIcon.enabled = false;
                break;
        }
    }

    private void ChangeScore(int value)
    {
        scoreText.text = "Score: " + value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
