using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public float fullY;
    public float frameIncrease;
    public bool isFull = false;
    public float initY;

    private ParticleSystem completionParticle;
    private Game game_ref;




    private void Awake()
    {
        completionParticle = GetComponentInChildren<ParticleSystem>();
        if (!game_ref || game_ref == null)
        {
            game_ref = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
        }
    }

    private void Update()
    {
        transform.localRotation = Quaternion.Euler(Mathf.Sin(Time.time), 0, 0);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (!isFull && other.CompareTag("WaterJet"))
        {
            transform.Translate(new Vector3(0, frameIncrease, 0));
            if (transform.position.y >= fullY)
            {
                transform.position = new Vector3(transform.position.x, fullY, transform.position.z);
                isFull = true;
                completionParticle.Play();
                game_ref.Score(500);
                game_ref.setPoolComplete(true);
            }
            game_ref.updateTextCounters();
        }
    }
}
