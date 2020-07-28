using System.Collections.Generic;
using System.Linq;
using FpsUnity.Enums;
using FpsUnity.Healper;
using FpsUnity.Interface;
using FpsUnity.Model;
using FpsUnity.Services;
using UnityEngine;
using UnityEngine.AI;


namespace FpsUnity.Controller
{
    public sealed class EnemyController : BaseController, IInitualization, IExecute
    {
        #region Fields

        private int _countBots = 100;
        private List<Bot> _bots = new List<Bot>();

        #endregion


        #region IInitialization

        public void Initialization()
        {
            for (var index = 0; index < _countBots; index++)
            {
                var tempBot = Object.Instantiate(ServiceLocatorMonoBehavior.GetService<Reference>().Bot,
                    GenericPoint(ServiceLocatorMonoBehavior.GetService<CharacterController>().transform),
                    Quaternion.identity);

                tempBot.Agent.avoidancePriority = index;
                tempBot.Target = ServiceLocatorMonoBehavior.GetService<CharacterController>().transform;
                //todo разных противников
                AddBotToList(tempBot);
            }
        }

        public Vector3 GenericPoint(Transform agent)
        {
            Vector3 result;

            var dis = Random.Range(5, 50);
            
            Vector3 randomPoint;
            do {
                randomPoint = Random.insideUnitSphere * dis;
            } 
            while (SearchSphere(randomPoint, 1.0f));


            NavMesh.SamplePosition(agent.position + randomPoint,
                out var hit, dis, NavMesh.AllAreas);
            result = hit.position;

            return result;
        }

        #endregion

        private bool SearchSphere(Vector3 center, float radius)
        {
            var hitColliders = Physics.OverlapSphere(center, radius);
            return hitColliders.Any();
        }

        private void AddBotToList(Bot bot)
        {
            if (!_bots.Contains(bot))
            {
                _bots.Add(bot);
                bot.OnDieChange += RemoveBotToList;
            }
        }

        private void RemoveBotToList(Bot bot)
        {
            if (!_bots.Contains(bot))
            {
                return;
            }

            bot.OnDieChange -= RemoveBotToList;
            _bots.Remove(bot);
        }


        #region IExecute

        public void Execute()
        {
            if (!IsActive)
            {
                return;
            }

            for (var i = 0; i < _bots.Count; i++)
            {
                _bots[i].Execute();
            }
        }

        #endregion
    }
}