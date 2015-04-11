﻿using System;
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
        List<double> resValues = new List<double>();//динамический массив значений ограничений
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

        private void fillNumberArrays()//заполнение массива коэффициентов
        {
            foreach(List<TextBox> textBoxLine in textBoxArray)//извлечение значения из каждого текстбокс и его сохранение в массив
            {
                List<double> coefLine = new List<double>();
                for (int i = 0; i < varNum; i++)
                {//сохранение в массиве коэффициентов переменных значений текстбоксов
                    coefLine.Add(getNumberFromForm(textBoxLine[i]));
                }
                if (textBoxLine.Count > varNum)
                {
                    resValues.Add(getNumberFromForm(textBoxLine[varNum]));//записать в массив значений ограничений содержимое последнего текстбокса в строке
                }
                coefficArray.Add(coefLine);
            }
        }

        private double getNumberFromForm(Control form)//извлечь число из формы
        {
            try//если содержимое текстбокса преобразуется в число, то оно будет сохранено в массиве
            {
                return Convert.ToDouble(form.Text);
            }
            catch (Exception)//иначе в массив будет записан 0 
            {
               return 0;
            }
        }

        //К ИНКАПЦУЛЯЦИИ//


        private List<List<double>> localCoefArray = new List<List<double>>();//массив коэффициентов
        int[] basisVarsIndexes;

        /// <summary>
        /// Поиск стартовых индексов базисных переменных(после преобразования гаусса)
        /// </summary>
        /// <returns></returns>
        private void chooseStartBasis()
        {
            basisVarsIndexes = new int[coefficArray.Count - 1];
            for (int i = 0; i < basisVarsIndexes.Length; i++)
            {
                for (int j = i; j < coefficArray[i+1].Count; j++)
                {
                    if (columnTest(j))
                    {
                        basisVarsIndexes[i] = j;
                        break;
                    }
                }
            }
        }

        
        /// <summary>
        /// Проверка является ли переменная с индексом varIndex базисной
        /// </summary>
        /// <param name="varIndex"></param>
        /// <returns></returns>
        private bool columnTest(int varIndex)
        {
            double coefSum = 0;//сумма индексов данной переменной
            for (int i = 1; i < coefficArray.Count; i++)//пробежаться по матрице коэффициентов 
            {
                coefSum += Math.Abs(coefficArray[i][varIndex]);//и найти абсолютную сумму коеффициентов переменной
            }
            if (coefSum.Equals(1))//Если сумма коеффициенов равна 1
            {
                return true;//то данная переменная является базисной
            }
            return false;//иначе не является
        }

        /// <summary>
        /// Сформировать локальный массив коэффициентов 
        /// </summary>
        private void formLocalCoefArray()
        {
            List<double> coefLine;
            for (int i = 0; i < basisVarsIndexes.Length; i++)
            {
                coefLine = new List<double>();
                foreach(double coef in coefficArray[findRestrictIndex(basisVarsIndexes[i])])
                {
                    coefLine.Add(coef);
                }
                localCoefArray.Add(coefLine);
            }
            localCoefArray.Add(findZ());
            localCoefArray.Add(findDelta());
        }

        /// <summary>
        /// Поиск ограничения, где коэффициент данной базисной переменной равен 1
        /// </summary>
        /// <param name="basisIndex"></param>
        /// <returns></returns>
        private int findRestrictIndex(int basisIndex)
        {
            for (int i = 1; i < coefficArray.Count; i++)//пройтись по строкам ограничений массива
            {
                if (coefficArray[i][basisVarsIndexes[basisIndex]] == 1)//и если элемент строки с указанным индексом = 1
                {
                    return i;//вернуть индекс данной строки
                }
            }
            return -1;//иначе вернуть -1
        }

        /// <summary>
        /// Найти относительные оценки Z
        /// </summary>
        /// <returns></returns>
        private List<double> findZ()
        {
            List<double>  ZLine = new List<double>();
            for (int i = 0; i < coefficArray[0].Count; i++)
            {
                double sum = 0;
                for (int j = 0; j < localCoefArray.Count; j++)
                {
                    sum += coefficArray[0][basisVarsIndexes[j]] * localCoefArray[j][i];
                }
                ZLine.Add(sum);
            }
            return ZLine;
        }

        /// <summary>
        /// Найти относительные оценки Дельта
        /// </summary>
        /// <returns></returns>
        private List<double> findDelta()
        {
            int indexZ = localCoefArray.Count - 1;
            List<double> DeltaLine = new List<double>();
            for (int i = 0; i < coefficArray[0].Count; i++)
            {
                DeltaLine.Add(coefficArray[0][i] - localCoefArray[indexZ][i]);    
            }
            return DeltaLine;
        }

        /// <summary>
        /// Проверка на выполнение условия окончания поиска максимума
        /// </summary>
        /// <returns></returns>
        private bool canFinish()
        {
            int deltaIndex = localCoefArray.Count - 1;
            if (localCoefArray[deltaIndex].Min() < 0)//если минимальная из оценок Дельта < 0
            {//то среди них есть отрицательные значения
                return true;//необходимо продолжить вычисления
            }
            return false;//иначе все оценки больше 0 => процесс вычислений можно завершить
        }

        //КОНЕЦ//

        private void calculationProcess()
        {
            int counter = 0;
            chooseStartBasis();
            formLocalCoefArray();
            while(canFinish())
            {
                counter ++;
                if (counter > 1000000)
                {
                    MessageBox.Show("Ошибка!\nМаксимум не найден после 1000000 итераций!");
                    break;
                }
            }
            localCoefArray.Clear();
            ;
        }

        private void calcBtn_Click(object sender, EventArgs e)
        {
            coefficArray.Clear(); 
            resValues.Clear();
            fillNumberArrays();
            MatrixPreparer preparer = new MatrixPreparer(coefficArray);
            coefficArray = preparer.prepareMatrix();

            calculationProcess();
        }



    }
}
