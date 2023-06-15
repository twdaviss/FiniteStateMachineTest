using UnityEngine;
using TMPro;
namespace StateManager {
    [CreateAssetMenu(menuName = "States/Character/Patrol")]
    public class PatrolState : State<CharacterController>
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

        private GameObject stops;

        [SerializeField] private float speed = 0.1f;
        [SerializeField] PlayerData characterData;

        public override void Init(CharacterController parent)
        {
            parent.GetComponentInChildren<SpriteRenderer>().color = Color.yellow;

            base.Init(parent);
            if (transform == null) transform = parent.GetComponent<Transform>();
            if (rigidbody == null) rigidbody = parent.GetComponent<Rigidbody2D>();
            stops = GameObject.Find("Stops");

            //hunger = parent.GetComponent<CharacterController>().hunger;
            //maxHunger = parent.GetComponent<CharacterController>().maxHunger;
            if (hungerValue == null) hungerValue = parent.GetComponent<CharacterController>().hungerValue;


        }
        public override void InputHandler()
        {
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");
            input.Normalize();
        }

        public override void Update()
        {
            int index;
            if (target == null)
            {
                index = Random.Range(1, stops.transform.childCount);
                target = stops.transform.GetChild(index);
            }
            if (Vector2.Distance(target.position, transform.position) < 0.5f)
            {
                index = Random.Range(1, stops.transform.childCount);
                target = stops.transform.GetChild(index);
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
                _manager.SetState(typeof(HuntState));
            }
            else if (characterData.hunger > characterData.maxHunger / 2)
            {
                _manager.SetState(typeof(HuntState));
            }
        }

        public override void Exit()
        {
        }


    }
}
