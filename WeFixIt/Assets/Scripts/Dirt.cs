using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour
{

    [SerializeField]
    float dirtyness;
    [SerializeField]
    float currentDirtyness;

    [SerializeField]
    Hose hose_ref;

    [SerializeField]
    Game game_ref;


    [SerializeField]
    int scoreValue;
    // Start is called before the first frame update
    void Start()
    {
        currentDirtyness = dirtyness;

        if (!game_ref || game_ref == null)
        {
            game_ref = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
        }

        if (!hose_ref || hose_ref == null)
        {
            hose_ref = GameObject.FindGameObjectWithTag("Hose").GetComponent<Hose>();
        }
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleTrigger()
    {
        //currentDirtyness -= hose_ref.getWaterJet().GetSafeTriggerParticlesSize(ParticleSystemTriggerEventType.Enter);
        //print(currentDirtyness);
        //if (currentDirtyness <= 0)
        //{
        //    this.game_ref.Score(scoreValue);
        //    Destroy(this.gameObject);
        //}
    }

    private void OnParticleCollision(GameObject other)
    {
        // tá funcionando só se for collision (não trigger) mas daí tem o problema que os personagens não vão conseguir atravessar a sujeira
        //talvez com layers de?
        //deve ter uma maneira de usar trigger but SONO

        if (other.CompareTag("WaterJet"))
        {
            //mas do jeito que tá a sujeira se autodestrói já
            currentDirtyness --;
            print(currentDirtyness + "col");
            //currentDirtyness -= ParticlePhysicsExtensions.GetSafeTriggerParticlesSize(other.GetComponent<ParticleSystem>(),ParticleSystemTriggerEventType.Enter);
            //print(currentDirtyness + "trigg");

            if (currentDirtyness <= 0)
            {
                this.game_ref.Score(scoreValue);
                Destroy(this.gameObject);
            }
        }

        /*
        currentDirtyness -= ParticlePhysicsExtensions.GetSafeCollisionEventSize(hose_ref.getWaterJet());
        print(currentDirtyness + "col");
        currentDirtyness -= ParticlePhysicsExtensions.GetSafeTriggerParticlesSize(hose_ref.getWaterJet(), ParticleSystemTriggerEventType.Enter);
        print(currentDirtyness + "trigg");
        */

    }
    


}
