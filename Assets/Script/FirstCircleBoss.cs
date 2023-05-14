using System;
using System.Collections;
using UnityEngine;

namespace Script
{
    public class FirstCircleBoss : MonoBehaviour, IBoss
    {
        [SerializeField]
        private GameObject[] Spikes;

        [SerializeField]
        private GameObject _wallSpikes;

        private int _stage = 0;

        [SerializeField]
        private MovementPath _secondPath;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.5f);
            StartAnim();
        }

        public float GetDamage(int hp, int maxHp, float currentScale)
        {
            if (Math.Abs(hp - maxHp / 4) < 20 && _stage < 3)
            {
                var _moving = GetComponent<MovingObj>();
                _stage++;
                _moving.Path = _secondPath;
                _moving.MyStart();
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


        public void StartAnim()
        {
            _wallSpikes.SetActive(true);
        }

        public void EndAnim()
        {
            _wallSpikes.SetActive(false);
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
    }
}