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


        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.5f);
            StartAnim();
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
            if (Math.Abs(hp - maxHp / 4) < 20 && _stage < 3)
            {
                var _moving = GetComponent<MovingObj>();
                _stage++;
                _moving.Path = _secondPath;
                _moving.Start();
                _moving.speed = 2;
                return currentScale * 3f / 4f;
            }

            if (Math.Abs(hp - maxHp / 2) < 20 && _stage < 2)
            {
                _stage++;
                Invoke(nameof(ThirdStage), 0.5f);
                return currentScale * 3f / 4f;
            }

            if (Math.Abs(hp - maxHp * 3f / 4) >= 20 || _stage > 0) return currentScale;

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
        }
    }
}