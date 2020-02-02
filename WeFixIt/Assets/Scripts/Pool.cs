using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public float fullY;
    public float frameIncrease;
    public bool isFull = false;

    private ParticleSystem completionParticle;
    private Game game;

    private void Awake()
    {
        completionParticle = GetComponentInChildren<ParticleSystem>();
        game = FindObjectOfType<Game>();
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
                game.Score(500);
            }
        }
    }
}
