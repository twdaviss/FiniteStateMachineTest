using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace StateManager
{
    public class CharacterController : StateMachine<CharacterController>
    {
        [SerializeField] public Transform[] stops;
        [SerializeField] public TextMeshProUGUI hungerValue;
        [SerializeField] public TextMeshProUGUI resetPrompt;

    }
}
