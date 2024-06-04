using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class ScoreTxtUI : MonoBehaviour
{
    private TMPro.TMP_Text scoreTxt;
    private void Start()
    {
        scoreTxt = GetComponent<TMPro.TMP_Text>();
        scoreTxt.text = 0.ToString();
    }
    public void UpdateScore(int newScore)
    {
        scoreTxt.text = newScore.ToString();
    }
}
