using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // GameManager ΩÃ±€≈Ê √≥∏Æ
    public static GameManager instance { get; set; }
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    public float noteSpeed;

    public GameObject scoreUI;
    private float score;
    private Text scoreText;

    public GameObject comboUI;
    private float combo;
    private Text comboText;
    private Animator comboAnimator;

    public enum judges { NONE=0,BAD,GOOD,PERFECT,MISS};

    public GameObject judgeUI;
    private Sprite[] judgeSprites;
    private Image judgementSpriteRenderer;
    private Animator judgementSpriteAnimator;

    public GameObject[] trails;
    private SpriteRenderer[] trailSpriteRenderers;

    private AudioSource audioSource;
    private string music = "Drops of H20";

    void MusicStart()
    {
        AudioClip audioClip = Resources.Load<AudioClip>("Beats/" + music);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    void Start()
    {
        Invoke("MusicStart", 2);
        judgementSpriteRenderer = judgeUI.GetComponent<Image>();
        judgementSpriteAnimator= judgeUI.GetComponent<Animator>();
        scoreText = scoreUI.GetComponent<Text>();
        comboText = comboUI.GetComponent<Text>();
        comboAnimator= comboUI.GetComponent<Animator>();

        judgeSprites = new Sprite[4];
        judgeSprites[0] = Resources.Load<Sprite>("Sprites/Bad");
        judgeSprites[1] = Resources.Load<Sprite>("Sprites/Good");
        judgeSprites[2] = Resources.Load<Sprite>("Sprites/Miss");
        judgeSprites[3] = Resources.Load<Sprite>("Sprites/Perfect");

        trailSpriteRenderers = new SpriteRenderer[trails.Length];
        for(int i=0; i < trails.Length; i++)
        {
            trailSpriteRenderers[i] = trails[i].GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D)) ShineTrail(0);
        if (Input.GetKey(KeyCode.F)) ShineTrail(0);
        if (Input.GetKey(KeyCode.J)) ShineTrail(0);
        if (Input.GetKey(KeyCode.K)) ShineTrail(0);

        for(int i = 0; i < trailSpriteRenderers.Length; i++)
        {
            Color color = trailSpriteRenderers[i].color;
            color.a -= 0.01f;
            trailSpriteRenderers[i].color = color;
        }
    }

    public void ShineTrail(int index)
    {
        Color color = trailSpriteRenderers[index].color;
        color.a = 0.32f;
        trailSpriteRenderers[index].color = color;
    }

    void showJudgement()
    {
        string scoreFormat = "000000";
        scoreText.text = score.ToString(scoreFormat);
        judgementSpriteAnimator.SetTrigger("Show");
        if (combo >= 2)
        {
            comboText.text = "COMBO" + combo.ToString();
            comboAnimator.SetTrigger("Show");
        }
    }

    public void processJudge(judges judge,int noteType)
    {
        if (judge == judges.NONE) return;
        if (judge == judges.MISS)
        {
            judgementSpriteRenderer.sprite = judgeSprites[2];
            combo = 0;
            if (score >= 15) score -= 15;
        }
        else if (judge == judges.BAD)
        {
            judgementSpriteRenderer.sprite = judgeSprites[0];
            combo = 0;
            if (score >= 5) score -= 5;
        }
        else
        {
            if (judge == judges.PERFECT)
            {
                judgementSpriteRenderer.sprite = judgeSprites[3];
                
                score += 20;
            }
            else if (judge == judges.GOOD)
            {
                judgementSpriteRenderer.sprite = judgeSprites[1];

                score += 15;
            }
            combo += 1;
            score += (float)combo * 0.1f;
        }
        showJudgement();
    }

}
