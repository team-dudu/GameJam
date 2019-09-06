using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Zenject;

namespace GameJam
{
    public class PlayerController : Character
    {
        //public IPlayerController _playerControler;

        public Transform HealthBar;

        public List<GameObject> weaponPrefabs;

        private GameObject weapon;

        private int currentWeapon = 0;

        AudioSource audioSource;

        private float moveInput;
        
        //[Inject]
        //public void Init(IPlayerController playerControler)
        //{
        //    _playerControler = playerControler;
        //}
       

        public new void Start()
        {
            base.Start();
            audioSource = GetComponent<AudioSource>();
            if (weaponPrefabs.Count > 0)
                weapon = Instantiate(weaponPrefabs[currentWeapon], transform);
        }

        // Update is called once per frame
        new void Update()
        {
            Move(Input.GetAxis("Horizontal"), false, Input.GetButtonDown("Jump"));
            if (Input.GetButtonDown("Dash"))
            {
                _animator.SetAnimation(AnimationParameter.Dash);
                Move(m_FacingRight ? m_DashForce : -m_DashForce, false, false);
            }

            if (Input.GetButton("Fire1") && _animator.GetCurrentAnimatorClipInfo(0)?[0].clip?.name != "Player_fire" &&
                !Input.GetButton("Dash"))
            {
                IAttack attack = weapon?.GetComponent<IAttack>();

                if (attack is MeleeAttack)
                {
                    _animator.SetAnimation(AnimationParameter.Attack);
                }
                else if (attack is DistanceAttack)
                {
                    _animator.SetAnimation(AnimationParameter.Fire);
                }

                attack?.Shoot(transform.right,null);
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                Destroy(weapon);
                currentWeapon++;
                if (currentWeapon >= weaponPrefabs.Count)
                {
                    currentWeapon = 0;
                }

                weapon = Instantiate(weaponPrefabs[currentWeapon], transform);
            }
        }

        public void Restart()
        {
            SceneManager.LoadScene(1);
        }

		public override void TakeDamage(int damage)
		{
			base.TakeDamage(damage);

            _animator.SetAnimation(AnimationParameter.Hurt);

            // Update health bar on UI
            var bar = HealthBar.Find("Bar");

			bar.localScale = new Vector3(
				(float)health / MaxHealth,
				bar.localScale.y,
				bar.localScale.z
			);
		}

        private void OnCollisionEnter2D(Collision2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    print("ok");
                    break;
            }
        }

        public void AddWeaponToInventory(Weapon weapon)
        {
            GameObject weaponObject = new GameObject(weapon.name);

            if (weapon.objectType == ObjectType.Weaponrange)
            {
                weaponObject.AddComponent<DistanceAttack>();
                var res = weaponObject.GetComponent<DistanceAttack>();
                res.firePoint = transform.GetChild(0);
            }

            weaponPrefabs.Add(weaponObject);
        }

        public void AddConsommableToInventory(Consommable consommable)
        {
        }
    }
}