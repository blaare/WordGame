using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    static private ScoreManager S;

    [Header("Set in Inspector")]
    public List<float> scoreFontSizes = new List<float> { 36, 64, 64, 1 };
    public Vector3 scoreMidPoint = new Vector3(1, 1, 0);
    public float scoreTravelTime = 3f;
    public float scoreComboDelay = 0.5f;

    private RectTransform rectTrans;

    void Awake()
    {
        S = this;
        rectTrans = GetComponent<RectTransform>();
    }



    public static void SCORE(Wyrd wyrd, int combo)
    {

        List<Vector2> pts = new List<Vector2>();

        Vector3 pt = wyrd.letters[0].transform.position;
        pt = Camera.main.WorldToViewportPoint(pt);

        pts.Add(pt);

        pts.Add(S.scoreMidPoint);

        pts.Add(S.rectTrans.anchorMax);

        int value = wyrd.letters.Count * combo;

        FloatingScore fs = Scoreboard.S.CreateFloatingScore(value, pts);

        fs.timeDuration = S.scoreTravelTime;
        fs.timeStart = Time.time + combo * S.scoreComboDelay;
        fs.fontSizes = S.scoreFontSizes;

        fs.easingCurve = Easing.InOut + Easing.InOut;

        string txt = wyrd.letters.Count.ToString();
        if (combo > 1)
        {
            txt += " x " + combo;
        }

        fs.GetComponent<Text>().text = txt;
    }


}
