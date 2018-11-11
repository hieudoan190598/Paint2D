using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint2D
{
    public partial class Paint2D : Form
    {
        //Attribute of Form
        Shape curObj;
        Bitmap primaryBMP;
        List<Shape> listObj;
        int objType;//1: Line, 2: Rectangle
        int mode;  //0: draw | 1: select | 2: move | 3: resize

        public Paint2D()
        {
            InitializeComponent();
            curObj = new Shape();
            //primaryBMP = new Bitmap(picBox.Width, picBox.Height,picBox.CreateGraphics()); //gan Graphics sinh ra tu picBox vs priamryBMP
            listObj = new List<Shape>();
            mode = 1; //mode default
        }

        private void Paint2D_Load(object sender, EventArgs e)
        {
            cbSize.SelectedIndex = 2;
            cbStyle.SelectedIndex = 0;
            primaryBMP = new Bitmap(picBox.Width, picBox.Height, picBox.CreateGraphics()); //gan Graphics sinh ra tu picBox vs priamryBMP
        }
        //Paint Event
        private void picBox_paint(object sender, PaintEventArgs e)
        {
            Graphics g = Graphics.FromImage(primaryBMP);
            g.Clear(Color.White);

            foreach (Shape o in listObj)
            {
                o.Draw(g);
            }
            e.Graphics.DrawImage(primaryBMP, 0, 0); //picBox call thi Draw len no thoi
        }
        //-----------------

        //Mouse Event

        private void btnCirArc_Click(object sender, EventArgs e)
        {

        }
        public void initObject(int objType)
        {
            switch (objType)
            {
                case 1:
                    curObj = new MyLine();
                    break;
                case 2:
                    curObj = new MyRectangle();
                    break;
                case 3:
                    curObj = new MyParallelogram();
                    break;
                case 4:
                    curObj = new MyPolyline();
                    break;
                case 5:
                    curObj = new MyEllipse();
                    break;
                case 6:
                    curObj = new MyCircle();
                    break;
                case 7:
                    curObj = new MyArc();
                    break;
                case 8:
                    curObj = new MyParabola();
                    break;
                case 9:
                    curObj = new MyHyperbola();
                    break;
                default:
                    curObj = null;
                    break;
            }
        }
        private void onMouseDown(object sender, MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Left)
            {
                if (mode == 0) //mode draw
                {
                    initObject(objType);
                    if (listObj.Count > 0) listObj[listObj.Count - 1].unSelect(); //bo chon thag truoc
                    curObj.setSelect(); //đang vẽ là đang chọn
                    listObj.Add(curObj);
                    curObj.isDraw = true;
                    //curObj.mouseDown(e);

                }
                //mode default
                if (mode == 1 /*&& !inProcess*/)
                {
                    List<Shape> dsTrung = new List<Shape>();
                    foreach (Shape o in listObj)
                    {
                        //Unselect all Object
                        o.unSelect();
                        if (o.isContain(e.Location))
                        {
                            curObj = o;
                            dsTrung.Add(o);
                        }
                    }
                    if (dsTrung.Count > 1)
                    {
                        foreach (Shape o in dsTrung)
                        {
                            if (Math.Abs((o.startPoint.X - e.Location.X) * (o.startPoint.X - e.Location.X)) +
                                Math.Abs((o.startPoint.Y - e.Location.Y) * (o.startPoint.Y - e.Location.Y)) <
                                Math.Abs((curObj.startPoint.X - e.Location.X) * (curObj.startPoint.X - e.Location.X)) +
                                Math.Abs((curObj.startPoint.Y - e.Location.Y) * (curObj.startPoint.Y - e.Location.Y)))
                            {
                                curObj = o;
                            }
                        }
                    }

                    curObj.setSelect(); //nho o form
                    //lam luc mouseDown 1 lan thoi
                    for (int i = 0; i < curObj.listControlPoint.Count;i++)
                    {
                        if (curObj.listControlPoint[i].Contains(e.Location))
                        {
                            curObj.isResize = true;
                            //inProcess = true;
                            curObj.currentCp = curObj.listControlPoint[i];
                            curObj.currentCpIndex = i;
                            //curObj.mouseDown(e);
                        }
                    }
                    if (curObj.isContain(e.Location) && !curObj.isResize)
                    {
                        curObj.isMove = true;
                        //curObj.mouseDown(e);
                        //inProcess = true;
                    }
                } //end TH mode =1
                    
                curObj.mouseDown(e);
                picBox.Refresh();
            } //end if click left mouse
        }

        //Update currentCp trong lúc onMouseMove
        //public void updateCurrentCp(MouseEventArgs e)
        //{

        //    curObj.currentCp = curObj.listControlPoint[curObj.currentCpIndex];
        //}
        private void onMouseMove(Object sender, MouseEventArgs e)
        {
            label1.Text = e.Location.X.ToString() + ", " + e.Location.Y.ToString();
            //chinh chuot cho dep thoi
            //Cursor = Cursors.Default;
            if (mode==1 && curObj.isSelect==true)
            {
                if (curObj.HitTest(e.Location) > 0)
                    Cursor = Cursors.Cross;
                else if (curObj.HitTest(e.Location) == 0)
                    Cursor = Cursors.SizeAll;
                else
                    Cursor = Cursors.Default;
            }
            else { Cursor = Cursors.Default; }
            //
            if (objType != 4 /*&& e.Button==MouseButtons.Left*/)
            {
                //curObj.currentCp = e.Location;
                //if (curObj.isResize)
                //{
                //    updateCurrentCp(e);
                //}
                curObj.mouseMove(e);
                picBox.Refresh();
            }
            if (objType==4 && e.Button == MouseButtons.None)
            {
                curObj.mouseMove(e);
                picBox.Refresh();
            }
            //picBox.Refresh();

        }

        private void onMouseUp(Object sender, MouseEventArgs e)
        {
            curObj.mouseUp(e);
            //inProcess = false;
            //curObj.isMove = false;
            //curObj.isDraw = false;
            picBox.Refresh();
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            objType = 1;
            mode = 0;
        }

        private void btnRec_Click(object sender, EventArgs e)
        {
            objType = 2;
            mode = 0;
        }

        private void btnPara_Click(object sender, EventArgs e)
        {
            objType = 3;
            mode = 0;
        }

        private void btnPoGon_Click(object sender, EventArgs e)
        {
            objType = 4;
            mode = 0;
        }

        private void onMouseHover(object sender, EventArgs e)
        {
            
        }

        private void onMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (objType == 4)
            {
                curObj.mouseDoubleClick(mode, e);
            }
        }

        private void btnEllipse_Click(object sender, EventArgs e)
        {
            objType = 5;
            mode = 0;
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            objType = 6;
            mode = 0;
        }

        private void btnEllArc_Click(object sender, EventArgs e)
        {
            objType = 7;
            mode = 0;
        }

        private void btnParabol_Click(object sender, EventArgs e)
        {
            objType = 8;
            mode = 0;
        }

        private void btnHype_Click(object sender, EventArgs e)
        {
            objType = 9;
            mode = 0;
        }

        private void btnCursor_Click(object sender, EventArgs e)
        {
            mode = 1;
        }

       
    }
}
