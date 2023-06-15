using UnityEngine;
using TMPro;
namespace StateManager {
    [CreateAssetMenu(menuName = "States/Character/Walk")]
    public class WalkState : State<CharacterController>
    {
        private Rigidbody2D rigidbody;
        private Transform transform;
        public Vector2 input;
        //private float hunger = -1;
        //private float maxHunger;
        private float inputCounter;
        private TextMeshProUGUI hungerValue;

        [SerializeField] private float speed = 0.1f;
        [SerializeField] PlayerData characterData;

        public override void Init(CharacterController parent)
        {
            base.Init(parent);
            parent.GetComponentInChildren<SpriteRenderer>().color = Color.white;

            if (rigidbody == null) rigidbody = parent.GetComponent<Rigidbody2D>();
            if (transform == null) transform = parent.GetComponent<Transform>();

            if (transform == null) transform = parent.GetComponent<Transform>();

            //hunger = parent.GetComponent<CharacterController>().hunger;
            //maxHunger = parent.GetComponent<CharacterController>().maxHunger;
            if (hungerValue == null) hungerValue = parent.GetComponent<CharacterController>().hungerValue;

        }
        public override void InputHandler()
        {
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");
            //input.Normalize();
            if (input.magnitude == 0)
            {
                inputCounter += Time.deltaTime;
            }
            else
            {
                inputCounter = 0;
            }
        }

        public override void Update()
        {
            rigidbody.velocity = new Vector2(input.x * speed, input.y * speed);
            characterData.hunger += 2*Time.deltaTime;

            Collider2D collider = Physics2D.OverlapCircle(transform.position, 0.5f);
            if(collider != null)
            {
                if (collider.CompareTag("food"))
                {
                    collider.gameObject.SetActive(false);
                    characterData.hunger = 0;
                }
            }


            hungerValue.SetText("Hunger: " + ((int)characterData.hunger).ToString() + " of " + characterData.maxHunger.ToString());
        }
        public override void FixedUpdate()
        {
        }
        public override void ChangeState()
        {
            if (characterData.hunger >= characterData.maxHunger)
            {
                _manager.SetState(typeof(DeadState));
            }
            else if (inputCounter >= 1f)
            {
                _manager.SetState(typeof(PatrolState));
            }
            //transform.parent.GetComponent<CharacterController>().SetHunger(hunger);
        }

        public override void Exit()
        {
        }
        
        
        


    }
}
