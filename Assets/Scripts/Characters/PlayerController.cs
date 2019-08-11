using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace GameJam
{
    public class PlayerController : Character
    {
		public Transform HealthBar;

        public GameObject[] weaponPrefabs;
        private GameObject weapon;
        private int currentWeapon = 0;

        public Inventory inventory;

        AudioSource audioSource;

        private float moveInput;

        // Start is called before the first frame update
        public new void Start()
        {
            base.Start();

            audioSource = GetComponent<AudioSource>();
            if (weaponPrefabs.Length > 0)
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
                if (currentWeapon >= weaponPrefabs.Length)
                    currentWeapon = 0;
                GameObject prefab = weaponPrefabs[currentWeapon];
                weapon = Instantiate(prefab, transform);
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
            if (inventory == null)
            {
                inventory = new Inventory();
            }

            inventory.Weapons.Add(weapon);
        }

        public void AddConsommableToInventory(Consommable consommable)
        {
            inventory.Consommables.Add(consommable);
        }
    }
}