using UnityEngine;
using TMPro;
namespace StateManager {
    [CreateAssetMenu(menuName = "States/Character/Hunt")]
    public class HuntState : State<CharacterController>
    {
        private Transform target;
        private Vector2 targetDir;
        private Vector2 targetDirNormalized;
        private Transform transform;
        private Rigidbody2D rigidbody;
        public Vector2 input;
        //private float hunger;
        //private float maxHunger;
        private TextMeshProUGUI hungerValue;


        [SerializeField]
        private float searchRadius;
        [SerializeField]
        private float speed = 0.1f;
        [SerializeField] PlayerData characterData;

        public override void Init(CharacterController parent)
        {
            base.Init(parent);
            parent.GetComponentInChildren<SpriteRenderer>().color = Color.red;
            if (transform == null) transform = parent.GetComponent<Transform>();
            if (rigidbody == null) rigidbody = parent.GetComponent<Rigidbody2D>();
            //hunger = parent.GetComponent<CharacterController>().hunger;
            //maxHunger = parent.GetComponent<CharacterController>().maxHunger;
            
            if (hungerValue == null) hungerValue = parent.GetComponent<CharacterController>().hungerValue;
            target = null;
        }
        public override void InputHandler()
        {
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");
            input.Normalize();
        }

        public override void Update()
        {
            if (target == null)
            {
                GameObject foods = GameObject.Find("Foods");
                for (int i = 0; i < foods.transform.childCount; i++)
                {
                    if (target == null)
                    {
                        target = foods.transform.GetChild(i);
                    }
                }
                Debug.Log("Target Acquired " + Time.time);
            }

            targetDir = (target.position - transform.position);
            targetDirNormalized = (target.position - transform.position).normalized;
            
            rigidbody.velocity = new Vector2(targetDirNormalized.x * speed, targetDirNormalized.y * speed);
            characterData.hunger += 2 * Time.deltaTime;
            
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
            else if (input.x != 0 || input.y != 0)
            {
                _manager.SetState(typeof(WalkState));
            }
            else if (characterData.hunger > 3 * characterData.maxHunger / 4)
            {
                _manager.SetState(typeof(StarvedState));
            }
            else if (Vector2.Distance(target.position, transform.position) < 0.5f)
            {
                target.gameObject.SetActive(false);
                target = null;
                characterData.hunger = 0;
                _manager.SetState(typeof(PatrolState));
            }
        }

        public override void Exit()
        {
        }


    }
}
