// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: Optimized convert float StringBuilder 


using UnityEngine;
using System.Collections.Generic;
using System.Text;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Converts an Float value to a String value with an optional format with less memory allocations.")]
	[HelpUrl("")]
	public class ConvertFloatToStringFast : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Float variable to convert.")]
		public FsmFloat floatVariable;
		 
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("A String variable to store the converted value.")]
		public FsmString stringVariable;
		 
		[Tooltip("Repeat every frame. Useful if the Int variable is changing.")]
		public FsmBool everyFrame;
	    
		private readonly char[] IntToChar = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
		private char[] m_chars = null;
		private string m_stringGenerated = "";
		private bool m_isStringGenerated = false;
		private int m_charsCount = 0;
		private int m_charsCapacity = 0;

		public override void Reset()
		{
			floatVariable = null;
			stringVariable = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoConvertIntToString();
			
			if (!everyFrame.Value)
			{
			    Finish();
			}
		}

		public override void OnUpdate()
		{
			DoConvertIntToString();
		}
		
		void DoConvertIntToString()
		{

			m_chars = new char[ m_charsCapacity = 32 ];

					Clear();
					Append(floatVariable.Value);
					stringVariable.Value = ToString();
					
		   
		
		}

		public void Append( float value )
		{
		
			ReallocateIFN( 16 );
			
		
			if( value < 0 )
			{
				value = -value;
				m_chars[ m_charsCount++ ] = '-';
			}
			
		
			int nbFloatDigits = 0;
			while( Mathf.Abs( value-Mathf.Round( value ) ) > 0.000001f )
			{
				value *= 10;
				nbFloatDigits++;
			}
			int valueInt = Mathf.RoundToInt( value );
			
		
			int nbChars = 0;
			do
			{
				m_chars[ m_charsCount++ ] = IntToChar[ valueInt%10 ];
				valueInt /= 10;
				nbChars++;
				if( nbFloatDigits == nbChars )
				{
					m_chars[ m_charsCount++ ] = '.';
					nbChars++;
				}
			} while( valueInt != 0 || nbChars <= nbFloatDigits+1 );
			
		
			for( int i=nbChars/2-1; i>=0; i-- ) 
			{
				char c = m_chars[ m_charsCount-i-1 ];
				m_chars[ m_charsCount-i-1 ] = m_chars[ m_charsCount-nbChars+i ];
				m_chars[ m_charsCount-nbChars+i ] = c;
			}
			m_isStringGenerated = false;
		}

		private void Append( int value )
		{

			ReallocateIFN( 16 );
			

			if( value < 0 )
			{
				value = -value;
				m_chars[ m_charsCount++ ] = '-';
			}

			int nbChars = 0;
			do
			{
				m_chars[ m_charsCount++ ] = IntToChar[ value%10 ];
				value /= 10;
				nbChars++;
			} while( value != 0 );

			for( int i=nbChars/2-1; i>=0; i-- )
			{
				char c = m_chars[ m_charsCount-i-1 ];
				m_chars[ m_charsCount-i-1 ] = m_chars[ m_charsCount-nbChars+i ];
				m_chars[ m_charsCount-nbChars+i ] = c;
			}
			m_isStringGenerated = false;
		}

		private void ReallocateIFN( int nbCharsToAdd )
		{
			if( m_charsCount + nbCharsToAdd > m_charsCapacity )
			{
				//Debug.Log( "- CString reallocation " + m_charsCapacity + "=>" + System.Math.Max( m_charsCapacity + nbCharsToAdd, m_charsCapacity * 2 ) );
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
