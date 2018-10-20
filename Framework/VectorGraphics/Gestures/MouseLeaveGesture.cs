﻿using System;
using JJ.Framework.VectorGraphics.EventArg;
using JJ.Framework.VectorGraphics.Models.Elements;

namespace JJ.Framework.VectorGraphics.Gestures
{
	public class MouseLeaveGesture : GestureBase
	{
		public event EventHandler<MouseEventArgs> MouseLeave;

		private readonly MouseMoveGesture _diagramMouseMoveGesture;
		private Diagram _diagram;

		public MouseLeaveGesture()
		{
			_diagramMouseMoveGesture = new MouseMoveGesture();
			_diagramMouseMoveGesture.MouseMove += _diagramMouseMoveGesture_MouseMove;
		}

		~MouseLeaveGesture()
		{
			if (_diagram != null)
			{
				if (_diagramMouseMoveGesture != null)
				{
					_diagramMouseMoveGesture.MouseMove -= _diagramMouseMoveGesture_MouseMove;
					if (_diagram.Gestures.Contains(_diagramMouseMoveGesture))
					{
						_diagram.Gestures.Remove(_diagramMouseMoveGesture);
					}
				}
			}
		}

		private Element _previousSender;

		protected override void HandleMouseMove(object sender, MouseEventArgs e)
		{
			if (MouseLeave == null)
			{
				return;
			}

			// We bind to the first diagram we encounter,
			// because we do not want to burden the interface with passing a Diagram to the constructor.
			if (_diagram == null)
			{
				if (e.Element.Diagram != null)
				{
					_diagram = e.Element.Diagram;
					_diagram.Gestures.Add(_diagramMouseMoveGesture);
				}
			}

			_previousSender = sender as Element;
		}

		private void _diagramMouseMoveGesture_MouseMove(object sender, MouseEventArgs e)
		{
			if (sender != _previousSender)
			{
				var e2 = new MouseEventArgs(_previousSender, e.XInPixels, e.YInPixels, e.MouseButtonEnum);
				MouseLeave?.Invoke(sender, e2);

				_previousSender = sender as Element;
			}
		}
	}
}