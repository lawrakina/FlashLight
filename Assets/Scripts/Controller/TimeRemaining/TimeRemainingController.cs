﻿using FpsUnity.Interface;
using System.Collections.Generic;
using UnityEngine;
                 

namespace FpsUnity.Controller
{
    public sealed class TimeRemainingController : IExecute
    {
        #region Fields

        private readonly List<ITimeRemaining> _timeRemainings;

        #endregion


        #region UnityMethods

        public void Execute()
        {
            var time = Time.deltaTime;
            for (var i = 0; i < _timeRemainings.Count; i++)
            {
                var obj = _timeRemainings[i];
                obj.CurrentTime -= time;
                if (obj.CurrentTime <= 0.0f)
                {
                    obj?.Method?.Invoke();
                    if (!obj.IsRepeating)
                    {
                        obj.RemoveTimeRemaining();
                    }
                    else
                    {
                        obj.CurrentTime = obj.Time;
                    }
                }
            }
        }

        #endregion


        #region Methods

        public TimeRemainingController()
        {
            _timeRemainings = TimeRemainingExtensions.TimeRemaining;
        }

        #endregion
    }
}