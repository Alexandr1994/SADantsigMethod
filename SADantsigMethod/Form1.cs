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
            initDataInterface();
        }


        private void initDataInterface()//инициализация интерфейса для работы с данными
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
            return localCoefLine;//набор текстбоксов
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
        int[] basisVarsIndexes;//массив индексов базисных переменных
        int godRowIndex;//индекс "неприкосновенной" строки при смене базиса
        int allowElIndex;//индкс разрешающего элемента в строке

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
            localCoefArray.Add(findZ(localCoefArray));
            localCoefArray.Add(findDelta(localCoefArray));
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
        private List<double> findZ(List<List<double>> matrix)
        {
            List<double>  ZLine = new List<double>();
            for (int i = 0; i < matrix[0].Count; i++)
            {
                double sum = 0;
                for (int j = 0; j < matrix.Count; j++)
                {
                    sum += coefficArray[0][basisVarsIndexes[j]] * matrix[j][i];
                }
                ZLine.Add(sum);
            }
            return ZLine;
        }

        /// <summary>
        /// Найти относительные оценки Дельта
        /// </summary>
        /// <returns></returns>
        private List<double> findDelta(List<List<double>> matrix)
        {
            int indexZ = matrix.Count - 1;
            List<double> DeltaLine = new List<double>();
            for (int i = 0; i < matrix[0].Count; i++)
            {
                DeltaLine.Add(coefficArray[0][i] - matrix[indexZ][i]);    
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
            foreach (double value in localCoefArray[deltaIndex])//перебрать элементы строки Дельта
            {
                if (value > 0)//если в строке Дельта есть положительные числа
                {
                    return true;//то необходимо продолжить вычисления
                }
            }
            return false;//иначе все оценки отрицательны или равны 0 => процесс вычислений можно завершить
        }

        /// <summary>
        /// Найти новый набор базисных переменных
        /// </summary>
        private void findNewBasis()
        {
            allowElIndex = findMaxDeltaIndex();//индекс новой базисной переменной = индексу максимальной "Дельты"
            godRowIndex = findChangeVarIndex(allowElIndex);//найти и сохранить индекс неизменяющейся строки
            basisVarsIndexes[godRowIndex] = allowElIndex;//замена базисной переменной в массиве
        }

        /// <summary>
        /// Найти столбец с наибольшим приращением Дельта
        /// </summary>
        /// <returns></returns>
        private int findMaxDeltaIndex()
        {
            List<double> deltaLine = localCoefArray[localCoefArray.Count - 1];//строка "Дельт" - последняя строка массива
            return deltaLine.IndexOf(deltaLine.Max());//вернуть индекс максимального значения строки
        }

        /// <summary>
        /// Найти индекс заменяемой переменной базиса
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        private int findChangeVarIndex(int columnIndex)
        {
            List<double> divArray = findDivs(columnIndex);//найти массив частных
            return divArray.IndexOf(divArray.Min());//вернуть индекс наименьшего элемента массива, который соответствует индексу заменяемой переменной
        }

        /// <summary>
        /// Найти массив частных базисных решений на элементы указанного столбца 
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        private List<double> findDivs(int columnIndex)
        {
            List<double> divArray = new List<double>();//инициализация массива
            for (int i = 0; i < resValues.Count; i++)//
            {
                if (localCoefArray[i][columnIndex] > 0)//если элемент выбранного столбца массива коеффициентов больше 0
                {
                    divArray.Add(resValues[i] / localCoefArray[i][columnIndex]);//разделить текущее базисное решение на этот элемент
                }
                else
                {
                    divArray.Add(Double.MaxValue);//иначе исключить данный элемент из рассмотрения
                }
               }
            return divArray;//вернуть найденный массив
        }

        /// <summary>
        /// Копирование значений строки матрицы в новую строку, с возможностью деление каждого элемента данной строки на число
        /// </summary>
        /// <param name="targetStr">Целевая строка</param>
        /// <param name="diver">Делитель</param>
        /// <returns></returns>
        private List<double> copyString(List<double> targetStr, double diver)
        {
            List<double> retStr = new List<double>();//возвращаемая строка
            foreach (double value in targetStr)
            {
                retStr.Add(value/diver);//поэлементное копирование
            }
            return retStr;//вернуть копию строки
        }

        /// <summary>
        /// Построение новой симплекс таблицы
        /// </summary>
        private void constructNewMatrix()
        {
            List<List<double>> newCoefMatrix = new List<List<double>>();//создание временной матрицы
            List<double> newRes = new List<double>();//массив базисных ренений
            for (int i = 0; i < resValues.Count; i++)
            {
                newCoefMatrix.Add(new List<double>());//инициализация строки времменой матрицы
                if (i == godRowIndex)//если рассматриваемая строка разрешающая, 
                {
                    newCoefMatrix[i] = copyString(localCoefArray[i], localCoefArray[godRowIndex][allowElIndex]);//то сохранить её в временной матрице, разделенную на разрешающий элемент
                    newRes.Add(resValues[i]);//сохранить старое базисное решение
                    continue;
                }
                for(int j = 0; j < localCoefArray[i].Count; j++)//иначе пересчитать каждые элемент строки
                {
                    newCoefMatrix[i].Add(localCoefArray[i][j] - (localCoefArray[i][allowElIndex] * localCoefArray[godRowIndex][j]) / localCoefArray[godRowIndex][allowElIndex]);
                }
                newRes.Add(resValues[i] - (resValues[godRowIndex] * localCoefArray[i][allowElIndex]) / localCoefArray[godRowIndex][allowElIndex]);//вычисление нового базисного решения
            }
            newCoefMatrix.Add(findZ(newCoefMatrix));//вычисление новогл массива Z
            newCoefMatrix.Add(findDelta(newCoefMatrix));//вычисление нового массива Delta
            localCoefArray.Clear();//отчистить устаревшую матрицу
            resValues.Clear();//отчистить устаревшие базисные решения
            localCoefArray = newCoefMatrix;//сохранение вычисленной матрице в массиве
            resValues = newRes;//сохранение вычисленных базисных решений
            

        }

        /// <summary>
        /// Получить найденный максимум
        /// </summary>
        /// <returns></returns>
        public double getMax()
        {
            double sum = 0;//сумма
            for (int i = 0; i < basisVarsIndexes.Length; i++ )//вычисление максимума
            {
                sum = resValues[i] * coefficArray[0][basisVarsIndexes[i]];
            }
            return sum;
        }

        /// <summary>
        /// Получение вектора значений переменных
        /// </summary>
        /// <returns></returns>
        public double[] getVarsValuesVector()
        {
            double[] retVector = new double[coefficArray[0].Count];//инициализация масс
            for (int i = 0; i < retVector.Length; i++)//заполнение возвращаемого вектора
            {
                retVector[i] = 0;//установк очередного элемента вектора в 0
                for(int j = 0; j < basisVarsIndexes.Length; j++)//проверка переменной на принадлежность к базису
                {

                    if (i == basisVarsIndexes[j])//если переменная относится к базису
                    {
                        retVector[i] = resValues[j];//внести в элемент вектора соответствующее базисное решение
                        break;
                    }
                }
            }
            return retVector;//вернуть найденный вектор
        }


        /// <summary>
        /// Основная вычислительная функция, возвращает true в случае успешного выполнения вычислений
        /// </summary>
        /// <returns></returns>
        public bool calculationProcess()
        {
            int counter = 0;
            chooseStartBasis();
            formLocalCoefArray();
            while(canFinish())
            {
                findNewBasis();
                constructNewMatrix();
                counter ++;
                if (counter > 10000000)
                {
                    return false;
                }
            }
            return true;
        }

        //КОНЕЦ//

        private void calcBtn_Click(object sender, EventArgs e)
        {
            coefficArray.Clear(); 
            resValues.Clear();
            fillNumberArrays();
            MatrixPreparer preparer = new MatrixPreparer(coefficArray);
            try
            {
                coefficArray = preparer.prepareMatrix();
                if (!calculationProcess())
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                statusLabel.Text = "Статус: \n Ошибка вычисления или подготовки!";
                resultLabel.Text = "Результат: \nНе может быть найден!";
                MessageBox.Show("Ошибка!\nМаксимум не может быть найден!");
                clearAll();
                return;
            }
            statusLabel.Text = "Статус: \n Вычисление выполнено!";
            resultLabel.Text = "Результат: Max =" + getMax().ToString() + "\nВектор значений {";
            double[] vector = getVarsValuesVector();
            foreach (double el in vector)
            {
                resultLabel.Text +=" " + el.ToString("G4") + " ";
            }
            resultLabel.Text += "}";
            clearAll();
        }

        /// <summary>
        /// Отчистка переменных
        /// </summary>
        private void clearAll()
        {
            coefficArray.Clear();
            resValues.Clear();
        }

    }
}
