using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpGL;
using SharpGL.Enumerations;
using Microsoft.VisualBasic;
using Dicom;
using SharpGL.SceneGraph.Assets;
using System.IO;



namespace Opengl
{

    public partial class Form1 : Form
    {
        OpenGL gl;

        #region HINDY CODE
        Texture texture0 = new Texture();
        Texture texture1 = new Texture();
        Texture texture2 = new Texture();
        Texture texture3 = new Texture();
        Texture texture4 = new Texture();
        Texture texture5 = new Texture();
        Texture texture6 = new Texture();
        Texture texture7 = new Texture();
        Texture texture8 = new Texture();
        Texture texture9 = new Texture();
        Texture texture10 = new Texture();
        Texture texture11 = new Texture();
        #endregion

        List<ushort> pixels = new List<ushort>();
        DicomDecoder dd = new DicomDecoder();
        Mode mode = Mode.Normal;
        byte[] pix = new byte[1];

        public Form1()
        {
            InitializeComponent();
            gl = (gl == null) ? this.openGLControl1.OpenGL : gl;
            dd.DicomFileName = "dicom.dcm";
            dd.GetPixels16(ref pixels);


            texture0.Create(gl, pixels.ToBitmap(dd.width, dd.height));
            texture1.Create(gl, pixels.ToBitmap(dd.width, dd.height).Conv(RobinsonFilter.E));
            texture2.Create(gl, pixels.ToBitmap(dd.width, dd.height).Conv(RobinsonFilter.NE));
            texture3.Create(gl, pixels.ToBitmap(dd.width, dd.height).Conv(RobinsonFilter.N));
            texture4.Create(gl, pixels.ToBitmap(dd.width, dd.height).Conv(RobinsonFilter.NW));
            texture5.Create(gl, pixels.ToBitmap(dd.width, dd.height).Conv(RobinsonFilter.W));
            texture6.Create(gl, pixels.ToBitmap(dd.width, dd.height).Conv(RobinsonFilter.SW));
            texture7.Create(gl, pixels.ToBitmap(dd.width, dd.height).Conv(RobinsonFilter.S));
            texture8.Create(gl, pixels.ToBitmap(dd.width, dd.height).Conv(RobinsonFilter.SE));
            texture9.Create(gl, pixels.ToBitmap(dd.width, dd.height).Conv(RobinsonFilter.W_LP1, 9));
            texture10.Create(gl, pixels.ToBitmap(dd.width, dd.height).Conv(RobinsonFilter.W_LP2, 6));
            texture11.Create(gl, pixels.ToBitmap(dd.width, dd.height).Conv(RobinsonFilter.W_LP3, 16));

        }

        private void openGLControl1_OpenGLDraw(object sender, RenderEventArgs args)
        {
            gl = (gl == null) ? this.openGLControl1.OpenGL : gl;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);	// Clear The Screen And The Depth Buffer
            gl.LoadIdentity();					// Reset The View               

            gl.Viewport(0, 0, this.Width, this.Height);
            gl.MatrixMode(MatrixMode.Projection);
            gl.Ortho(-this.Width / 2, this.Width / 2, -this.Height / 2, this.Height / 2, -1, 1);

            gl.Enable(OpenGL.GL_TEXTURE_2D);

            switch (mode)
            {
                case Mode.Normal:
                    texture0.Bind(gl);
                    break;
                case Mode.E:
                    texture1.Bind(gl);
                    break;
                case Mode.NE:
                    texture2.Bind(gl);
                    break;
                case Mode.N:
                    texture3.Bind(gl);
                    break;
                case Mode.NW:
                    texture4.Bind(gl);
                    break;
                case Mode.W:
                    texture5.Bind(gl);
                    break;
                case Mode.SW:
                    texture6.Bind(gl);
                    break;
                case Mode.S:
                    texture7.Bind(gl);
                    break;
                case Mode.SE:
                    texture8.Bind(gl);
                    break;
                case Mode.W_LP1:
                    texture9.Bind(gl);
                    break;
                case Mode.W_LP2:
                    texture10.Bind(gl);
                    break;
                case Mode.W_LP3:
                    texture11.Bind(gl);
                    break;
                default:
                    break;
            }

            gl.Begin(BeginMode.Quads);
            gl.TexCoord(1.0f, 0.0f); gl.Vertex(-dd.width / 2-80, -dd.height / 2-50);
            gl.TexCoord(1.0f, 1.0f); gl.Vertex(dd.width / 2-80, -dd.height / 2-50);
            gl.TexCoord(0.0f, 1.0f); gl.Vertex(dd.width / 2-80, dd.height / 2-50);
            gl.TexCoord(0.0f, 0.0f); gl.Vertex(-dd.width / 2-80, dd.height / 2-50);
            gl.End();

            gl.Flush();
        }



        private void openGLControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case ' ':
                    if ((int)mode < 12)
                    {
                        mode++;
                    }
                    else
                    {
                        mode = Mode.Normal;
                    }
                    break;
                case 'q':
                    this.Close();
                    break;
            }
        }

        public enum Mode
        {
            Normal,
            E,
            NE,
            N,
            NW,
            W,
            SW,
            S,
            SE,
            W_LP1,
            W_LP2,
            W_LP3
        }

        private void openGLControl1_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (Reset.Checked) mode=Mode.Normal;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked) mode = Mode.N;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked) mode = Mode.NW;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked) mode = Mode.W;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton10.Checked) mode = Mode.W_LP1;
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton11.Checked) mode = Mode.W_LP2;
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton11.Checked) mode = Mode.W_LP3;
        }
    }
}
