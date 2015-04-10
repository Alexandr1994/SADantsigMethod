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
                resValues.Add(getNumberFromForm(textBoxLine[varNum - 1]));//записать в массив значений ограничений содержимое последнего текстбокса в строке
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


        //-//-//-//-//-//-//К ИНКАПСУЛЯЦИИ
        private void zeroColumsSlice()//поиск и удаление нулевых переменных из массива чисел
        {
            for (int i = 0; i < varNum; i++)//перебор столбцов (без последнего)
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
                foreach(double coeffic in coefficArray[index])//перебор всех колонок, кроме последней
                {
                    absSummString += Math.Abs(coeffic);//подсчет абсолютной суммы коэффициентов в строке
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

        private void toCanonForm()
        {
            int addIndex = 1;//индекс ограничения, к которому будет добавлен не нулевой коэффициент
            while (coefficArray.Count >= coefficArray[0].Count)//до тех пор пока ограничений больше переменных вводить новые переменные
            {
                for (int i = 0; i < coefficArray.Count; i++)
                {
                    if (i == addIndex)
                    {
                        coefficArray[i].Add(1);
                    }
                    else
                    {
                        coefficArray[i].Add(0);
                    }
                }
                addIndex++;
            }
        }

        public void gaussMethod()
        {
            for (int i = 1; i < coefficArray.Count; i++)
            {
                if (coefficArray[i][i - 1] == 0)
                {
                    int index = findStrIndex(i);
                    List<double> temp = coefficArray[i];
                    coefficArray[i] = coefficArray[index];
                    coefficArray[index] = temp;
                }
                coefficArray[i] = multStr(coefficArray[i], 1/coefficArray[i][i - 1]);//приравнивание первого эл строки к 1
                for (int j = i+1; j < coefficArray.Count; j++)//отчистка нижнего угла матрицы
                {
                    coefficArray[j] = subStrs(coefficArray[j], multStr(coefficArray[i], coefficArray[j][i - 1]));
                }
            }
            for (int i = coefficArray.Count - 1; i > 1; i--)//отчистка верхнего угла матрицы
            {
                for (int j = i - 1; j > 0; j--)
                {
                    coefficArray[j] = subStrs(coefficArray[j], multStr(coefficArray[i], coefficArray[j][i - 1]));
                }
            }
        }

        


        private List<double> multStr(List<double> str, double multiplier)//умножение всех элементов строки на число
        {
            List<double> temp = str.GetRange(0 ,str.Count);
            
            for (int i = 0; i < temp.Count; i++)
            {
                temp[i] *= multiplier;//умножение элемента на число
            }
            return temp;//вернуть полученную строку
        }

        private List<double> subStrs(List<double> arg1, List<double> arg2)//вычитание строк
        {
            List<double> temp = arg1.GetRange(0, arg1.Count);
            for (int i = 0; i < arg1.Count; i++)
            {
                temp[i] -= arg2[i];//вычесть соответствующие элементы строк
            }
            return temp;//вернуть полученную строку
        }

        private int findStrIndex(int currentIndex)//поиск индекса строки с не нулевым элементом первого столбца
        {
            for (int i = currentIndex; i < coefficArray.Count; i++)
            {
                if (coefficArray[i][currentIndex - 1] != 0)
                {
                    return i;
                }
            }
           return -1;
        }
        //-//-//-//-//-//-//-//-//
        private void calcBtn_Click(object sender, EventArgs e)
        {
            coefficArray.Clear(); 
            resValues.Clear();
            fillNumberArrays();
            zeroTest();
            toCanonForm();
            gaussMethod();
            ;
        }



    }
}
