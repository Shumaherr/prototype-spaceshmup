using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Image healthIcon;
    public List<Sprite> healthIcons;
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnScoreChange += ChangeScore;
        GameManager.Instance.OnHealthChange += ChangeHealth;
        healthIcon.sprite = healthIcons[healthIcons.Capacity - 1];
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
                healthIcon.sprite = null;
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
