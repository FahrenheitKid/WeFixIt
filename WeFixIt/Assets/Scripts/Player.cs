using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Properties")]
    private int id;

    [Header("Movement")]
    public float maxSpeed;
    public float acceleration;
    public float breakSpeed;
    private Vector3 speed;
    private CharacterController body;
    private RaycastHit hit;

    [Header("Stumble")]
    public float stumbleSpeed;
    public float stumbleBreakSpeed;
    public float stumbleTime;
    private bool stumbling = false;
    private float timer = 0f;

    [Header("Animation")]
    private Animator animator;

    [Header("Rotation")]
    public float maxRotationSpeed;
    private Vector3 direction;

    [SerializeField]
    public Transform itemPosition;

    [SerializeField]
    protected Item item;

    [SerializeField]
     bool canTrashFall;

    [SerializeField]
    bool canHoseFall;

    private void Awake()
    {
        id = 0;
        InputManager.Initialize();
        speed = Vector3.zero;
        body = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!stumbling)
        {
            Move();
        }
        else
        {
            Stumble();
        }
    }

    private void Move()
    {
        bool up = InputManager.GetKey(id, KeyActions.Up);
        bool down = InputManager.GetKey(id, KeyActions.Down);
        bool left = InputManager.GetKey(id, KeyActions.Left);
        bool right = InputManager.GetKey(id, KeyActions.Right);

        if (up)
        {
            direction.z = 1;
            if (!left && !right)
            {
                direction.x = 0;
            }
        }
        else if (down)
        {
            direction.z = -1;
            if (!left && !right)
            {
                direction.x = 0;
            }
        }

        if (left)
        {
            direction.x = -1;
            if (!up && !down)
            {
                direction.z = 0;
            }
        }
        else if (right)
        {
            direction.x = 1;
            if (!up && !down)
            {
                direction.z = 0;
            }
        }

        // X Axis
        if (right && speed.x < maxSpeed) // Going Right
        {
            if (speed.x >= 0) // Already going right
            {
                speed.x += acceleration * Time.deltaTime;
                if (speed.x > maxSpeed)
                {
                    speed.x = maxSpeed;
                }
            }
            else // Switching direction
            {
                speed.x += (acceleration + breakSpeed) * Time.deltaTime;
            }
        }
        else if (left && speed.x > -maxSpeed) // Going Left
        {
            if (speed.x <= 0) // Already going left
            {
                speed.x -= acceleration * Time.deltaTime;
                if (speed.x < -maxSpeed)
                {
                    speed.x = -maxSpeed;
                }
            }
            else // Switching direction
            {
                speed.x -= (acceleration + breakSpeed) * Time.deltaTime;
            }
        }
        else
        {
            if (speed.x > 0)
            {
                speed.x -= breakSpeed * Time.deltaTime;
                if (speed.x < 0)
                {
                    speed.x = 0;
                }
            }
            else if (speed.x < 0)
            {
                speed.x += breakSpeed * Time.deltaTime;
                if (speed.x > 0)
                {
                    speed.x = 0;
                }
            }
        }

        // Z Axis
        if (up && speed.z < maxSpeed) // Going Up
        {
            if (speed.z >= 0)
            {
                speed.z += acceleration * Time.deltaTime;
                if (speed.z > maxSpeed)
                {
                    speed.z = maxSpeed;
                }
            }
            else
            {
                speed.z += (acceleration + breakSpeed) * Time.deltaTime;
            }
        }
        else if (down && speed.z > -maxSpeed) // Going Back
        {
            if (speed.z <= 0)
            {
                speed.z -= acceleration * Time.deltaTime;
                if (speed.z < -maxSpeed)
                {
                    speed.z = -maxSpeed;
                }
            }
            else
            {
                speed.z -= (acceleration + breakSpeed) * Time.deltaTime;
            }
        }
        else
        {
            if (speed.z > 0)
            {
                speed.z -= breakSpeed * Time.deltaTime;
                if (speed.z < 0)
                {
                    speed.z = 0;
                }
            }
            else if (speed.z < 0)
            {
                speed.z += breakSpeed * Time.deltaTime;
                if (speed.z > 0)
                {
                    speed.z = 0;
                }
            }
        }

        animator.SetFloat("speed", speed.magnitude);
        body.SimpleMove(speed);

        if (direction != Vector3.zero)
        {
            Vector3 newFacingDirection = Vector3.RotateTowards(transform.forward, direction, Mathf.Deg2Rad * maxRotationSpeed, Mathf.Infinity);
            transform.forward = newFacingDirection;
        }
    }

    private void Stumble()
    {
        timer += Time.deltaTime;
        if (timer >= stumbleTime)
        {
            stumbling = false;
            timer = 0f;
            return;
        }

        if (speed.x < 0)
        {
            speed.x += stumbleBreakSpeed * Time.deltaTime;
            if (speed.x > 0)
            {
                speed.x = 0;
            }
        }
        else if (speed.x > 0)
        {
            speed.x -= stumbleBreakSpeed * Time.deltaTime;
            if (speed.x < 0)
            {
                speed.x = 0;
            }
        }

        if (speed.z < 0)
        {
            speed.z += stumbleBreakSpeed * Time.deltaTime;
            if (speed.z > 0)
            {
                speed.z = 0;
            }
        }
        else if (speed.z > 0)
        {
            speed.z -= stumbleBreakSpeed * Time.deltaTime;
            if (speed.z < 0)
            {
                speed.z = 0;
            }
        }

        body.SimpleMove(speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TrashTrigger") && !other.gameObject.GetComponent<InteractionCollider>().item_ref.GetBeingCarried())
        {
            print("entrou lixo");
            canTrashFall = true;
        }

        if (other.gameObject.CompareTag("Dirt"))
        {
            triggerStumble();
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Hose") && !stumbling && !canHoseFall)
        {
            if (item && item != null)
            {
                if (!item.CompareTag("Hose"))
                {
                    canHoseFall = true;
                }
            }
            else
            {
                canHoseFall = true;
            }
        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Hose") && !stumbling && canHoseFall)
        {
            if(item && item != null)
            {
                 if(!item.CompareTag("Hose"))
                {
                    triggerStumble();
                    item.Drop();
                }
            }
            else
            {
                triggerStumble();
            }
            
        }

        if (other.gameObject.CompareTag("TrashTrigger") && canTrashFall)
        {
            canTrashFall = false;
            triggerStumble();
        }

        

    }

    public void triggerStumble()
    {
        stumbling = true;
        animator.SetTrigger("stumble");
        speed = speed.normalized * stumbleSpeed;
    }
    public int getId()
    {
        return id;
    }

    public bool getKeyDown(KeyActions ac)
    {
        return InputManager.GetKeyDown(id, ac);
    }

    public bool getKeyUp(KeyActions ac)
    {
        return InputManager.GetKeyUp(id, ac);
    }

    public bool getKey(KeyActions ac)
    {
        return InputManager.GetKey(id, ac);
    }

    public void setItem(Item i)
    {
        item = i;
    }

    public Item GetItem()
    {
        if (item || item != null)
            return item;
        else return null;
    }

    public void setTrashFall(bool b)
    {
        canTrashFall = b;
    }

    public void setHoseFall(bool b)
    {
        canHoseFall = b;
    }
}
