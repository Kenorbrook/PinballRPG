using System;
using System.Collections;
using ProjectFiles.Enemies;
using ProjectFiles.MovingObjects;
using UnityEngine;

namespace ProjectFiles.Bosses
{
    public class FirstCircleBoss : Boss
    {
        [SerializeField]
        private GameObject[] Spikes;

        [SerializeField]
        private GameObject _wallSpikes;

        private int _stage;

        [SerializeField]
        private MovementPath _secondPath;
        [SerializeField]
        private MovementPath _firstPath;


        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.5f);
            StartAnim();
        }

        private void FirstStage()
        {
            Spikes[1].SetActive(false);
            Spikes[2].SetActive(false);
            Spikes[Spikes.Length - 1].SetActive(false);
            var _moving = GetComponent<MovingObjectOnPath>();
            _moving.Path = _firstPath;
            _moving.Start();
            _moving.speed = 1;
        }

        private void SecondStage()
        {
            Debug.Log("Second Stage boss");
            Spikes[Spikes.Length - 1].SetActive(true);
        }

        private void ThirdStage()
        {
            Debug.Log("Third Stage boss");
            Spikes[1].SetActive(true);
            Spikes[2].SetActive(true);
            Spikes[Spikes.Length - 1].SetActive(false);
        }

        public override float GetDamage(int hp, int maxHp, float currentScale)
        {
            if ((float)hp/maxHp < .25f && _stage < 3)
            {
                var _moving = GetComponent<MovingObjectOnPath>();
                _stage++;
                _moving.Path = _secondPath;
                _moving.Start();
                _moving.speed = 2;
                return currentScale * 3f / 4f;
            }

            if ((float)hp/maxHp < .5f && _stage < 2)
            {
                _stage++;
                Invoke(nameof(ThirdStage), 0.5f);
                return currentScale * 3f / 4f;
            }

            if ((float)hp/maxHp >= .75f || _stage > 0) return currentScale;

            _stage++;
            Invoke(nameof(SecondStage), 0.5f);
            return currentScale * 3f / 4f;
        }

        public override void StartAnim()
        {
            _wallSpikes.SetActive(true);
            @interface.EnableBossHealth();
        }

        public override void EndAnim()
        {
            _wallSpikes.SetActive(false);
            @interface.DisableBossHealth();
            @interface.KillingBoss();
        }

        public override void Reset()
        {
            _wallSpikes.SetActive(false);
            @interface.DisableBossHealth();
            _stage = 0;
            FirstStage();
            StartCoroutine(Start());
        }
    }
}