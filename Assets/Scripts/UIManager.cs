using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    private UnityEvent scoreChanged;

    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnScoreChange += ChangeScore;
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
