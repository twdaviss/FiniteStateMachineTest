using UnityEngine;
using TMPro;
namespace StateManager {
    [CreateAssetMenu(menuName = "States/Character/Dead")]
    public class DeadState : State<CharacterController>
    {
        private Rigidbody2D rigidbody;
        private Transform transform;
        public Vector2 input;
        //private float hunger = -1;
        //private float maxHunger;
        private float inputCounter;
        private TextMeshProUGUI hungerValue;
        private TextMeshProUGUI resetPrompt;

        [SerializeField] private float speed = 0.1f;
        [SerializeField] PlayerData characterData;

        public override void Init(CharacterController parent)
        {
            base.Init(parent);
            parent.GetComponentInChildren<SpriteRenderer>().color = Color.black;

            if (rigidbody == null) rigidbody = parent.GetComponent<Rigidbody2D>();
            if (transform == null) transform = parent.GetComponent<Transform>();

            if (transform == null) transform = parent.GetComponent<Transform>();

            if (hungerValue == null) hungerValue = parent.GetComponent<CharacterController>().hungerValue;
            if(resetPrompt == null) resetPrompt = parent.GetComponent<CharacterController>().resetPrompt;
            resetPrompt.gameObject.SetActive(true);
            rigidbody.velocity = new Vector2(0f, 0f);

        }
        public override void InputHandler()
        {
           
        }

        public override void Update()
        {
        }
        public override void FixedUpdate()
        {
        }
        public override void ChangeState()
        {
            if (Input.GetMouseButton(0))
            {
                resetPrompt.gameObject.SetActive(false);
                _manager.SetState(typeof(WalkState));
            }
        }

        public override void Exit()
        {

            transform.position = new Vector3(0, 0, transform.position.z);
            characterData.hunger = 0;
            
            GameObject foods = GameObject.Find("Foods");
            for (int i = 0; i < foods.transform.childCount; i++)
            {
                if (foods.transform.GetChild(i).gameObject.activeSelf == false)
                {
                    foods.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
        


    }
}
