﻿using JJ.Framework.Presentation.VectorGraphics.Models.Styling;
using JJ.Framework.Exceptions;
using System.Diagnostics;
using JJ.Framework.Presentation.VectorGraphics.Helpers;

namespace JJ.Framework.Presentation.VectorGraphics.Models.Elements
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class Point : Element
    {
        public Point()
        {
            Position = new PointPosition(this);
        }

        public override ElementPosition Position { get; }

        private PointStyle _pointStyle = new PointStyle();
        /// <summary> not nullable, auto-instantiated </summary>
        public PointStyle PointStyle
        {
            [DebuggerHidden]
            get { return _pointStyle; }
            set
            {
                if (value == null) throw new NullException(() => value);
                _pointStyle = value;
            }
        }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}