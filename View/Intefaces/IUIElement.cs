﻿using System.Drawing;

namespace TrainConfigurator.View.Intefaces
{
    public interface IUIElement
    {
        public Point Position { get; set; }
        public int Width { get; }
        public int Height { get; }
    }
}