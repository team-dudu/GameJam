using UnityEngine;

namespace GameJam
{
	public class HealthBar : MonoBehaviour
	{
		public Character Character;
		Transform bar;

		// Start is called before the first frame update
		void Start()
		{
			bar = transform.Find("Bar");
		}

		// Update is called once per frame
		void Update()
		{
			bar.localScale = new Vector3((float)Character.health / Character.MaxHealth, bar.localScale.y, bar.localScale.z);
		}
	}
}

