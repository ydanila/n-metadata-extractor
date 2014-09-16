using System;
using System.Text;
using System.Collections;
using System.IO;
using com.drew.metadata;
using com.utils;

/// <summary>
/// This class was first written by Drew Noakes in Java.
///
/// This is public domain software - that is, you can do whatever you want
/// with it, and include it software that is licensed under the GNU or the
/// BSD license, or whatever other licence you choose, including proprietary
/// closed source licenses.  I do ask that you leave this header in tact.
///
/// If you make modifications to this code that you think would benefit the
/// wider community, please send me a copy and I'll post it on my site.
///
/// If you make use of this code, Drew Noakes will appreciate hearing 
/// about it: <a href="mailto:drew@drewnoakes.com">drew@drewnoakes.com</a>
///
/// Latest Java version of this software kept at 
/// <a href="http://drewnoakes.com">http://drewnoakes.com/</a>
///
/// The C# class was made by Ferret Renaud: 
/// <a href="mailto:renaud91@free.fr">renaud91@free.fr</a>
/// If you find a bug in the C# code, feel free to mail me.
/// </summary>
namespace com.drew.metadata.jpeg
{
	/// <summary>
	/// The Jpeg Directory class
	/// </summary>
	public class JpegDirectory : Directory 
	{
		/// <summary>
		/// This is in bits/sample, usually 8 (12 and 16 not supported by most software).
		/// </summary>
		public const int TAG_JPEG_DATA_PRECISION = 0;

		/// <summary>
		/// The image's height.  Necessary for decoding the image, so it should always be there.
		/// </summary>
		public const int TAG_JPEG_IMAGE_HEIGHT = 1;

		/// <summary>
		/// The image's width.  Necessary for decoding the image, so it should always be there.
		/// </summary>
		public const int TAG_JPEG_IMAGE_WIDTH = 3;

		/// <summary>
		/// Usually 1 = grey scaled, 3 = color YcbCr or YIQ, 4 = color CMYK Each component TAG_COMPONENT_DATA_[1-4], 
		/// has the following meaning: component Id(1byte)(1 = Y, 2 = Cb, 3 = Cr, 4 = I, 5 = Q), 
		/// sampling factors (1byte) (bit 0-3 vertical., 4-7 horizontal.), 
		/// quantization table number (1 byte).
		/// This info is from http://www.funducode.com/freec/Fileformats/format3/format3b.htm
		/// </summary>
		public const int TAG_JPEG_NUMBER_OF_COMPONENTS = 5;

		// NOTE!  Component tag type int values must increment in steps of 1

		/// <summary>
		/// the first of a possible 4 color components.  Number of components specified in TAG_JPEG_NUMBER_OF_COMPONENTS.
		/// </summary>
		public const int TAG_JPEG_COMPONENT_DATA_1 = 6;

		/// <summary>
		/// the second of a possible 4 color components.  Number of components specified in TAG_JPEG_NUMBER_OF_COMPONENTS.
		/// </summary>
		public const int TAG_JPEG_COMPONENT_DATA_2 = 7;

		/// <summary>
		/// the third of a possible 4 color components.  Number of components specified in TAG_JPEG_NUMBER_OF_COMPONENTS.
		/// </summary>
		public const int TAG_JPEG_COMPONENT_DATA_3 = 8;

		/// <summary>
		/// the fourth of a possible 4 color components.  Number of components specified in TAG_JPEG_NUMBER_OF_COMPONENTS.
		/// </summary>
		public const int TAG_JPEG_COMPONENT_DATA_4 = 9;

		protected static readonly ResourceBundle BUNDLE = new ResourceBundle("JpegMarkernote");
		protected static readonly IDictionary tagNameMap =  JpegDirectory.InitTagMap();

		/// <summary>
		/// Initialize the tag map.
		/// </summary>
		/// <returns>the tag map</returns>
		private static IDictionary InitTagMap() 
		{
			IDictionary resu = new Hashtable();
			resu.Add(TAG_JPEG_DATA_PRECISION, BUNDLE["TAG_JPEG_DATA_PRECISION"]);
			resu.Add(TAG_JPEG_IMAGE_WIDTH, BUNDLE["TAG_JPEG_IMAGE_WIDTH"]);
			resu.Add(TAG_JPEG_IMAGE_HEIGHT, BUNDLE["TAG_JPEG_IMAGE_HEIGHT"]);
			resu.Add(TAG_JPEG_NUMBER_OF_COMPONENTS, BUNDLE["TAG_JPEG_NUMBER_OF_COMPONENTS"]);
			resu.Add(TAG_JPEG_COMPONENT_DATA_1, BUNDLE["TAG_JPEG_COMPONENT_DATA_1"]);
			resu.Add(TAG_JPEG_COMPONENT_DATA_2, BUNDLE["TAG_JPEG_COMPONENT_DATA_2"]);
			resu.Add(TAG_JPEG_COMPONENT_DATA_3, BUNDLE["TAG_JPEG_COMPONENT_DATA_3"]);
			resu.Add(TAG_JPEG_COMPONENT_DATA_4, BUNDLE["TAG_JPEG_COMPONENT_DATA_4"]);
			return resu;
		}

		/// <summary>
		/// Constructor of the object.
		/// </summary>
		public JpegDirectory() : base()
		{
			this.SetDescriptor(new JpegDescriptor(this));
		}

		/// <summary>
		/// Provides the name of the directory, for display purposes.  E.g. Exif 
		/// </summary>
		/// <returns>the name of the directory</returns>
		public override string GetName() 
		{
			return BUNDLE["MARKER_NOTE_NAME"];
		}

		/// <summary>
		/// Provides the map of tag names, hashed by tag type identifier. 
		/// </summary>
		/// <returns>the map of tag names</returns>
		protected override IDictionary GetTagNameMap() 
		{
			return tagNameMap;
		}

		/**
		 *
		 * @param componentNumber 
		 * @return
		 */

		/// <summary>
		/// Gets the component
		/// </summary>
		/// <param name="componentNumber">The zero-based index of the component.  This number is normally between 0 and 3. Use GetNumberOfComponents for bounds-checking.</param>
		/// <returns>the JpegComponent</returns>
		public JpegComponent GetComponent(int componentNumber) 
		{
			int tagType = JpegDirectory.TAG_JPEG_COMPONENT_DATA_1 + componentNumber;

			JpegComponent component = (JpegComponent) GetObject(tagType);

			return component;
		}

		/// <summary>
		/// Gets image width
		/// </summary>
		/// <returns>image width</returns>
		public int GetImageWidth() 
		{
			return GetInt(JpegDirectory.TAG_JPEG_IMAGE_WIDTH);
		}

		/// <summary>
		/// Gets image height
		/// </summary>
		/// <returns>image height</returns>
		public int GetImageHeight() 
		{
			return GetInt(JpegDirectory.TAG_JPEG_IMAGE_HEIGHT);
		}

		/// <summary>
		/// Gets the Number Of Components
		/// </summary>
		/// <returns>the Number Of Components</returns>
		public int GetNumberOfComponents() 
		{
			return GetInt(JpegDirectory.TAG_JPEG_NUMBER_OF_COMPONENTS);
		}
	}
}
