using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

namespace StateMachineFinitePattern
{
    public class Entity : MonoBehaviour
    {
        [Header("Tester")]
        [SerializeField] private Consumable _consumableTarget = null;
        [SerializeField] private Dummy _dummyTarget = null;

        [Header("Miscellaneous")]
        [SerializeField] private PrototypePattern.MeleeScriptable _arm = null;
        [SerializeField] private VenderMachineManager _venderMachine = null;
        [SerializeField] private Transform _handTransform = null;

        [Header("Buffs")]
        [SerializeField] private int _increaseDamage;
        [SerializeField] private float _increaseSpeed;
        [SerializeField] private float _buffTime;

        [Space]
        [SerializeField] private TextMeshProUGUI _moneyText = null;

        private float _money = 0.0f;
        
        private StateMachine _stateMachine;

        //Components
        private Animator _animator;
        private NavMeshAgent _agent;

        //Default parms
        private float _speed;
        private int _damage;

        private float _elapsedTime;
        private bool _isBuffed;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();

            _speed = _agent.speed;
            _damage = _arm.damage;
        }

        private void Start()
        {
            _stateMachine = new StateMachine();

            var searcher = new ConsumablesResearcher(transform, SetConsumable);
            var treiner = new Treiner(_animator, "Attack", GetDummy, GetArm);
            var moveToConsumable = new MoveToTarget(GetConsumablePosition, _agent, _animator, "Speed");
            var moveToVenderMachine = new MoveToTarget(GetVenderMachinePosition, _agent, _animator, "Speed");
            var dummyChooser = new DummyChooser(transform, SetDummy, _agent, _animator, "Speed", FindObjectsOfType<Dummy>());
            var consume = new ConsumeConsumables(GetConsumable, Consume);
            var buyerArms = new BuyerArms(_venderMachine, GetMoney, UseWeapon);
            
            Func<bool> foundConsumable = () => FindObjectOfType<Consumable>() != null;
            Func<bool> notFoundConsumable = () => FindObjectOfType<Consumable>() == null;
            Func<bool> canBuyArm = () => _venderMachine.CanBuyWeapon(_money);
            Func<bool> consumeIsClose = () => Vector3.Distance(transform.position, _consumableTarget.Positon) < 2f;
            Func<bool> dummyIsClose = () => Vector2.Distance(transform.position, _dummyTarget.Position) < 2f;
            Func<bool> venderIsClose = () => Vector2.Distance(transform.position, _venderMachine.transform.position) < .5f;
            Func<bool> exit = () => true;
            
            At(searcher, moveToConsumable, foundConsumable);
            At(moveToConsumable, consume, consumeIsClose);
            At(consume, moveToVenderMachine, canBuyArm);
            At(consume, searcher, exit);
            At(searcher, dummyChooser, notFoundConsumable);
            At(dummyChooser, treiner, dummyIsClose);
            At(treiner, moveToVenderMachine, canBuyArm);
            At(moveToVenderMachine, buyerArms, venderIsClose);
            At(buyerArms, searcher, exit);
            At(treiner, searcher, foundConsumable);

            _stateMachine.SetEntryState(searcher);

            //---------- Local Functions -----------\\

            //Sets
            void SetConsumable(Consumable consum) => _consumableTarget = consum;
            void SetDummy(Dummy dummy) => _dummyTarget = dummy;           
            
            //Gets
            Vector3 GetConsumablePosition() => _consumableTarget.transform.position;
            Vector3 GetVenderMachinePosition() => _venderMachine.transform.position;
            Consumable GetConsumable() => _consumableTarget;
            Dummy GetDummy() => _dummyTarget;
            PrototypePattern.MeleeScriptable GetArm() => _arm;

            //Function to State Machine
            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTrasition(from, to, condition);                 
        }

        private void Update() => _stateMachine?.Tick();            

        private void FixedUpdate()
        {
            if(_isBuffed)
            {
                _elapsedTime += Time.fixedDeltaTime;
                
                if(_elapsedTime > _buffTime)
                {
                    SetDefaultDamage();
                    SetDefaultSpeed();

                    _elapsedTime = 0.0f;
                    _isBuffed = false;
                }
                
            }
        }

        private void SetDefaultDamage() => _arm.damage = _damage;

        private void SetDefaultSpeed() => _agent.speed = _speed;

        private void Consume(Consumable con)
        {
            Consumable.ConsumeType conType = con.Consume();

            if (conType == Consumable.ConsumeType.Money)
            {
                _money += con.amount;
                _moneyText.text = "R$: " + _money.ToString("F2");
            }
            else
            {
                if (conType == Consumable.ConsumeType.Potion)
                {
                    //Increase Damage
                    _arm.damage += _increaseDamage;
                }
                if (conType == Consumable.ConsumeType.Tonic)
                {
                    //Increase Speed
                    _agent.speed += _increaseSpeed;
                }

                _isBuffed = true;
            }
        }
       
        private float GetMoney()
        {
            float money = _money;
                
            _money = 0.0f;
            _moneyText.text = "R$: " + _money.ToString("F2");

            return money;
        }

        private void UseWeapon(PrototypePattern.MeleeScriptable arm)
        {
            _arm = arm;

            Destroy(_handTransform.GetChild(0).gameObject);
            Instantiate(_arm.prefab, _handTransform.position, quaternion.identity, _handTransform);            
        }
    }
}
