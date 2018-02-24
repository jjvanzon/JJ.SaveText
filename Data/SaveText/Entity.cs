namespace JJ.Data.SaveText
{
	public class Entity
	{
		private int _id;
		private string _text;

		public virtual int ID
		{
			get { return _id; }
			set { _id = value; }
		}

		public virtual string Text
		{
			get { return _text; }
			set { _text = value; }
		}
	}
}
