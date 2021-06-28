using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : MonoBehaviour
{
    public int noteType;
    private GameManager.judges judge;
    private KeyCode keyCode;
    
    // Start is called before the first frame update
    void Start()
    {
        if(noteType==1) keyCode=KeyCode.D;
        else if (noteType == 2) keyCode = KeyCode.F;
        else if (noteType == 3) keyCode = KeyCode.J;
        else if (noteType == 4) keyCode = KeyCode.K;
    }

    public void Initialize()
    {
        judge = GameManager.judges.NONE;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * GameManager.instance.noteSpeed);

        if (Input.GetKey(keyCode))
        {
            Debug.Log(judge);
            if (judge != GameManager.judges.NONE) gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Bad Line")
        {
            judge = GameManager.judges.BAD;
        }
        else if (other.gameObject.tag == "Good Line")
        {
            judge = GameManager.judges.GOOD;
        }
        else if (other.gameObject.tag == "Perfect Line")
        {
            judge = GameManager.judges.PERFECT;
        }
        if (other.gameObject.tag == "Miss Line")
        {
            judge = GameManager.judges.MISS;
            gameObject.SetActive(false);
        }
        
    }
}
