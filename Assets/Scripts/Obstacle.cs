using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{    
    private GameController gameController;

    private Material mat;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        mat = GetComponent<MeshRenderer>().material;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(gameController.ObstacleHit())
            {
                StartCoroutine(Blink());
            }            
        }
    }

    IEnumerator Blink()
    {
        float blinks = 7;        
        float blinksDone = 0;

        float secondsPerBlink = .75f;
        float halfBlinkTime = secondsPerBlink / 2;
        float secondsInCurrentBlink = 0;

        bool rampingUp = true;
        Color nonEmissive = Color.black;
        Color emissiveColor = Color.red;
        Color colorToUse = nonEmissive;
        while(blinksDone < blinks)
        {            
            if (rampingUp)
            {
                float t = secondsInCurrentBlink / halfBlinkTime;
                colorToUse = Color.Lerp(nonEmissive, emissiveColor, t);                                
            }
            else
            {
                float t = (secondsInCurrentBlink - halfBlinkTime) / halfBlinkTime;
                colorToUse = Color.Lerp(emissiveColor, nonEmissive, t);
            }

            secondsInCurrentBlink += Time.deltaTime;
            
            if(rampingUp && secondsInCurrentBlink >= halfBlinkTime)
            {
                rampingUp = false;
            }
            else if(!rampingUp && secondsInCurrentBlink >= secondsPerBlink)
            {
                rampingUp = true;
                secondsInCurrentBlink = 0;
                blinksDone++;
            }

            mat.SetColor("_EmissionColor", colorToUse);

            yield return null;
        }        
    }
}
