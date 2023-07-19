using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Core.Printing
{
    public abstract class PdfDocument
    {

		private string _entreprise;

		public string Entreprise
		{
			get 
			{
				return _entreprise;
			}
			set 
			{
				_entreprise = value; 
			}
		}


		public DateTime DateDuJour { get; set; }

		private string _pied;

		public string Pied
		{
			get { return _pied; }
			set { _pied = value; }
		}


	}
}
