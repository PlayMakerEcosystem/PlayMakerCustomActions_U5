// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: Optimized StringBuilder


using UnityEngine;
using System.Collections.Generic;
using System.Text;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Builds a String from other Strings with less memory allocations.")]
	[HelpUrl("")]
	public class BuildStringFast : FsmStateAction
	{
		[RequiredField]
        [Tooltip("Array of Strings to combine.")]
		public FsmString[] stringParts;

        [Tooltip("Separator to insert between each String. E.g. space character.")]
        public FsmString separator;

        [Tooltip("Add Separator to end of built string.")]
	    public FsmBool addToEnd;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the final String in a variable.")]
        public FsmString storeResult;

        [Tooltip("Repeat every frame while the state is active.")]
		public FsmBool everyFrame;
	    

		private char[] m_chars = null;
		private string m_stringGenerated = "";
		private bool m_isStringGenerated = false;
		private int m_charsCount = 0;
		private int m_charsCapacity = 0;

		public override void Reset()
		{
			stringParts = new FsmString[3];
			separator = null;
		    addToEnd = true;
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoBuildString();
			
			if (!everyFrame.Value)
			{
			    Finish();
			}
		}

		public override void OnUpdate()
		{
			DoBuildString();
		}
		
		void DoBuildString()
		{
			if (storeResult == null) return;
			
	
			m_chars = new char[ m_charsCapacity = 32 ];
			Clear();

		    for (var i = 0; i < stringParts.Length-1; i++)
		    {
				Append(stringParts[i].Value);
				Append(separator.Value);
		    }

			ToString();

			Append(stringParts[stringParts.Length - 1].Value);

		    if (addToEnd.Value)
		    {
				Append(separator.Value);
		    }


			storeResult.Value = ToString();
		
		}

		public void Append( string value )
		{
			ReallocateIFN( value.Length );
			int n = value.Length;
			for( int i=0; i<n; i++ )
				m_chars[ m_charsCount + i ] = value[ i ];
			m_charsCount += n;
			m_isStringGenerated = false;
		}

		private void ReallocateIFN( int nbCharsToAdd )
		{
			if( m_charsCount + nbCharsToAdd > m_charsCapacity )
			{
				m_charsCapacity = System.Math.Max( m_charsCapacity + nbCharsToAdd, m_charsCapacity * 2 );
				char[] newChars = new char[ m_charsCapacity ];
				m_chars.CopyTo( newChars, 0 );
				m_chars = newChars;
			}
		}


		public bool IsEmpty()
		{
			return (m_isStringGenerated ? (m_stringGenerated == null) : (m_charsCount == 0));
		}

		public void Clear()
		{
			m_charsCount = 0;
			m_isStringGenerated = false;
		}

		public override string ToString()
		{
			if( !m_isStringGenerated )
			{
				m_stringGenerated = new string( m_chars, 0, m_charsCount );
				m_isStringGenerated = true;
			}
			return m_stringGenerated;
		}

	}
}
