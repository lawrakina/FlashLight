﻿using FpsUnity.Interface;


namespace FpsUnity.Model
{
    public sealed class Wall : BaseObjectScene, ISelectObject
    {
        public string GetMessage()
        {
            return Name;
        }
    }
}