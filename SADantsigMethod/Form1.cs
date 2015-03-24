using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SADantsigMethod
{
    public partial class Form1 : Form
    {

        List<List<double>> coefficArray = new List<List<double>>();//двумерный динамический массив коэффициентов
        List<List<TextBox>> textBoxArray = new List<List<TextBox>>();//двумерный динамический массив TextBox-ов коэффициентов
        int restrictionNum=2;//количество ограничений
        int varNum=3;//количество переменных

        public Form1()
        {
            InitializeComponent();
            intiDataInterface();
        }


        private void intiDataInterface()//инициализация интерфейса для работы с данными
        {
            varAddBtn.Click += addNewVar;
            resAddBtn.Click += addNewRestricton;
            renderDataInterface();
        }


        private void addNewVar(object sender, EventArgs e)//добавление переменной
        {
            varNum++;//увеличение числа заполняемых переменных
            renderDataInterface();//перерисовка интерфейса
        }

        private void addNewRestricton(object sender, EventArgs e)//добавление ограничения
        {
            restrictionNum++;//увеличение числа ограничений
            renderDataInterface();//перерисовка интерфейса
        }

        private void renderDataInterface()//отрисовка интерфейса для работы с данными
        {
            dataBox.Controls.Clear();//отчистить groupbox
            addLabel(dataBox, 5, 30, "Функция:");
            addLabel(dataBox, 5, 95, "Ограничения:");
            textBoxArray.Clear();//отчистить массив форм 
            renderFunctionLine();//отобразить формы функции
            renderRestrictionForms();//отобразить формы ограничений
            
 
        }

        public void renderFunctionLine()//отобразить формы функции и ряд столбцов переменных
        {
            addLabel(dataBox, 5, 60, "f(x)");
            textBoxArray.Add(addCoefLine(25, 60));
            addLabelsLine();
        }

        public void renderRestrictionForms()//отобразить наборы форм для ограничений
        {
            for (int i = 0; i < restrictionNum; i++)//постороение маблицы полей для ограничений
            {
                addLabel(dataBox, 5, 130 + (i * 20), (i + 1).ToString());
                textBoxArray.Add(addCoefLine(25, 130 + (i * 20)));
            }
        }

        private void addLabelsLine()//отобразить ряд подписей переменных
        {
            for (int i = 0; i < varNum; i++)
            {
                addLabel(dataBox, textBoxArray[0][i].Left + 10, textBoxArray[0][i].Top - 15, "x" + (i + 1));
            }
        }

        private List<TextBox> addCoefLine(int coordX, int coordY)//добавить набор текстбоксов для ограничения или функции
        {
            List<TextBox> localCoefLine = new List<TextBox>();
            for (int i = 0; i < varNum; i++)//ряд текстбоксов для коэффициентов
            {
                if (i > 0)
                {
                    localCoefLine.Add(addTextBox(dataBox, 40, localCoefLine[i - 1].Left + 40, localCoefLine[i - 1].Top));
                }
                else
                {
                    localCoefLine.Add(addTextBox(dataBox, 40, coordX, coordY));
                }
            }
            addLabel(dataBox, localCoefLine[varNum - 1].Left + 40, localCoefLine[varNum - 1].Top, "=");//знак "="
            localCoefLine.Add(addTextBox(dataBox, 40, localCoefLine[varNum - 1].Left + 60, localCoefLine[varNum - 1].Top));//
            return localCoefLine;
        }

        private TextBox addTextBox(Control Controller, int weidth,int CoordX, int CoordY)//добавить поле для ввода коэфициента
        {
            TextBox newBox = new TextBox();
            newBox.Size = new Size(weidth, 20);
            newBox.Location = new Point(CoordX, CoordY);
            Controller.Controls.Add(newBox);
            return newBox;
        }

        private Label addLabel(Control Controller, int CoordX, int CoordY, String text)//добавить надпись
        {
            Label newLabel = new Label();
            newLabel.Location = new Point(CoordX, CoordY);
            newLabel.Text = text;
            newLabel.AutoSize = true;
            Controller.Controls.Add(newLabel);
            return newLabel;
        }

    }
}
