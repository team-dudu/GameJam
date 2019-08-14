using GameJam;
using UnityEngine;

public class Monstre : Enemy
{
    public int damage;
    private float _timeBtwDamage = 1.5f;

    private enum Steps
    {
        Step1, // Step1 : 2 A/R -> Move
        Step2, // Step2 : 1 A/R -> CaC
        Step3, // Step3 : 1 A/R -> Fire
        Step4 // Step4 : 1 A/R -> CaC + Jump
    }

    private Steps _currentStep = Steps.Step1;

    public new void Start()
    {
        base.Start();
    }

    public new void Update()
    {
        base.Update();

        // give the player some time to recover before taking more damage !
        if (_timeBtwDamage > 0)
        {
            _timeBtwDamage -= Time.deltaTime;
        }


        switch (_currentStep)
        {
            case Steps.Step1:
                Step1();
                return;
            case Steps.Step2:
                Step2();
                return;
            case Steps.Step3:
                Step3();
                return;
            case Steps.Step4:
                Step4();
                return;
        }
    }

    private void Step1()
    {
        if (_roundTripCount > 4)
            _currentStep = Steps.Step2;
    }

    private void Step2()
    {
        _cacActivated = true;
        if (_roundTripCount > 2)
            _currentStep = Steps.Step3;
    }

    private void Step3()
    {
        _jumpActivated = true;
        if (_roundTripCount > 2)
            _currentStep = Steps.Step4;
    }

    private void Step4()
    {
        _cacActivated = true;
        _jumpActivated = true;
        if (_roundTripCount > 2)
            _currentStep = Steps.Step1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        // deal the player damage !
        if (_timeBtwDamage <= 0)
        {
            other.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }
}