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
            varAddBtn.Click += addNewVar;//кнопки добавления
            resAddBtn.Click += addNewRestricton;
            removeVarBtn.Click += removeVar;//кнопки удаления
            removeResBtn.Click += removeRes;
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

        private void removeVar(object sender, EventArgs e)
        {
            if (varNum > 3)
            {
                varNum--;//уменьшение числа переменных
                renderDataInterface();//перерисовка интерфейса
            }
        }

        private void removeRes(object sender, EventArgs e)
        {
            if (restrictionNum > 2)
            {
                restrictionNum--;//уменьшение числа ограничений
                renderDataInterface();//перерисовка интерфейса
            }        
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
            addLabel(dataBox, 5, 60, "f(x)=");
            textBoxArray.Add(addVarsLine(35, 60));
            addLabelsLine();
        }

        public void renderRestrictionForms()//отобразить наборы форм для ограничений
        {
            for (int i = 0; i < restrictionNum; i++)//постороение маблицы полей для ограничений
            {
                addLabel(dataBox, 5, 130 + (i * 20), (i + 1).ToString());
                textBoxArray.Add(addCoefLine(35, 130 + (i * 20)));
            }
        }

        private void addLabelsLine()//отобразить ряд подписей переменных
        {
            for (int i = 0; i < varNum; i++)
            {
                addLabel(dataBox, textBoxArray[0][i].Left + 10, textBoxArray[0][i].Top - 15, "x" + (i + 1));
            }
        }

        private List<TextBox> addCoefLine(int coordX, int coordY)//добавить набор текстбоксов для ограничения
        {
            List<TextBox> localCoefLine = addVarsLine(coordX, coordY);
            addLabel(dataBox, localCoefLine[varNum - 1].Left + 40, localCoefLine[varNum - 1].Top, "=");//знак "="
            localCoefLine.Add(addTextBox(dataBox, 40, localCoefLine[varNum - 1].Left + 60, localCoefLine[varNum - 1].Top));
            return localCoefLine;
        }


        private List<TextBox> addVarsLine(int coordX, int coordY)//добавить набор текстбоксов отвечающих за переменные
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
            return localCoefLine;//вернуть набор текстбоксов
        }

        private TextBox addTextBox(Control Controller, int weidth,int CoordX, int CoordY)//добавить поле для ввода коэфициента
        {
            TextBox newBox = new TextBox();
            newBox.Size = new Size(weidth, 20);
            newBox.Location = new Point(CoordX, CoordY);
            newBox.Text = "0";
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

        private void fillCoefArray()//заполнение массива коэффициентов
        {
            foreach(List<TextBox> textBoxLine in textBoxArray)//извлечение значения из каждого текстбокс и его сохранение в массив
            {
                List<double> coefLine = new List<double>();
                foreach (TextBox textBox in textBoxLine)
                {
                    try//если содержимое текстбокса преобразуется в число, то оно будет сохранено в массиве
                    {
                        coefLine.Add(Convert.ToDouble(textBox.Text));
                    }
                    catch (Exception)//иначе в массив будет записан 0 
                    {
                        coefLine.Add(0);
                    }
                }
                coefficArray.Add(coefLine);
            }
        }
        //-//-//-//-//-//-//
        private void zeroColumsSlice()//поиск и удаление нулевых переменных из массива чисел
        {
            for (int i = 0; i < varNum-1; i++)//перебор столбцов (без последнего)
            {
                double absSummColum = 0;//сумма коэффициентов в колоне по модулю
                foreach(List<double> coefLine in coefficArray)//перебор всех строк
                {
                    absSummColum += Math.Abs(coefLine[i]);//подсчет абсолютной суммы всех коэффициентов в столюце
                }
                if (absSummColum == 0)//если сумма = 0
                {
                    removeColum(i);//удаление колонки
                }
            }
        }

        private void removeColum(int index)//удаление колонки коэффициентов
        {
            foreach (List<double> coefLine in coefficArray)//перебор всех строк
            {
                coefLine.RemoveAt(index);
            }
        }

        private void zeroStringSlice()//поиск и удаление нулевого ограничения
        {
            for (int index = 1; index < coefficArray.Count; index++)//перебор всех строк 
            {
                double absSummString = 0;//абсолютная сумма коэффициентов
                for (int i = 0; i < coefficArray[index].Count - 1; i++)//перебор всех колонок, кроме последней
                {
                    absSummString += Math.Abs(coefficArray[index][i]);//подсчет абсолютной суммы коэффициентов в строке
                }
                if (absSummString == 0)//если сумма = 0 
                {
                    coefficArray.RemoveAt(index);//удалить строку
                }
            }
        }

        private void zeroTest()//уничтожение нулевых строк и столбцов
        {
            zeroColumsSlice();//удаление нулевых столбцов
            zeroStringSlice();//удаление нулевых строк
        }
        //-//-//-//-//-//-//-//-//
        private void calcBtn_Click(object sender, EventArgs e)
        {
            coefficArray.Clear();
            fillCoefArray();
            zeroTest();
            ;
        }



    }
}
