using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace File_Manager
{
    [Serializable]
    public class Customised
    {
        public string Theme = "white";
        public string view = "Tile";
      
       private Font font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular, GraphicsUnit.Pixel, 0, false);

        [XmlIgnore()]
        public Font Font
        {
            get { return font; }
            set { font = value; }
        }

        [Browsable(false)]
        public string FontSerialize
        {
            get { return TypeDescriptor.GetConverter(typeof(Font)).ConvertToInvariantString(font); }
            set { font = TypeDescriptor.GetConverter(typeof(Font)).ConvertFromInvariantString(value) as Font; }
        }
        [XmlIgnore()]
        public Color ListViewColor { get; set; }

        [XmlIgnore()]
        public Color TextColor { get; set; }
        [XmlElement("TextColor")]
        public int TextColorAsArgb
        {
            get { return TextColor.ToArgb(); }
            set { TextColor = Color.FromArgb(value); }
        }

        [XmlElement("ListViewColor")]
        public int ListViewColorAsArgb
        {
            get { return ListViewColor.ToArgb(); }
            set { ListViewColor = Color.FromArgb(value); }
        }
    }
   
}
